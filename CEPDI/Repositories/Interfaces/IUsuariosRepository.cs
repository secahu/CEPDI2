using CEPDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.Repositories.Interfaces
{
    public interface IUsuariosRepository:IGenericRepository<UsuariosModel>
    {
        Task<UsuariosModel> getByFiltros(UsuariosModel filtro);

    }
}
