using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEPDI.Models
{
    public class UsuariosModel:SuperModel
    {
        [Key]
        [Description("Primary")]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion{ get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int IdPerfil { get; set; }
        public int Estatus { get; set; }

        public UsuariosModel() {
            FechaCreacion = DateTime.Now;
        }
    }
}