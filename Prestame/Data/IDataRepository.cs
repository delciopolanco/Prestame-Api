using Prestame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Prestame.Data
{
    public interface IDataRepository<T> where T : IEntity, IDisposable 
    {

        JsonResponse Get();

        JsonResponse Get(int id);

        JsonResponse Delete(int id);

        JsonResponse Update(int id, T entity, ModelStateDictionary ModelState);

        JsonResponse Save(T entity, ModelStateDictionary ModelState);
    }

    public interface IEntity { }
}