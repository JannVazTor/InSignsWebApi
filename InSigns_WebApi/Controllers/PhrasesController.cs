using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using InSigns_WebApi.Models;

namespace InSigns_WebApi.Controllers
{
    [RoutePrefix("api/phrases")]
    public class PhrasesController : ApiController
    {
        InSignsEntities db = new InSignsEntities();
        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory());

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPhrases()
        {
            var phrasesList = TheModelFactory.Create(new List<phrase>(db.phrases));
            if (phrasesList == null)
            {
                return NotFound();
            }
            return Ok(phrasesList);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PostPhrase(phrase phrase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.phrases.Add(phrase);
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
