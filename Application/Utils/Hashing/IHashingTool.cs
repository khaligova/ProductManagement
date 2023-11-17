using Application.Utils.Hashing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.Hashing
{
    public interface IHashingTool
    {
        HashResponse GeneratePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash);
    }
}
