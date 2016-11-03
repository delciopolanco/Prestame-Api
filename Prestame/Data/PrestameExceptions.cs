using Prestame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prestame.Data
{
    public abstract class PrestameExceptions : Exception
    {
        private static Error error = new Error();
        public static Error HandleException(Exception ex, JsonResponse json)
        {
            switch (ex.GetType().Name)
            {
                case "DbEntityValidationException":
                case "NotSupportedException":
                    error.code = "001";
                    error.message = "No cumple con las validaciones del contrato";
                    break;
                case "DbUpdateConcurrencyException":
                    error.code = ex.HResult.ToString();
                    error.message = "En estos momentos no podemos llevar a cabo su solicitud intente nuevamente o cumiquese al administrador del sistema";
                    break;
                case "InvalidOperationException":
                    error.code = ex.HResult.ToString();
                    error.message = "En estos momentos no podemos llevar a cabo su solicitud intente nuevamente o cumiquese al administrador del sistema";
                    break;
                default:
                    error.code = "9999";
                    error.message = "Ups! algo anda mal con la aplicación, disculpe los inconvenientes";
                    break;
            }

            json.setMessage(error, JsonResponse.MessageType.Error);

            return error;
        }
    }
}