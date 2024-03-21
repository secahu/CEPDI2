using CEPDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.Repositories.Interfaces
{
    public interface IMedicamentosRepository:IGenericRepository<MedicamentosModel>
    {
        Task<MedicamentosModel> getByFiltros(MedicamentosModel filtro);

    }
}
