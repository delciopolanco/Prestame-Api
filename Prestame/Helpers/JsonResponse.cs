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
            error = new Error();
            data = new object();
            message = string.Empty;
            hasError = false;
        }

        public object data { get; set; }
        public bool hasError { get; set; }
        public string message { get; set; }
        public Error error { get; set; }

        public void setMessage(object dataJson, MessageType type, string customMessage = "")
        {

            if (type == MessageType.Success)
            {
                data = dataJson != null ? dataJson : new object();
                hasError = false;
                message = (!string.IsNullOrEmpty(customMessage) ? customMessage : GetMessage(MessageType.Success));
            }
            else if (data is Error)
            {
                var error = (Error)dataJson;
                error.code = error.code;
                error.message = (!string.IsNullOrEmpty(customMessage) ? customMessage : error.message);
            }
            else
            {
                this.hasError = true;
                this.error.code = "003";
                this.error.message = (!string.IsNullOrEmpty(customMessage) ? customMessage : GetMessage(MessageType.Error));
            }
        }

        public void setMessage(ModelStateDictionary errors, MessageType type, string customMessage = "")
        {
            string messages = string.Join(", ", errors.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            if (string.IsNullOrEmpty(messages))
            {
                messages = string.Join(", ", errors.Values.SelectMany(x => x.Errors).Select(x => x.Exception.Message));

                if (messages is String)
                {
                    //messages = string.Join("''", message.Substring(message.IndexOf("'") + 1, message.IndexOf("'") - message.Length - 1));
                    //TODO
                    messages = "No Cumple con el contrato";
                }
            }

            setMessage(new object(), MessageType.Error, messages);
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