using CEPDI.Models;
using CEPDI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CEPDI.Controllers.Api
{
    public class UsuariosApiController : GenericController<UsuariosModel>
    {
        private readonly IUsuariosRepository _repository;

        public UsuariosApiController(IUsuariosRepository repository) : base(repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<UsuariosModel> Login([FromBody] UsuariosModel user)
        {
            //HttpContext.Current.Session.Keys.Get

            UsuariosModel filtro = new UsuariosModel();
            filtro.Usuario = user.Usuario;

            UsuariosModel usuario = await _repository.getByFiltros(filtro);

            if (usuario == null) {

                usuario = new UsuariosModel();
                usuario.Mensaje = "Usuario no registrado";

            }
            else if (usuario != null && usuario.Password.Equals(user.Password))
            {

                if (usuario.Estatus==0)
                {

                    usuario = new UsuariosModel();
                    usuario.Mensaje = "Usuario deshabilitado";

                }
                else
                {

                    usuario.Password = "";
                    
                    HttpContext.Current.Session["IdUsuario"] = usuario.IdUsuario;
                    HttpContext.Current.Session["IdPerfil"] = usuario.IdPerfil;
                    HttpContext.Current.Session["Nombre"] = usuario.Nombre;
                    HttpContext.Current.Session["Usuario"] = usuario.Usuario;

                }
            }
            else
            {

                usuario = new UsuariosModel();
                usuario.Mensaje = "Password incorrecto";

            }

            return usuario;
        }
        
    }
}
