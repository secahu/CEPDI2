using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEPDI.Models
{
    public class FormasFarmaceuticasModel:SuperModel
    {
        [Key]
        [Description("Primary")]
        public int IdFormaFarmaceutica{ get; set; }
        public string Nombre { get; set; }
        public int Habilitado { get; set; }
    }
}