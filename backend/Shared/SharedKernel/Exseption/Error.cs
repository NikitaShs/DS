using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedKernel.Exseption
{
    public record Error
    {
        private Error(string code, string message, ErrorType errorType, string? propertyName)
        {
            Code = code;
            Message = message;
            Type = errorType;
            PropertyName = propertyName;
        }

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? PropertyName { get; }

        public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation, null);
        public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound, null);
        public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure, null);
        public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict, null);
        public static Error Unknown(string code, string message) => new(code, message, ErrorType.Unknown, null);
        public static Error Update(string code, string massage) => new(code, massage, ErrorType.Update, null);
        public static Error FluentValidation(string code, string message, string propertyName) => new(code, message, ErrorType.Validation, propertyName);


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
