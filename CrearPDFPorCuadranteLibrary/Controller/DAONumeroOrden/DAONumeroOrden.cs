using ConnectorLibrary.Constantes;
using ConnectorLibrary.Sql;
using CrearPDFPorCuadranteLibrary.Model.Cuadrantes;
using CrearPDFPorCuadranteLibrary.Model.Proyectos;
using CrearPDFPorCuadranteLibrary.Utilities;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrearPDFPorCuadranteLibrary.Controller.DAONumeroOrden
{
    public class DAONumeroOrden
    {
        private static readonly object _mutex = new object();
        private static DAONumeroOrden _Instance;
        public static DAONumeroOrden Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_Instance == null)
                    {
                        _Instance = new DAONumeroOrden();
                    }
                }
                return _Instance;
            }
        }

        public bool ExisteNumeroOrdenPath(string pathBusqueda, string numeroOrden)
        {
            string buscarArchivoEnPath = System.IO.Path.Combine(pathBusqueda, "ODT " + numeroOrden + ".pdf");

            string usuario = ConfigurationManager.AppSettings["usuario"];
            string pass = ConfigurationManager.AppSettings["pass"];
            bool existeArchivo = false;
            using (new NetworkConnection(pathBusqueda, new NetworkCredential(usuario, pass)))
            {
                if (File.Exists(buscarArchivoEnPath))
                {
                    existeArchivo = true;
                }
                else
                {
                    existeArchivo = false;
                }
            }
            return existeArchivo;
        }

        public bool CrearPFDPorCuadrante(object proyectoSeleccionado, object cuadranteSeleccionado, bool esTrineo, string pathDestino, int tipoBusqueda, string path, string nombreArchivo, out string mensageError)
        {
            try
            {
                Proyecto proyecto = (Proyecto)proyectoSeleccionado;
                Cuadrante cuadrante = (Cuadrante)cuadranteSeleccionado;

                SplitPDF.Instance = null;
                SplitPDF.CantidadTravelers = 0;
                mensageError = "";

                if (proyecto == null || proyecto.ProyectoID <= 0)
                {
                    mensageError = "Selecciona un proyecto";
                    return false;
                }
                else if (tipoBusqueda == 1 && (cuadrante == null || cuadrante.CuadranteID <= 0))
                {
                    mensageError = "Selecciona un cuadrante";
                    return false;
                }
                else if (pathDestino == string.Empty)
                {
                    mensageError = "Selecciona una carpeta de destino";
                    return false;
                }
                else if (tipoBusqueda == 2 && path == "")
                {
                    mensageError = "Selecciona un archivo";
                    return false;
                }
                else if (tipoBusqueda == 0)
                {
                    mensageError = "Necesitas seleccionar el tipo de busqueda: por cuadrante o por csv";
                    return false;
                }
                DataTable dtSpools = new DataTable();
                int cantidadArchivosEncontrados = 0;

                if (tipoBusqueda == 1) //cuadrante
                {
                    dtSpools = ObtieneSpools(cuadrante.CuadranteID, proyecto.ProyectoID);
                    string mensajeNombreCsv;
                    SplitPDF.Instance.CrearObtenerCSV(pathDestino, cuadrante.NombreCuadrante, out mensajeNombreCsv);
                    for (int i = 0; i < dtSpools.Rows.Count; i++)
                    {
                        if ((ExisteNumeroOrdenPath(proyecto.pathRutaCompartida, dtSpools.Rows[i][0].ToString())))
                            cantidadArchivosEncontrados++;
                    }

                    if (cantidadArchivosEncontrados > 0 && dtSpools.Rows.Count > 0)
                    {
                        string mensajeCrearPDF = "";
                        if (SplitPDF.Instance.CrearObtenerPDF(pathDestino, cuadrante.NombreCuadrante, out mensajeCrearPDF))
                        {
                            foreach (DataRow item in dtSpools.Rows)
                            {
                                string ruta = System.IO.Path.Combine(proyecto.pathRutaCompartida, "ODT " + item[0].ToString() + ".pdf");
                                try
                                {
                                    if (ExisteNumeroOrdenPath(proyecto.pathRutaCompartida, item[0].ToString()))
                                    {
                                        int numeroPaginasPDF = SplitPDF.Instance.CantidadDePaginas(ruta) - int.Parse(item[2].ToString());

                                        try
                                        {
                                            SplitPDF.Instance.SplitAndSaveInterval(ruta, pathDestino, numeroPaginasPDF, 1, mensajeCrearPDF, item[0].ToString(), item[0].ToString() + "-" + item[1].ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), "Error desconocido " + ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), "No existe el archivo pdf en la carpeta compartida");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), ex.Message);
                                }
                            }
                            if (SplitPDF.Instance.AgregoPaginas)
                            {
                                SplitPDF.Instance.document.Close();
                                SplitPDF.Instance.CerrarDocumentoCreado(pathDestino + "\\" + mensajeCrearPDF.Trim() + ".pdf");
                            }
                            CrearArchivo.Instance.CerrarDocumento();
                            mensageError = mensajeCrearPDF + " con " + SplitPDF.CantidadTravelers.ToString();
                            return true;
                            // escribir que se cerro el documento.
                        }
                        else
                        {
                            mensageError = "No se pudo crear el archivo pdf " + mensajeCrearPDF;
                            return false;
                        }

                    }
                    else
                    {


                        mensageError = "No se generó archivo con travelers, revisar archivo " + mensajeNombreCsv + ".csv para mayor informaciòn";
                        for (int i = 0; i < dtSpools.Rows.Count; i++)
                        {
                            CrearArchivo.Instance.EscribirMensajeDocumento(dtSpools.Rows[i][0].ToString(), dtSpools.Rows[i][0].ToString() + "-" + dtSpools.Rows[i][1].ToString(), "no se encontro  el archivo pdf en la ruta de la carpeta compartida");
                        }
                        CrearArchivo.Instance.CerrarDocumento();
                        return false;
                    }
                }
                else if (tipoBusqueda == 2)//csv
                {
                    DataTable dt = ConvertToDataTable(path, 1);
                    if (dt.Rows.Count < 70)
                    {
                      
                        DataSet dataSet = DAONumeroOrden.Instance.ValidarNumerosControl(dt, proyecto.ProyectoID);

                        dtSpools = dataSet.Tables[0];
                        string mensajeCrearPDF = "";
                        string mensajeNombreCsv;
                        SplitPDF.Instance.CrearObtenerCSV(pathDestino, nombreArchivo.Split('.')[0], out mensajeNombreCsv);

                        for (int i = 0; i < dtSpools.Rows.Count; i++)
                        {
                            if ((ExisteNumeroOrdenPath(proyecto.pathRutaCompartida, dtSpools.Rows[i][0].ToString())))
                                cantidadArchivosEncontrados++;
                        }

                        if (dtSpools.Rows.Count > 0 && dtSpools.Rows.Count <= 70)
                        {
                            if (cantidadArchivosEncontrados > 0)
                            {
                                if (SplitPDF.Instance.CrearObtenerPDF(pathDestino, nombreArchivo.Split('.')[0], out mensajeCrearPDF))
                                {


                                    if (dtSpools.Rows.Count > 0 && dtSpools.Rows.Count <= 70)
                                    {


                                        if (dataSet.Tables[1].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                                            {
                                                CrearArchivo.Instance.EscribirMensajeDocumento("", dataSet.Tables[1].Rows[i][0].ToString(), "No pertenece al proyecto " + proyecto.NombreProyecto);
                                            }
                                        }

                                        foreach (DataRow item in dtSpools.Rows)
                                        {
                                            string ruta = System.IO.Path.Combine(proyecto.pathRutaCompartida, "ODT " + item[0].ToString() + ".pdf");
                                            try
                                            {
                                                if (ExisteNumeroOrdenPath(proyecto.pathRutaCompartida, item[0].ToString()))
                                                {
                                                    int numeroPaginasPDF = SplitPDF.Instance.CantidadDePaginas(ruta) - int.Parse(item[2].ToString());

                                                    try
                                                    {
                                                        SplitPDF.Instance.SplitAndSaveInterval(ruta, pathDestino, numeroPaginasPDF, 1, mensajeCrearPDF, item[0].ToString(), item[0].ToString() + "-" + item[1].ToString());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), "Error desconocido " + ex.Message);
                                                    }
                                                }
                                                else
                                                {
                                                    CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), "No existe el archivo pdf en la carpeta compartida");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), ex.Message);
                                            }
                                        }
                                        if (SplitPDF.Instance.AgregoPaginas)
                                        {
                                            SplitPDF.Instance.document.Close();
                                            SplitPDF.Instance.CerrarDocumentoCreado(pathDestino + "\\" + mensajeCrearPDF.Trim() + ".pdf");
                                        }
                                        CrearArchivo.Instance.CerrarDocumento();
                                        mensageError = mensageError = mensajeCrearPDF + " con " + SplitPDF.CantidadTravelers.ToString();
                                        return true;
                                        // escribir que se cerro el documento.


                                    }
                                    else
                                    {

                                        if (dtSpools.Rows.Count > 70)
                                            CrearArchivo.Instance.EscribirMensajeDocumento("", "", "La cantidad de numeros de control que coinciden son mas de 70");

                                        for (int i = 0; i < dtSpools.Rows.Count; i++)
                                        {
                                            CrearArchivo.Instance.EscribirMensajeDocumento("", dtSpools.Rows[i][0].ToString() + "" + dtSpools.Rows[i][1].ToString(), "coincide con el proyecto " + proyecto.NombreProyecto);
                                        }



                                        if (dataSet.Tables[1].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                                            {
                                                CrearArchivo.Instance.EscribirMensajeDocumento("", dataSet.Tables[1].Rows[i][0].ToString(), "No pertenece al proyecto " + proyecto.NombreProyecto);
                                            }
                                        }

                                        if (SplitPDF.Instance.AgregoPaginas)
                                        {
                                            SplitPDF.Instance.document.Close();
                                            SplitPDF.Instance.CerrarDocumentoCreado(pathDestino + "\\" + mensajeCrearPDF.Trim() + ".pdf");
                                        }
                                        CrearArchivo.Instance.CerrarDocumento();

                                        mensageError = SplitPDF.CantidadTravelers.ToString();
                                        return false;

                                    }


                                }
                                else
                                {
                                    mensageError = "No se pudo crear el archivo pdf " + mensajeCrearPDF;
                                    return false;
                                }
                            }
                            else
                            {
                                mensageError = "No se generó archivo con travelers, revisar archivo " + mensajeNombreCsv + ".csv para mayor informaciòn";
                                for (int i = 0; i < dtSpools.Rows.Count; i++)
                                {
                                    CrearArchivo.Instance.EscribirMensajeDocumento(dtSpools.Rows[i][0].ToString(), dtSpools.Rows[i][0].ToString() + "-" + dtSpools.Rows[i][1].ToString(), "no se encontro  el archivo pdf en la ruta de la carpeta compartida");
                                }
                                CrearArchivo.Instance.CerrarDocumento();
                                return false;
                            }
                        }
                        else
                        {
                            mensageError = "No se generó archivo con travelers, revisar archivo " + mensajeNombreCsv + ".csv para mayor informaciòn";
                            //for (int i = 0; i < dtSpools.Rows.Count; i++)
                            //{
                            //    CrearArchivo.Instance.EscribirMensajeDocumento(dtSpools.Rows[i][0].ToString(), dtSpools.Rows[i][0].ToString() + "-" + dtSpools.Rows[i][1].ToString(), " maximo tienen que ser 70 numeros de control en el archivo");
                            //}
                            if (dtSpools.Rows.Count > 70)
                            {
                                mensageError = "Como máximo tienen que ser 70 numeros de control para iniciar con el proceso.";
                            }
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                                {
                                    CrearArchivo.Instance.EscribirMensajeDocumento("", dataSet.Tables[1].Rows[i][0].ToString(), "No pertenece al proyecto " + proyecto.NombreProyecto);
                                }
                            }
                            CrearArchivo.Instance.CerrarDocumento();
                            return false;
                        }
                    }
                    else
                    {
                        mensageError = " El número máximo permitido es 70 ";
                        return false;
                    }



                }
                else
                {
                    mensageError = "No se selecciono un tipo de busqueda";
                    return false;
                }




            }
            catch (Exception ex)
            {
                mensageError = ex.Message;
                return false;
            }

        }

        private DataTable ObtieneSpools(int cuadranteID, int proyectoID)
        {
            ObjetosSQL _SQL = new ObjetosSQL();
            string[,] parametro = { { "@CuadranteID", cuadranteID.ToString() }, { "@ProyectoID", proyectoID.ToString() } };

            return _SQL.EjecutaDataAdapter(Stords.PDF_SP_GetSpoolCuadrantes, parametro);
        }

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {

                var cols = line.Split(',');

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);

            }

            return tbl;
        }

        public DataSet ValidarNumerosControl(DataTable dt, int proyecto)
        {
            ObjetosSQL _SQL = new ObjetosSQL();
            string[,] parametro = { { "@proyectoID", proyecto.ToString() } };

            return _SQL.Coleccion(Stords.PDF_SP_GetNumeroControlProyecto, dt, "@NumeroControl", parametro);
        }
    }
}
