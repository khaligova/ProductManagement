using Application.Utils.JWT.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.JWT
{
    public interface ITokenHelper
    {
        Token CreateToken(string identifier,List<string> permissionClaims);
        bool ValidateToken(string rawToken);
        string NormalizeRawToken(string rawToken);
        JwtSecurityToken DecodeToken(string token);
        string GetCurrentToken();
        JwtSecurityToken GetCurrentDecodedToken();
        string GetCurrentIdentifier();
        string[] GetPermissionClaims();
    }
}
