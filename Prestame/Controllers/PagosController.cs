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
using Prestame.Interfaces;
using Prestame.Models;
using Prestame.ViewModel;

namespace Prestame.Controllers
{
    public class PagosController : ApiController
    {
        private IPagosRepository<PagosViewModel> _dbPagos;

        // GET: api/Pagos
        public JsonResponse GetPagos()
        {
            var json =_dbPagos.Get();
            return json;
        }

        // GET: api/Pagos/5
        [ResponseType(typeof(Pagos))]
        public JsonResponse GetPagos(int id)
        {
            var json = _dbPagos.Get(id);
            return json;
        }

      /* 
        // POST: api/Pagos
        [ResponseType(typeof(Pagos))]
        public IHttpActionResult PostPagos(Pagos pagos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pagos.Add(pagos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pagos.Id }, pagos);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbPagos.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagosExists(int id)
        {
            return db.Pagos.Count(e => e.Id == id) > 0;
        }*/
    }
}