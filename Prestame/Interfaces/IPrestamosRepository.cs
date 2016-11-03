using Prestame.Models;
using System;
using System.Web.Http.ModelBinding;

namespace Prestame.ViewModel
{
    public interface IPrestamosRepository<T> where T : IEntity, IDisposable
    {
        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Update(int id, PrestamosEstados estado);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);
    }
}