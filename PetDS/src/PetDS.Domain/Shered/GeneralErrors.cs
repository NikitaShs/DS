using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Shered
{
    public class GeneralErrors
    {
        public static Error ValueNotValid(string? nameValue)
        {
            var value = nameValue ?? "value";
            return Error.Validation("value.is.Invalid", $"{value} is invalid");
        }

        public static Error ValueNotFound(Guid? IdValue)
        {
            var id = IdValue == null ? " " : $"for id '{IdValue}'";
            return Error.Validation("value.not.Found", $"record not Found {id}");
        }

        public static Error ValueFailure(string? nameValue)
        {
            var value = nameValue ?? "value";
            return Error.Validation("value.not.create", $"{value} not create");
        }

        public static Error Unknown()
        {
            return Error.Unknown("error.is.unknown", "error of unknown type");
        }

        public static Error Update(string nameValue)
        {
            return Error.Update("update.is.failed", $"update {nameValue} failed");
        }

    }
}
