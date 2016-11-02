using System;
using System.Linq;
using Prestame.Interfaces;
using System.Data.Entity;
using Prestame.ViewModel;
using Prestame.Data;
using System.Web.Http.ModelBinding;
using Prestame.Utils;
using System.Web.Http;

namespace Prestame.Repositories
{
    public class PrestamoRepository : IPrestamosRepository<PrestamoViewModel>, IDisposable
    {
        private PrestameContext _db;
        private JsonResponse json = new JsonResponse();

        public PrestamoRepository(PrestameContext db)
        {
            _db = db;
        }

        public PrestamoRepository()
        {
            _db = new PrestameContext();
        }

        public JsonResponse Get()
        {
            try
            {
                var _prestamos = (from cliente in _db.Cliente
                                  from prestamos in _db.Prestamos
                                  where cliente.Id == prestamos.ClienteId
                                  select new PrestamoViewModel()
                                  {
                                      Cliente = new ClienteViewModel()
                                      {
                                          Id = cliente.Id,
                                          Nombres = cliente.Nombres,
                                          Apellidos = cliente.Apellidos,
                                          Identificacion = cliente.Identificacion
                                      },
                                      Id = prestamos.Id,
                                      CapitalActual = prestamos.CapitalActual,
                                      CapitalInicial = prestamos.CapitalInicial,
                                      InteresActual = prestamos.InteresActual,
                                      InteresInicial = prestamos.InteresInicial,
                                      FechaDeCreacion = prestamos.FechaDeCreacion,
                                      FechaDeSaldo = prestamos.FechaDeSaldo,
                                      Estado = prestamos.Estado
                                  }).ToList();

                if (_prestamos != null)
                {
                    json.setMessage(_prestamos, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(_prestamos, JsonResponse.MessageType.Error, "No existen clientes registrados.");
                }
            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Get(int id)
        {
            try
            {
                var _prestame = _db.Prestamos
                                  .Where(p => p.Id == id).FirstOrDefault();

                if (_prestame != null)
                {
                    json.setMessage(_prestame, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(_prestame, JsonResponse.MessageType.Error, "El prestamo no existe.");
                }

            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Save(PrestamoViewModel prestamo, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _prestamo = new Prestamos()
                    {
                        CapitalActual = prestamo.CapitalInicial,
                        CapitalInicial = prestamo.CapitalInicial,
                        InteresActual = prestamo.InteresInicial,
                        InteresInicial = prestamo.InteresInicial,
                        FechaDeCreacion = DateTime.Now,
                        ClienteId = prestamo.ClienteId,
                        Estado = PrestamoEstado.Activo
                    };

                    _db.Prestamos.Add(_prestamo);
                    _db.SaveChanges();
                    json.setMessage(_prestamo, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(ModelState, JsonResponse.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Update(int id, PrestamoEstado estado)
        {
            try
            {
                if (id > 0 && estado > 0)
                {
                    var _prestamo = _db.Prestamos.Where(c => c.Id == id).FirstOrDefault();
                    if (_prestamo != null)
                    {
                        _prestamo.Estado = estado;

                        _db.SaveChanges();
                        json.setMessage(_prestamo, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(new Error() { code = "003" , message = "No existe data con esa criteria"}, JsonResponse.MessageType.Error);
                    }
                }
                else
                {
                    json.setMessage(new Error() { code = "003", message = "El valor enviado es invalido" }, JsonResponse.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
