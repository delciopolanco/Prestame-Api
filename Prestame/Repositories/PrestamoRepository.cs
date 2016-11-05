using System;
using System.Linq;
using Prestame.Interfaces;
using Prestame.Data;
using System.Web.Http.ModelBinding;
using Prestame.Models;
using System.Data.Entity;


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
                var prestamos = (from c in _db.Cliente
                                 join p in _db.Prestamos
                                 on c.Id equals p.ClienteId
                                 where c.Id == p.ClienteId
                                 select new
                                 {
                                     p.Id,
                                     p.CapitalActual,
                                     p.CapitalInicial,
                                     p.InteresActual,
                                     p.InteresInicial,
                                     p.FechaDeCreacion,
                                     p.FechaDeSaldo,
                                     p.Estado,
                                     c.Nombres,
                                     c.Apellidos

                                 }).AsEnumerable()
                                  .Select(prestamo => new PrestamoViewModel()
                                  {
                                      Id = prestamo.Id,
                                      NombreCliente = prestamo.Nombres + ' ' + prestamo.Apellidos,
                                      CapitalActual = prestamo.CapitalActual,
                                      CapitalInicial = prestamo.CapitalInicial,
                                      InteresActual = prestamo.InteresActual,
                                      InteresInicial = prestamo.InteresInicial,
                                      FechaDeCreacion = prestamo.FechaDeCreacion,
                                      FechaDeSaldo = prestamo.FechaDeSaldo,
                                      EstadoPrestamo = EnumHelper.GetEnumDescription((Estados.EstadosPrestamos)prestamo.Estado)
                                  }).ToList();

                var message = (prestamos.Count > 0 ? null : "No existen prestamos registrados.");
                json.setMessage(prestamos, JsonResponse.MessageType.Success, message);
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
                var prestamo = _db.Prestamos.Where(p => p.Id == id).FirstOrDefault();

                if (prestamo != null)
                {
                    json.setMessage(prestamo, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(prestamo, JsonResponse.MessageType.Success, "El prestamo no existe.");
                }

            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse GetPrestamosByClient(int Id)
        {

            try
            {
                var prestamos = _db.Prestamos.Where(p => p.ClienteId == Id)
                                .AsEnumerable().Select(prestamo => new PrestamoViewModel()
                                {
                                    Id = prestamo.Id,
                                    EstadoPrestamo = EnumHelper.GetEnumDescription((Estados.EstadosPrestamos)prestamo.Estado),
                                    CapitalActual = prestamo.CapitalActual,
                                    CapitalInicial = prestamo.CapitalInicial,
                                    InteresActual = prestamo.InteresActual,
                                    InteresInicial = prestamo.InteresInicial,
                                    FechaDeCreacion = prestamo.FechaDeCreacion,
                                    FechaDeSaldo = prestamo.FechaDeSaldo
                                }).ToList();

                if (prestamos != null)
                {
                    json.setMessage(prestamos, JsonResponse.MessageType.Success);

                }
                else
                {
                    json.setMessage(new object(), JsonResponse.MessageType.Error, "El cliente no posee prestamos.");
                }
            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Save(PrestamoViewModel prestamoViewModel, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var prestamo = new Prestamos()
                    {
                        CapitalActual = prestamoViewModel.CapitalInicial,
                        CapitalInicial = prestamoViewModel.CapitalInicial,
                        InteresActual = prestamoViewModel.InteresInicial,
                        InteresInicial = prestamoViewModel.InteresInicial,
                        FechaDeCreacion = DateTime.Now,
                        ClienteId = prestamoViewModel.ClienteId,
                        Estado = (int)Estados.EstadosPrestamos.AlDía
                    };

                    _db.Prestamos.Add(prestamo);
                    _db.SaveChanges();
                    json.setMessage(prestamo, JsonResponse.MessageType.Success);
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

        public JsonResponse Update(int id, PrestamosEstadosViewModel estado)
        {
            try
            {
                if (id > 0 && estado != null)
                {
                    var _prestamo = _db.Prestamos.Where(c => c.Id == id).FirstOrDefault();

                    if (_prestamo != null)
                    {
                        _prestamo.Estado = estado.Id;
                        _db.SaveChanges();

                        var prestamo = new PrestamoViewModel()
                        {
                            Id = _prestamo.Id,
                            EstadoPrestamo = EnumHelper.GetEnumDescription((Estados.EstadosPrestamos)_prestamo.Estado)
                        };

                        json.setMessage(prestamo, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(new Error() { code = "003", message = "Prestamo no existe" }, JsonResponse.MessageType.Error);
                    }
                }
                else
                {
                    json.setMessage(new Error() { code = "004", message = "No cumple con el contrato" }, JsonResponse.MessageType.Error);
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
