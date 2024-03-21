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
    public class FormasFarmaceuticasApiController : GenericController<FormasFarmaceuticasModel>
    {
        private readonly IFormasFarmaceuticasRepository _repository;

        public FormasFarmaceuticasApiController(IFormasFarmaceuticasRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
