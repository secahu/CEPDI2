using CEPDI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CEPDI.Controllers.Api
{
    public class GenericController<U> : ApiController where U : class
    {
        public readonly IGenericRepository<U> _genericRepository;
        public GenericController(IGenericRepository<U> repository)
        {
            _genericRepository = repository;
        }

        // GET: api/[controller]
        public virtual async Task<IEnumerable<U>> Get()
        {
            try
            {
                return await _genericRepository.GetAll();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public virtual async Task<U> Get(int id)
        {
            try
            {
                return await _genericRepository.GetById(id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        [System.Web.Http.HttpPost]
        public virtual async Task<ActionResult<bool>> Post([System.Web.Http.FromBody] U model)
        {
            try
            {
                return await _genericRepository.Create(model);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [System.Web.Http.HttpPost]
        public virtual async Task<ActionResult<long>> PostId([System.Web.Http.FromBody] U model)
        {
            try
            {
                return await _genericRepository.CreateId(model);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // PUT api/[controller]/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public virtual async Task<ActionResult<bool>> Put(int id, [System.Web.Http.FromBody] U model)
        {
            try
            {
                return await _genericRepository.Update(id, model);
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        // DELETE api/[controller]/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public virtual async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                return await _genericRepository.Delete(id);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
