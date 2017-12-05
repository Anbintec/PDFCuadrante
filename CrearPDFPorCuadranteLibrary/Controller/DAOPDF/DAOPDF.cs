using ConnectorLibrary.Constantes;
using ConnectorLibrary.Sql;
using CrearPDFPorCuadranteLibrary.Model.Cuadrantes;
using CrearPDFPorCuadranteLibrary.Model.Proyectos;
using CrearPDFPorCuadranteLibrary.Model.TransactionalInformation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearPDFPorCuadranteLibrary.Controller.DAOPDF
{
    public class DAOPDF
    {
        private static readonly object _mutex = new object();
        private static DAOPDF _Instance;
        public static DAOPDF Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_Instance == null)
                    {
                        _Instance = new DAOPDF();
                    }
                }
                return _Instance;
            }
        }

        public List<Proyecto> ObtieneProyectos()
        {
            try
            {

                ObjetosSQL _SQL = new ObjetosSQL();

                DataTable dtProyectos = _SQL.EjecutaDataAdapter(Stords.PDF_SP_GetProyectos);

                List<Proyecto> listaProyectos = new List<Proyecto>();
                if (dtProyectos.Rows.Count > 0)
                    listaProyectos.Add(new Proyecto());

                for (int i = 0; i < dtProyectos.Rows.Count; i++)
                {
                    listaProyectos.Add(new Proyecto
                    {
                        ProyectoID = int.Parse(dtProyectos.Rows[i][0].ToString()),
                        NombreProyecto = dtProyectos.Rows[i][1].ToString(),
                        pathRutaCompartida = dtProyectos.Rows[i][2].ToString()
                    });
                }

                return listaProyectos;

            }
            catch (Exception ex)
            {
                return new List<Proyecto>();
            }
        }

        public List<Cuadrante> ObtieneCuadrantes(int proyectoID , int estrineo)
        {
            try
            {

                ObjetosSQL _SQL = new ObjetosSQL();
                string[,] parametro = { { "@ProyectoID", proyectoID.ToString() }, { "@Estrineo", estrineo.ToString() } };

                DataTable dtCuadrantes = _SQL.EjecutaDataAdapter(Stords.PDF_SP_GetCuadrantes, parametro);

                List<Cuadrante> listaCuadrantes = new List<Cuadrante>();
                if (dtCuadrantes.Rows.Count > 0)
                    listaCuadrantes.Add(new Cuadrante());

                for (int i = 0; i < dtCuadrantes.Rows.Count; i++)
                {
                    listaCuadrantes.Add(new Cuadrante
                    {
                        CuadranteID = int.Parse(dtCuadrantes.Rows[i][0].ToString()),
                        NombreCuadrante = dtCuadrantes.Rows[i][1].ToString()
                        
                    });
                }

                return listaCuadrantes;

            }
            catch (Exception ex)
            {
                return new List<Cuadrante>();
            }
        }

        


    }
}
