using Prestame.Interfaces;
using System;
using System.Web.Http.ModelBinding;

namespace Prestame.Interfaces
{
    public interface IPrestamosRepository<T> where T : IEntity, IDisposable
    {
        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Update(int id, PrestamoEstado estado);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);
    }
}