﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters.Authorization
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class ClaimAttribute:Attribute
    {
        public readonly string claimName;
        public ClaimAttribute(string claimName)
        {
            this.claimName = claimName;
        }
    }
}
