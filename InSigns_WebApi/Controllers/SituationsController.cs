using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using InSigns_WebApi.Models;

namespace InSigns_WebApi.Controllers
{
    [RoutePrefix("api/situations")]
    public class SituationsController : ApiController
    {
        InSignsEntities db = new InSignsEntities();
        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory());

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetSituations()
        {
            var situationsList = TheModelFactory.Create(new List<situation>(db.situations));
            if (situationsList == null)
            {
                return NotFound();
            }
            return Ok(situationsList);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PostSitution(situation situation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.situations.Add(situation);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult(this);
            }
            return Ok();
        }
    }
}
