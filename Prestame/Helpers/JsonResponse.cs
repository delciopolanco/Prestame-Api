using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Prestame.Interfaces
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            this.error = new Error();
            this.data = new object();
            this.message = string.Empty;
            this.hasError = false;
        }

        public object data { get; set; }
        public bool hasError { get; set; }
        public string message { get; set; }
        public Error error { get; set; }

        public void setMessage(object data, MessageType type, string customMessage = "")
        {
            this.data = new object();

            if (type == MessageType.Success)
            {
                this.data = data;
                this.hasError = false;
                this.message = this.GetMessage(MessageType.Success);
            }
            else if (data is Error)
            {
                var error = (Error)data;
                this.error.code = error.code ;
                this.error.message = error.message;
            }
            else
            {
                this.hasError = true;
                this.error.code = "003";
                this.error.message = this.GetMessage(MessageType.Error);
            }

        }

        public void setMessage(ModelStateDictionary errors, MessageType type, string customMessage = "")
        {
            string messages = string.Join(", ", errors.Values
                                                      .SelectMany(x => x.Errors)
                                                      .Select(x => x.ErrorMessage));
            setMessage(messages, MessageType.Error, customMessage);
        }

        public enum MessageType
        {
            Error,
            Success,
        }

        public string GetMessage(MessageType messageType)
        {
            string message = string.Empty;

            if (messageType == MessageType.Success)
            {
                message = "Su solicitud se proceso correctamente.";
            }
            else
            {
                message = "En estos momentos no podemos procesar su solcitud.";
            }

            return message;
        }


    }

    public class Error
    {
        public Error()
        {
            this.code = string.Empty;
            this.message = string.Empty;
        }
        public string code { get; set; }

        public string message { get; set; }

    }
}