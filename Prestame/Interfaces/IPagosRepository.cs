using System;
using System.Web.Http.ModelBinding;

namespace Prestame.Interfaces
{
    public interface IPagosRepository<T> where T : IEntity, IDisposable
    {
        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);
    }
}