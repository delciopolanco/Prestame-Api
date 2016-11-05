using Prestame.Models;
using System;
using System.Web.Http.ModelBinding;


namespace Prestame.Interfaces
{
    public interface IPagosRepository<T> where T : IEntity, IDisposable
    {
        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);

        JsonResponse GetClientPagos(int idClient);

        void Dispose();
    }
}