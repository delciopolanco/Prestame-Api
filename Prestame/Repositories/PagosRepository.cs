using Prestame.Data;
using Prestame.Interfaces;
using Prestame.ViewModel;
using System.Linq;
using System;
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
              
                                  
            }
            catch (Exception)
            {
                
                throw;
            }

            throw new NotImplementedException();
        }

        public JsonResponse Get(int id)
        {
            throw new NotImplementedException();
        }

        public JsonResponse Save(PagosViewModel entity, System.Web.Http.ModelBinding.ModelStateDictionary ModelState)
        {
            throw new NotImplementedException();
        }
    }
}