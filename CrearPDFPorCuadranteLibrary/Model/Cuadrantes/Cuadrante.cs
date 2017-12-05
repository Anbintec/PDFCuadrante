using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearPDFPorCuadranteLibrary.Model.Cuadrantes
{
    public class Cuadrante
    {
        public int CuadranteID { get; set; }
        public string NombreCuadrante { get; set; }

        public Cuadrante()
        {
            CuadranteID = 0;
            NombreCuadrante = "--Selecciona un cuadrante--";
        }
    }
}
