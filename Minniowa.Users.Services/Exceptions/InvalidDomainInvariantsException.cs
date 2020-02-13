using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Minniowa.Users.Services.Exceptions
{
    public class InvalidDomainInvariantsException : Exception
    {
        private List<ValidationFailure> _errors;
        public InvalidDomainInvariantsException(List<ValidationFailure> errors)
        {
            _errors = errors;
        }

        public IReadOnlyList<ValidationFailure> Errors => _errors.AsReadOnly();
    }
}
