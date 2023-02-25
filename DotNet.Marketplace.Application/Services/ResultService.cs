
using FluentValidation.Results;

namespace DotNet.Marketplace.Application.Services
{
    public class ResultService
    {
        public bool IsSucesse { get; set; }
        public string Message { get; set; }
        public ICollection<ErrorValidation> Errors { get; set; }

        public static ResultService RequestError(string message, ValidationResult validation)
        {
            return new ResultService
            {
                IsSucesse = false,
                Message = message,
                Errors = validation.Errors.Select(
                        x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }
        public static ResultService<T> RequestError<T>(string message, ValidationResult validation)
        {
            return new ResultService<T>
            {
                IsSucesse = false,
                Message = message,
                Errors = validation.Errors.Select(
                        x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }

        public static ResultService Fail(string message) => new ResultService { IsSucesse = false, Message = message };
        public static ResultService<T> Fail<T>(string message) => new ResultService<T> { IsSucesse = false, Message = message };

        public static ResultService Ok(string message) => new ResultService { IsSucesse = true, Message = message };
        public static ResultService<T> Ok<T>(T data) => new ResultService<T> { IsSucesse = true, Data = data };
    }

    public class ResultService<T> : ResultService
    {
        public T Data { get; set; }
    }
}
