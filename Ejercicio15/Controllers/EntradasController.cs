using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ejercicio15;
using Ejercicio15.Models;
using Ejercicio15.Repository;
using Ejercicio15.Servicios;

namespace Ejercicio15.Controllers
{
    public class EntradasController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();//Eliminarlo y pasarlo a la capa de Repository
        private IEntradasService entradasService;

        public EntradasController(IEntradasService _entradasService)
        {
            this.entradasService = _entradasService;
        }

        // GET: api/Entradas
        public IQueryable<Entrada> GetEntradas()
        {
            return entradasService.GetEntradas();
            //return db.Entradas;
        }

        // GET: api/Entradas/5
        [ResponseType(typeof(Entrada))]
        public IHttpActionResult GetEntrada(long id)
        {
            Entrada entrada = entradasService.Get(id);
            //Entrada entrada = db.Entradas.Find(id);
            if (entrada == null)
            {
                return NotFound();
            }

            return Ok(entrada);
        }

        // PUT: api/Entradas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntrada(long id, Entrada entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entrada.Id)
            {
                return BadRequest();
            }
            
            try
            {
                entradasService.Put(entrada);
            }
            catch (NoEncontradoException)
            {                
                    return NotFound();               
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Entradas
        [ResponseType(typeof(Entrada))]
        public IHttpActionResult PostEntrada(Entrada entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //IEntradasRepository entradasRepository = new EntradasRepository();//Ya no hace falta es esta en el constructor
            //IEntradasService entradasService = new EntradasService(entradasRepository);//Ya no hace falta es esta en el constructor el SERVICE con el Unity ya todo¿?

            entrada = entradasService.Create(entrada);

            return CreatedAtRoute("DefaultApi", new { id = entrada.Id }, entrada);
        }

        // DELETE: api/Entradas/5
        [ResponseType(typeof(Entrada))]
        public IHttpActionResult DeleteEntrada(long id)
        {
            Entrada entrada;
            try
            {
                entrada = entradasService.Delete(id);
            } catch (NoEncontradoException)
            {
                return NotFound();
            }

            return Ok(entrada);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EntradaExists(long id)
        //{
        //    return db.Entradas.Count(e => e.Id == id) > 0;
        //}
    }
}