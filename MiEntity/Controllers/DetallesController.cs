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
    public class DetallesController : ApiController
    {
        private FacturaDBEntities db = new FacturaDBEntities();

        // GET: api/Detalles
        public IQueryable<Detalle> GetDetalle()
        {
            return db.Detalle;
        }

        // GET: api/Detalles/5
        [ResponseType(typeof(Detalle))]
        public IHttpActionResult GetDetalle(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return NotFound();
            }

            return Ok(detalle);
        }

        // PUT: api/Detalles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalle(int id, Detalle detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalle.IdDetalle)
            {
                return BadRequest();
            }

            db.Entry(detalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleExists(id))
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

        // POST: api/Detalles
        [ResponseType(typeof(Detalle))]
        public IHttpActionResult PostDetalle(Detalle detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Detalle.Add(detalle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalle.IdDetalle }, detalle);
        }

        // DELETE: api/Detalles/5
        [ResponseType(typeof(Detalle))]
        public IHttpActionResult DeleteDetalle(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return NotFound();
            }

            db.Detalle.Remove(detalle);
            db.SaveChanges();

            return Ok(detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleExists(int id)
        {
            return db.Detalle.Count(e => e.IdDetalle == id) > 0;
        }
    }
}