using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CrossCuttingConcerns.Exceptions.ExceptionProblemDetails
{
    public class ValidationProblemDetail:ProblemDetails
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ValidationProblemDetail()
        {

        }
        public ValidationProblemDetail(Dictionary<string, string[]> errors)
        {
            Errors = errors;
        }
    }
}
