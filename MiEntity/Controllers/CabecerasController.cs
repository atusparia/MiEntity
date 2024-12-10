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
using MiEntity.Models;

namespace MiEntity.Controllers
{
    public class CabecerasController : ApiController
    {
        private FacturaDBEntities context = new FacturaDBEntities();

        // GET: api/Cabeceras
        public IQueryable<Cabecera> GetCabecera(string cliente)
        {
            //Linq=>Expresiones Lambda
            var cabeceras = context.Cabecera.Where(x=>x.Activo==true && x.Cliente.Contains(cliente));

            //return context.Cabecera;
            return cabeceras;
        }

        // GET: api/Cabeceras/5
        [ResponseType(typeof(Cabecera))]
        public IHttpActionResult GetCabecera(int id)
        {
            Cabecera cabecera = context.Cabecera.Find(id);
            if (cabecera == null)
            {
                return NotFound();
            }

            return Ok(cabecera);
        }

        // PUT: api/Cabeceras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCabecera(int id, Cabecera cabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cabecera.IdCabecera)
            {
                return BadRequest();
            }

            context.Entry(cabecera).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabeceraExists(id))
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

        // POST: api/Cabeceras
        [ResponseType(typeof(Cabecera))]
        public IHttpActionResult PostCabecera(Cabecera cabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Cabecera.Add(cabecera);
            context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cabecera.IdCabecera }, cabecera);
        }

        // DELETE: api/Cabeceras/5
        [ResponseType(typeof(Cabecera))]
        public IHttpActionResult DeleteCabecera(int id)
        {
            Cabecera cabecera = context.Cabecera.Find(id);
            if (cabecera == null)
            {
                return NotFound();
            }

            context.Entry(cabecera).State = EntityState.Modified;

            cabecera.Activo = false;

            //context.Cabecera.Remove(cabecera);
            context.SaveChanges();

            return Ok(cabecera);
        }



        //public IHttpActionResult DeleteCabecera(int id)
        //{
        //    Cabecera cabecera = context.Cabecera.Find(id);
        //    if (cabecera == null)
        //    {
        //        return NotFound();
        //    }

        //    context.Cabecera.Remove(cabecera);
        //    context.SaveChanges();

        //    return Ok(cabecera);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CabeceraExists(int id)
        {
            return context.Cabecera.Count(e => e.IdCabecera == id) > 0;
        }
    }
}