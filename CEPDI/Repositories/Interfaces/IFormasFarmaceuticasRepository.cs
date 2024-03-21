using CEPDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.Repositories.Interfaces
{
    public interface IFormasFarmaceuticasRepository:IGenericRepository<FormasFarmaceuticasModel>
    {
        Task<FormasFarmaceuticasModel> getByFiltros(FormasFarmaceuticasModel filtro);

    }
}
