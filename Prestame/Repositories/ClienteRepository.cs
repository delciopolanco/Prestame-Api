using System;
using System.Linq;
using Prestame.ViewModel;
using System.Web.Http.ModelBinding;
using System.Data.Entity;
using Prestame.Models;

namespace Prestame.Data
{
    ///<Summary>
    /// Gets the answer
    ///</Summary>
    public class ClienteRepository : IClientesRepository<ClienteViewModel>, IDisposable
    {
        private PrestameContext _db;
        private JsonResponse json = new JsonResponse();

        public ClienteRepository(PrestameContext db)
        {
            _db = db;
        }

        public ClienteRepository()
        {
            _db = new PrestameContext();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public JsonResponse Get()
        {
            try
            {
                var _clientes = _db.Cliente
                                    .Include(p => p.Telefonos)
                                    .Include(p => p.Direcciones)
                                    .Select(c => new ClienteViewModel()
                                 {
                                     Id = c.Id,
                                     Nombres = c.Nombres,
                                     Apellidos = c.Apellidos,
                                     Identificacion = c.Identificacion,
                                     Direcciones = c.Direcciones,
                                     Telefonos = c.Telefonos.Select(t => new TelefonosViewModel()
                                     {
                                         Id = t.TiposTelefonoId,
                                         Telefono = t.Telefono,
                                         TiposTelefonoId = t.TiposTelefonoId,
                                         TiposTelefono = t.TiposTelefono.TipoTelefono
                                     }).ToList()
                                 }).ToList();

                if (_clientes != null)
                {
                    json.setMessage(_clientes, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(_clientes, JsonResponse.MessageType.Error, "No existen clientes registrados.");
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
                var _cliente = _db.Cliente
                                  .Include(p => p.Telefonos)
                                  .Include(p => p.Direcciones)
                                  .Where(c => c.Id == id).FirstOrDefault();

                if (_cliente != null)
                {
                    json.setMessage(_cliente, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(_cliente, JsonResponse.MessageType.Error, "El cliente no existe.");
                }

            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Save(ClienteViewModel cliente, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _cliente = new Cliente()
                    {
                        Nombres = cliente.Nombres,
                        Apellidos = cliente.Apellidos,
                        Identificacion = cliente.Identificacion,
                        Direcciones = cliente.Direcciones,
                        Telefonos = cliente.Telefonos.Select(t => new Telefonos()
                        {
                            Telefono = t.Telefono,
                            TiposTelefonoId = t.TiposTelefonoId
                        }).ToList()
                    };

                    _db.Cliente.Add(_cliente);
                    _db.SaveChanges();
                    json.setMessage(cliente, JsonResponse.MessageType.Success);
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

        public JsonResponse Update(int id, ClienteViewModel cliente, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _cliente = _db.Cliente.Where(c => c.Id == id).FirstOrDefault();
                    if (_cliente.Id > 0)
                    {
                        _cliente.Nombres = cliente.Nombres;
                        _cliente.Apellidos = cliente.Apellidos;
                        _cliente.Identificacion = cliente.Identificacion;

                        _db.SaveChanges();
                        json.setMessage(cliente, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(ModelState, JsonResponse.MessageType.Error);
                    }
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

        public JsonResponse Delete(int id)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var _cliente = _db.Cliente
                                    .Include(p => p.Telefonos)
                                    .Include(p => p.Direcciones)
                                    .Where(c => c.Id == id).FirstOrDefault();

                    var _direcciones = _cliente.Direcciones;
                    var _telefonos = _cliente.Telefonos;

                    if (_cliente != null)
                    {
                        if (_direcciones != null) _db.Direcciones.RemoveRange(_direcciones);
                        if (_telefonos != null) _db.Telefonos.RemoveRange(_telefonos);
                        _db.Cliente.Remove(_cliente);

                        _db.SaveChanges();
                        transaction.Commit();
                        json.setMessage(_cliente, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(new Error() { code = "004", message = "La entidad a eliminar no existe." }, JsonResponse.MessageType.Error);
                        transaction.Rollback();
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

    }
}