using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearPDFPorCuadranteLibrary.Model.Proyectos
{
    public class Proyecto
    {
        public int ProyectoID { get; set; }
        public string NombreProyecto { get; set; }
        public string pathRutaCompartida { get; set; }

        public Proyecto()
        {
            ProyectoID = 0;
            NombreProyecto = "--Selecciona un proyecto--";
            pathRutaCompartida = "";
        }
    }
}
