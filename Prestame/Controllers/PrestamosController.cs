using System.Web.Http;
using System.Web.Http.Description;
using Prestame.Interfaces;
using Prestame.Repositories;
using Prestame.ViewModel;

namespace Prestame.Controllers
{
    public class PrestamosController : ApiController
    {
        private IPrestamosRepository<PrestamoViewModel> _prestamosDB;

        public PrestamosController()
        {
            _prestamosDB = new PrestamoRepository();
        }

        public PrestamosController(IPrestamosRepository<PrestamoViewModel> repository)
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

        // GET: api/Prestamos/5
        [ResponseType(typeof(JsonResponse))]
        public IHttpActionResult GetById(int id)
        {
            var json = _prestamosDB.Get(id);
            return Ok(json);
        }

        // POST: api/Prestamos
        [ResponseType(typeof(JsonResponse))]
        [HttpPost]
        public IHttpActionResult Post(PrestamoViewModel prestamo)
        {
            var json = _prestamosDB.Save(prestamo, ModelState);
            return Ok(json);
        }

        // PUT: api/Prestamos
        [ResponseType(typeof(JsonResponse))]
        [HttpPut]
        public IHttpActionResult Put(int id, PrestamoEstado estado)
        {
            var json = _prestamosDB.Update(id, estado);
            return Ok(json);
        }

    }
}