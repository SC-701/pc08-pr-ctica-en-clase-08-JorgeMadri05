using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Reglas
{
    public interface IReglas
    {
        byte[] GenerarHash(string contrasenia);

        string ObtenerHash(byte[] hash);

        JwtSecurityToken? leerToken(string token);

        List<Claim> GenerarClaims(JwtSecurityToken? jwtToken, string accessToken);
    }
}
