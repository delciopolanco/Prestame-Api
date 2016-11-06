using System.Web.Http;
using System.Web.Http.Description;
using Prestame.Data;
using Prestame.Interfaces;

namespace Prestame.Controllers.Clientes
{
    ///<Summary>
    /// Client Controller: Genera las llamadas apis que manejan los clientes
    ///</Summary>
    public class ClientesController : ApiController
    {

        private IClientesRepository<ClienteViewModel> _clientDB;

        public ClientesController()
        {
            _clientDB = new ClienteRepository();
        }

        public ClientesController(IClientesRepository<ClienteViewModel> repository)
        {
            _clientDB = repository;
        }

        // GET: api/Clientes
        /// <summary>
        /// Obtiene todos los clientes registrados
        /// </summary>
        /// <returns>JsonResponse</returns>
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult Get()
        {
            return Ok(_clientDB.Get());
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult GetById(int id)
        {
            var json = _clientDB.Get(id);
            return Ok(json);
        }

        // POST: api/Clientes
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        [HttpPost]
        public IHttpActionResult Post(ClienteViewModel cliente)
        {
            var json = _clientDB.Save(cliente, ModelState);
            return Ok(json);
        }


        // PUT: api/Clientes/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult Put(int id, ClienteViewModel cliente)
        {

            var json = _clientDB.Update(id, cliente, ModelState);
            return Ok(json);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public IHttpActionResult Delete(int id)
        {
           var json = _clientDB.Delete(id);
            return Ok(json);
        }
    }
}