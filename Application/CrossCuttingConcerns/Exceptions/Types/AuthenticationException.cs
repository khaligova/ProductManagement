using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CrossCuttingConcerns.Exceptions.Types
{
    public class AuthenticationException:Exception
    {

        public AuthenticationException()
        {
                
        }

        public AuthenticationException(string message):base(message)
        {
            
        }
    }
}
