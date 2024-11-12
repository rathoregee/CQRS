using EventSourcing.Features.CreatePlayer;
using EventSourcing.Models;
using EventSourcing.Validation;
using System.Runtime.CompilerServices;
using System.Text;

namespace EventSourcing.Common
{
    public static class Extensions
    {
        public static RequestValidationResult VaildateRequest(this CreatePlayerCommand command)
        {
            var validator = new CreatePlayerValidator();
            var result = new RequestValidationResult {IsValid = true,Message = string.Empty };
            var vaildation =  validator.Validate(command);

            if (!vaildation.IsValid)
            {
                var builder = new StringBuilder();

                foreach (var failure in vaildation.Errors)
                {
                    builder.AppendLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                result.IsValid = false;
                result.Message = builder.ToString();
            }

            return result;
        }
    }
}
