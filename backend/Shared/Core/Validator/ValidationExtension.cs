﻿using System.Text.Json;
﻿using FluentValidation.Results;
using SharedKernel.Exseption;

namespace Core.Validation;

public static class ValidationExtension
{
    public static Errors ToList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = JsonSerializer.Deserialize<Error>(errorMessage)
            select Error.FluentValidation(error.Code, errorMessage, validationError.PropertyName);

        return errors.ToList();
    }
}