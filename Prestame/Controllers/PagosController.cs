﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Prestame.Interfaces;
using Prestame.Models;
using Prestame.ViewModel;
using Prestame.Repositories;

namespace Prestame.Controllers
{
    public class PagosController : ApiController
    {
        private IPagosRepository<PagosViewModel> _dbPagos;


        public PagosController()
        {
            _dbPagos = new PagosRepository();
        }

        public PagosController(IPagosRepository<PagosViewModel> repository)
        {
            _dbPagos = repository;
        }

        // GET: api/Pagos
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public JsonResponse Get()
        {
            var json =_dbPagos.Get();
            return json;
        }

        // GET: api/Pagos/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public JsonResponse Get(int id)
        {
            var json = _dbPagos.Get(id);
            return json;
        }

        // GET: api/Pagos/GetPagosByCliente/5
        [ResponseType(typeof(JsonResponse))]
        [ActionName("GetPagosByCliente")]

        public JsonResponse GetPagosByCliente(int id)
        {
            var json = _dbPagos.GetPagosByCliente(id);
            return json;
        }

        // POST: api/Pagos
        [ResponseType(typeof(JsonResponse))]
        [ActionName("DefaultAction")]
        public JsonResponse PostPagos(PagosViewModel pago)
        {
            var json = _dbPagos.Save(pago, ModelState);
            return json;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbPagos != null)
                {
                    _dbPagos.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}