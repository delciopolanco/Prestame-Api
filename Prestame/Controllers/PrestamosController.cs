using System.Web.Http;
using System.Web.Http.Description;
using Prestame.Data;
using Prestame.Models;
using Prestame.Repositories;
using Prestame.ViewModel;

namespace Prestame.Controllers
{
    public class PrestamosController : ApiController
    {
        private IDataRepository<PrestamoViewModel> _prestamosDB;

        public PrestamosController()
        {
            _prestamosDB = new PrestamoRepository();
        }

        public PrestamosController(IDataRepository<PrestamoViewModel> repository)
        {
            _prestamosDB = repository;
        }

        // GET: api/Prestamos
        [ResponseType(typeof(JsonResponse))]
        public IHttpActionResult Get()
        {
            var json = _prestamosDB.Get();
            return Ok(json);
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(JsonResponse))]
        public IHttpActionResult GetById(int id)
        {
            var json = _prestamosDB.Get(id);
            return Ok(json);
        }

        // POST: api/Clientes
        [ResponseType(typeof(JsonResponse))]
        [HttpPost]
        public IHttpActionResult Post(PrestamoViewModel prestamo)
        {
            var json = _prestamosDB.Save(prestamo, ModelState);
            return Ok(json);
        }

    }
}