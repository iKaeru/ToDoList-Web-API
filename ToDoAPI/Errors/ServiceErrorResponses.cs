using System;
using System.Net;
using Client.Models.Errors;

namespace ToDoAPI.Errors
{
    public static class ServiceErrorResponses
    {
        public static ServiceErrorResponse ItemNotFound(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            var error = new ServiceErrorResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Error = new ServiceError
                {
                    Code = ServiceErrorCodes.NotFound,
                    Message = $"A ToDo item with \"{itemId}\" not found.",
                    Target = "ToDoItem"
                }
            };

            return error;
        }

        public static ServiceErrorResponse BodyIsMissing(string target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            
            var error = new ServiceErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Error = new ServiceError
                {
                    Code = ServiceErrorCodes.BadRequest,
                    Message = "Request body is empty.",
                    Target = target
                }
            };

            return error;
        }
    }
}
