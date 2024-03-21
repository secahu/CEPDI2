using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEPDI.Models
{
    public class SuperModel
    {
        public string Mensaje { get; set; }
        public SuperModel()
        {
            Mensaje = string.Empty;
        }
    }
}