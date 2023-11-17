using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.CrossCuttingConcerns.Exceptions.Extensions
{
    public static class ValidationExceptionExtensions
    {

        public static Dictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> validationFailures)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            List<string> distinctPropertyNames = validationFailures.Select(v => v.PropertyName).Distinct().ToList();

            foreach (var propertyName in distinctPropertyNames)
            {
                string[] errors = validationFailures.Where(v => v.PropertyName == propertyName).Select(v => v.ErrorMessage).ToArray();
                result.Add(propertyName, errors);
            }

            return result;
        }
    }
}
