using System.Web.Http;
using System.Web.Http.Description;
using Prestame.Interfaces;
using Prestame.Repositories;
using Prestame.Models;

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
        [ActionName("DefaultAction")]
        public IHttpActionResult Get()
        {
            var json = _prestamosDB.Get();
            return Ok(json);
        }

        // GET: api/Prestamos/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult GetById(int id)
        {
            var json = _prestamosDB.Get(id);
            return Ok(json);
        }

        // GET: api/Pagos/GetPrestamosByClient/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("GetPrestamosByClient")]
        public JsonResponse GetPrestamosByClient(int id)
        {
            var json = _prestamosDB.GetPrestamosByClient(id);
            return json;
        }

        // POST: api/Prestamos
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        [HttpPost]
        public IHttpActionResult Post(PrestamoViewModel prestamo)
        {
            var json = _prestamosDB.Save(prestamo, ModelState);
            return Ok(json);
        }

        // PUT: api/Prestamos
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult Put(int id, PrestamosEstadosViewModel estado)
        {
            var json = _prestamosDB.Update(id, estado);
            return Ok(json);
        }

    }
}