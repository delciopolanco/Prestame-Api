using System;
using System.Linq;
using Prestame.Interfaces;
using Prestame.Data;
using System.Web.Http.ModelBinding;
using Prestame.Models;
using Prestame.ViewModel;
using System.Data.Entity;

namespace Prestame.Repositories
{
    public class PagosRepository : IPagosRepository<PagosViewModel>, IDisposable
    {
        private PrestameContext _db;
        private JsonResponse json = new JsonResponse();

        public PagosRepository(PrestameContext db)
        {
            _db = db;
        }

        public PagosRepository()
        {
            _db = new PrestameContext();
        }
        public JsonResponse Get()
        {
            try
            {
                var pagos = (from c in _db.Cliente
                             join pe in _db.Prestamos
                             on c.Id equals pe.ClienteId
                             join pa in _db.Pagos
                             on pe.Id equals pa.PrestamoId
                             where c.Id == pe.ClienteId
                             select new
                             {
                                 pa.Id,
                                 pa.Interes,
                                 pa.Capital,
                                 pa.FechaPago,
                                 pa.Tasa,
                                 pa.Monto,
                                 PrestamoId = pe.Id,
                                 c.Nombres,
                                 c.Apellidos

                             }).AsEnumerable().Select(pago => new PagosViewModel()
                             {
                                 Id = pago.Id,
                                 NombreCliente = string.Format("{0} {1}", pago.Nombres, pago.Apellidos),
                                 Monto = pago.Monto,
                                 Capital = pago.Capital,
                                 Interes = pago.Interes,
                                 Tasa = pago.Tasa,
                                 FechaPago = pago.FechaPago,
                                 PrestamoId = pago.PrestamoId
                             }).ToList();

                var message = (pagos.Count > 0 ? null : "No existen pagos registrados.");
                json.setMessage(pagos, JsonResponse.MessageType.Success, message);
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
                var _pagos = _db.Pagos.Where(p => p.Id == id).FirstOrDefault();
                var message = (_pagos != null ? "No existe el pago." : null);

                json.setMessage(_pagos, JsonResponse.MessageType.Success, message);

            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse GetPagosByCliente(int idClient)
        {

            try
            {
                var pagos = (from c in _db.Cliente
                             join pe in _db.Prestamos
                             on c.Id equals pe.ClienteId
                             join pa in _db.Pagos
                             on pe.Id equals pa.PrestamoId
                             where c.Id == idClient
                             select new
                             {
                                 pa.Id,
                                 pa.Interes,
                                 pa.Capital,
                                 pa.FechaPago,
                                 pa.Tasa,
                                 pa.Monto,
                                 PrestamoId = pe.Id,
                                 c.Nombres,
                                 c.Apellidos

                             }).AsEnumerable().Select(pago => new PagosViewModel()
                             {
                                 Id = pago.Id,
                                 NombreCliente = string.Format("{0} {1}", pago.Nombres, pago.Apellidos),
                                 Monto = pago.Monto,
                                 Capital = pago.Capital,
                                 Interes = pago.Interes,
                                 Tasa = pago.Tasa,
                                 FechaPago = pago.FechaPago
                             }).ToList();

                if (pagos != null)
                {
                    json.setMessage(pagos, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(new object(), JsonResponse.MessageType.Error, "El cliente no posee pagos.");
                }
            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Save(PagosViewModel pagoViewModel, ModelStateDictionary modelState)
        {

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    if (modelState.IsValid)
                    {
                        var prestamo = _db.Prestamos.Where(p => p.Id == pagoViewModel.PrestamoId).FirstOrDefault();
                        if (prestamo == null)
                        {
                            json.setMessage(new object(), JsonResponse.MessageType.Error, "No existe el prestamo.");
                            return json;
                        }


                        var montoPago = GetMontoPago(prestamo.CapitalActual, prestamo.InteresActual, pagoViewModel.Monto);

                        var pago = new Pagos()
                        {
                            Monto = pagoViewModel.Monto,
                            Tasa = prestamo.InteresActual,
                            Interes = montoPago.Interes,
                            Capital = montoPago.Capital,
                            FechaPago = DateTime.Now,
                            PrestamoId = prestamo.Id
                        };

                        _db.Pagos.Add(pago);
                        var newCapital = GetCapitalActual(prestamo.CapitalActual, pago.Capital);
                        prestamo.CapitalActual = newCapital.Capital;
                        _db.SaveChanges();
                        transaction.Commit();

                        json.setMessage(pago, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(modelState, JsonResponse.MessageType.Error, "No pago es invalido.");
                    }
                }
                catch (Exception ex)
                {
                    PrestameExceptions.HandleException(ex, json);
                    transaction.Rollback();
                }
            }

            return json;
        }

        private Pagos GetMontoPago(decimal? capital, decimal? interes, decimal montoTotal)
        {
            var montoPago = new Pagos
            {
                Capital = (montoTotal - (capital * interes)),
                Interes = (capital * interes)
            };
            return montoPago;
        }

        private Pagos GetCapitalActual(decimal? capitalActual, decimal? capitalPago)
        {
            var montoPago = new Pagos
            {
                Capital = capitalActual - capitalPago
            };
            return montoPago;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}