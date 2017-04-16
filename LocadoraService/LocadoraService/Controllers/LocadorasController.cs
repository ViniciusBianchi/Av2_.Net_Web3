using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LocadoraService.Models;

namespace LocadoraService.Controllers
{
    public class LocadorasController : ApiController
    {
        private LocadoraServiceContext db = new LocadoraServiceContext();

        // GET api/Locadoras
        public IQueryable<LocadoraDTO> GetLocadoras()
        {
            var locadora = from l in db.Locadoras
                        select new LocadoraDTO()
                        {
                            Id = l.Id,
                            Nome = l.Nome,
                            Endereco = l.Endereco
                        };

            return locadora;
        }

        // GET api/Locadoras/5
        [ResponseType(typeof(LocadoraDetailDTO))]
        public async Task<IHttpActionResult> GetLocadora(int id)
        {
            var locadora = await db.Locadoras.Include(l => l.Filmes).Select(l =>
                new LocadoraDetailDTO()
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    Endereco = l.Endereco,
                    Filmes = l.Filmes,
                    
                }).SingleOrDefaultAsync(l => l.Id == id);
                    if (locadora == null)
                    {
                        return NotFound();
                    }

                    return Ok(locadora);
                }

        // PUT api/Locadoras/5
        public async Task<IHttpActionResult> PutLocadora(int id, Locadora locadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locadora.Id)
            {
                return BadRequest();
            }

            db.Entry(locadora).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocadoraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Locadoras
        [ResponseType(typeof(Locadora))]
        public async Task<IHttpActionResult> PostLocadora(Locadora locadora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locadoras.Add(locadora);
            await db.SaveChangesAsync();

            //db.Entry(locadora).Reference(x => x.Filmes).Load();

            var dto = new LocadoraDTO()
            {
                Id = locadora.Id,
                Nome = locadora.Nome,
                Endereco = locadora.Endereco
            };


            return CreatedAtRoute("DefaultApi", new { id = locadora.Id }, dto);
        }

        // DELETE api/Locadoras/5
        [ResponseType(typeof(Locadora))]
        public async Task<IHttpActionResult> DeleteLocadora(int id)
        {
            Locadora locadora = await db.Locadoras.FindAsync(id);
            if (locadora == null)
            {
                return NotFound();
            }

            db.Locadoras.Remove(locadora);
            await db.SaveChangesAsync();

            return Ok(locadora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocadoraExists(int id)
        {
            return db.Locadoras.Count(e => e.Id == id) > 0;
        }
    }
}