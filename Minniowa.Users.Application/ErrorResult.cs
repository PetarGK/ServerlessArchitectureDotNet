using FluentValidation.Results;
using System.Collections.Generic;

namespace Minniowa.Users.Application
{
    public class ErrorResult : Result
    {
        public ErrorResult(IReadOnlyList<ValidationFailure> errors) : base(400, new ErrorData(errors))
        {
        }
    }

    public class ErrorData
    {
        public ErrorData(IReadOnlyList<ValidationFailure> messages)
        {
            Messages = messages;
        }

        public IReadOnlyList<ValidationFailure> Messages { get; private set; }
    }
}
