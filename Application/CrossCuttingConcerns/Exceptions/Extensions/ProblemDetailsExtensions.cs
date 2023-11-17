using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.CrossCuttingConcerns.Exceptions.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static string ToJsonString(this ProblemDetails problemDetails)
        {
            return JsonSerializer.Serialize(problemDetails);
        }
    }
}
