using System;
using System.Linq;
using Prestame.Interfaces;
using System.Web.Http.ModelBinding;
using System.Data.Entity;
using Prestame.Models;
using System.Collections.Generic;

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

        public JsonResponse Get()
        {
            try
            {
                var clientes = _db.Cliente
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

                var message = (clientes.Count > 0 ? null : "No existen Clientes registrados.");
                json.setMessage(clientes, JsonResponse.MessageType.Success, message);
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
                var cliente = _db.Cliente
                                  .Include(p => p.Telefonos)
                                  .Include(p => p.Direcciones)
                                  .Where(c => c.Id == id).FirstOrDefault();

                if (cliente != null)
                {
                    json.setMessage(cliente, JsonResponse.MessageType.Success);
                }
                else
                {
                    json.setMessage(cliente, JsonResponse.MessageType.Error, "El cliente no existe.");
                }

            }
            catch (Exception ex)
            {
                PrestameExceptions.HandleException(ex, json);
            }

            return json;
        }

        public JsonResponse Save(ClienteViewModel clienteViewModel, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = new Cliente()
                    {
                        Nombres = clienteViewModel.Nombres,
                        Apellidos = clienteViewModel.Apellidos,
                        Identificacion = clienteViewModel.Identificacion,
                        Direcciones = new List<Direcciones>(),
                        Telefonos = new List<Telefonos>(),
                        Prestamos = new List<Prestamos>()
                    };

                    if (clienteViewModel.Direcciones != null)
                        cliente.Direcciones = clienteViewModel.Direcciones;
                    if (clienteViewModel.Telefonos != null)
                    {
                        cliente.Telefonos = clienteViewModel.Telefonos.Select(t => new Telefonos()
                        {
                            Telefono = t.Telefono,
                            TiposTelefonoId = t.TiposTelefonoId
                        }).ToList();
                    }

                    _db.Cliente.Add(cliente);
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

        public JsonResponse Update(int id, ClienteViewModel clienteViewModel, ModelStateDictionary ModelState)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = _db.Cliente.Where(c => c.Id == id).FirstOrDefault();
                    if (cliente != null)
                    {
                        cliente.Nombres = clienteViewModel.Nombres;
                        cliente.Apellidos = clienteViewModel.Apellidos;
                        cliente.Identificacion = clienteViewModel.Identificacion;

                        _db.SaveChanges();
                        json.setMessage(cliente, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(new object(), JsonResponse.MessageType.Error, "Cliente no existe");
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
                    var cliente = _db.Cliente
                                    .Include(p => p.Telefonos)
                                    .Include(p => p.Direcciones)
                                    .Where(c => c.Id == id).FirstOrDefault();

                    if (cliente != null)
                    {

                        if (cliente.Direcciones != null) _db.Direcciones.RemoveRange(cliente.Direcciones);
                        if (cliente.Telefonos != null) _db.Telefonos.RemoveRange(cliente.Telefonos);

                        _db.Cliente.Remove(cliente);
                        _db.SaveChanges();

                        transaction.Commit();
                        json.setMessage(cliente, JsonResponse.MessageType.Success);
                    }
                    else
                    {
                        json.setMessage(cliente, JsonResponse.MessageType.Error, "El cliente no existe.");
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

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}