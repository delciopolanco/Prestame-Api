using System;
using System.Web.Http.ModelBinding;

namespace Prestame.Interfaces
{
    public interface IClientesRepository<T> where T : IEntity, IDisposable 
    {

        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Delete(int id);

        JsonResponse Update(int id, T entity, ModelStateDictionary ModelState);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);
    }
}