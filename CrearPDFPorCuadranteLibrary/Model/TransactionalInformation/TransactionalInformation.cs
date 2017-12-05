using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearPDFPorCuadranteLibrary.Model.TransactionalInformation
{
    public class TransactionalInformation
    {
        public bool ReturnStatus { get; set; }
        public int ReturnCode { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable ValidationErrors;
        public int TotalPages;
        public int TotalRows;
        public int PageSize;
        public Boolean IsAuthenicated;
        public int PerfilID { get; set; }

        public TransactionalInformation()
        {
            ReturnMessage = new List<String>();
            ValidationErrors = new Hashtable();
            IsAuthenicated = false;
        }
    }
}
