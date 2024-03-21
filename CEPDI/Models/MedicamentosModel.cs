using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEPDI.Models
{
    public class MedicamentosModel:SuperModel
    {
        [Key]
        [Description("Primary")]
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public string Concentracion { get; set; }
        public int IdFormaFarmaceutica { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Presentacion { get; set; }
        public int bhabilitado { get; set; }
        [Description("ignore")]
        public string FormaFarmaceutica { get; set; }


    }
}