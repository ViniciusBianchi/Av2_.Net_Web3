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
    public class ClientesController : ApiController
    {
        private LocadoraServiceContext db = new LocadoraServiceContext();

        // GET api/Clientes
        public IQueryable<ClienteDTO> GetClientes()
        {
            var clientes = from c in db.Clientes
                        select new ClienteDTO()
                        {
                            Id = c.Id,
                            Nome = c.Nome,
                            NomeDoFilme = c.Filme.Nome
                        };

            return clientes;
        }

        // GET api/Clientes/5
        [ResponseType(typeof(ClienteDetailDTO))]
        public async Task<IHttpActionResult> GetCliente(int id)
        {
            var cliente = await db.Clientes.Include(c => c.Filme).Select(c =>
                new ClienteDetailDTO()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    FilmeId = c.Filme.Id,
                    NomeDoFilme = c.Filme.Nome
                }).SingleOrDefaultAsync(c => c.Id == id);
                    if (cliente == null)
                    {
                        return NotFound();
                    }

                    return Ok(cliente);
                }

        // PUT api/Clientes/5
        public async Task<IHttpActionResult> PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST api/Clientes
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();

            //db.Entry(cliente).Reference(x => x.Filme).Load();

            var dto = new FilmeDTO()
            {
                Id = cliente.Filme.Id,
                Nome = cliente.Filme.Nome,
                Categoria = cliente.Filme.Categoria
            };

            return CreatedAtRoute("DefaultApi", new { id = cliente.Id }, dto);
        }

        // DELETE api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Clientes.Count(e => e.Id == id) > 0;
        }
    }
}