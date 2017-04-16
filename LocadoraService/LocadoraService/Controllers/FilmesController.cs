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
    public class FilmesController : ApiController
    {
        private LocadoraServiceContext db = new LocadoraServiceContext();

        // GET api/Filmes
        public IQueryable<FilmeDTO> GetFilmes()
        {
            var filmes = from f in db.Filmes
                        select new FilmeDTO()
                        {
                            Id = f.Id,
                            Nome = f.Nome,
                            Categoria = f.Categoria
                        };

            return filmes;
        }

        // GET api/Filmes/5
        [ResponseType(typeof(FilmeDetailDTO))]
        public async Task<IHttpActionResult> GetFilme(int id)
        {
            var filme = await db.Filmes.Select(f =>
        new FilmeDetailDTO()
        {
            Id = f.Id,
            Nome = f.Nome,
            Categoria = f.Categoria,
            FaixaEtaria = f.FaixaEtaria,
            Preco = f.Preco
        }).SingleOrDefaultAsync(f => f.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return Ok(filme);

        }

        // PUT api/Filmes/5
        public async Task<IHttpActionResult> PutFilme(int id, Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filme.Id)
            {
                return BadRequest();
            }

            db.Entry(filme).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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

        // POST api/Filmes
        [ResponseType(typeof(Filme))]
        public async Task<IHttpActionResult> PostFilme(Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filmes.Add(filme);
            await db.SaveChangesAsync();

            var dto = new FilmeDTO()
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Categoria = filme.Categoria
            };

            return CreatedAtRoute("DefaultApi", new { id = filme.Id }, dto);
        }

        // DELETE api/Filmes/5
        [ResponseType(typeof(Filme))]
        public async Task<IHttpActionResult> DeleteFilme(int id)
        {
            Filme filme = await db.Filmes.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            db.Filmes.Remove(filme);
            await db.SaveChangesAsync();

            return Ok(filme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmeExists(int id)
        {
            return db.Filmes.Count(e => e.Id == id) > 0;
        }
    }
}