using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Shered
{
    public record Error
    {
        private Error(string code, string message, ErrorType errorType)
        {
            Code = code;
            Message = message;
            Type = errorType;
        }

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        public static Error Validation(string code, string message) => new Error(code, message, ErrorType.Validation);
        public static Error NotFound(string code, string message) => new Error(code, message, ErrorType.NotFound);
        public static Error Failure(string code, string message) => new Error(code, message, ErrorType.Failure);
        public static Error Conflict(string code, string message) => new Error(code, message, ErrorType.Conflict);
        public static Error Unknown(string code, string message) => new Error(code, message, ErrorType.Unknown);
        public static Error Update(string code, string massage) => new Error(code, massage, ErrorType.Update);

        public Errors ToErrors() => new([this]);
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict,
        Unknown,
        Update
    }
}
