using CEPDI.Models;
using CEPDI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CEPDI.Controllers.Api
{
    public class MedicamentosApiController : GenericController<MedicamentosModel>
    {
        private readonly IMedicamentosRepository _repository;

        public MedicamentosApiController(IMedicamentosRepository repository) : base(repository)
        {
            _repository = repository;
        }


    }
}
