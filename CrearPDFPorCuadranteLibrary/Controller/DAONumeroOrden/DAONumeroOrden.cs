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

        public bool CrearPFDPorCuadrante(object proyectoSeleccionado, object cuadranteSeleccionado, bool esTrineo, string pathDestino, out string mensageError)
        {
            try
            {
                Proyecto proyecto = (Proyecto)proyectoSeleccionado;
                Cuadrante cuadrante = (Cuadrante)cuadranteSeleccionado;
                
                SplitPDF.Instance = null;
                SplitPDF.CantidadTravelers = 0;
                mensageError = "";

                if (proyecto.ProyectoID <= 0)
                {
                    mensageError = "Selecciona un proyecto porfavor";
                    return false;
                }
                else if (cuadrante.CuadranteID <= 0)
                {
                    mensageError = "Selecciona un cuadrante porfavor";
                    return false;
                }
                else if (pathDestino == string.Empty)
                {
                    mensageError = "Selecciona una carpeta de destino porfavor";
                    return false;
                }

                DataTable dtSpools = ObtieneSpools(cuadrante.CuadranteID, proyecto.ProyectoID);
                if (dtSpools.Rows.Count > 0)
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
                                        CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(),  item[0].ToString() + "-" + item[1].ToString() , "Error desconocido " + ex.Message);
                                    }
                                }
                                else
                                {
                                    CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString(), "No existe el archivo pdf en la carpeta compartida");
                                }
                            }
                            catch (Exception ex)
                            {
                                CrearArchivo.Instance.EscribirMensajeDocumento(item[0].ToString(), item[0].ToString() + "-" + item[1].ToString() , ex.Message);
                            }
                        }
                        if (SplitPDF.Instance.AgregoPaginas)
                        {
                            SplitPDF.Instance.document.Close();
                            SplitPDF.Instance.CerrarDocumentoCreado(pathDestino + "\\" + mensajeCrearPDF.Trim() + ".pdf");
                        }
                        CrearArchivo.Instance.CerrarDocumento();
                        mensageError = SplitPDF.CantidadTravelers.ToString();
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
                    mensageError = "No existen spools en el cuadrante, por lo tanto no se creara ningun archivo pdf ";
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
    }
}
