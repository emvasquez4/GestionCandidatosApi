using GestionCandidatosApi.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace GestionCandidatosApi.Services.Utilidades
{
    public class Utilidades
    {
        private readonly IConfiguration _config;
        public Utilidades(IConfiguration config)
        {

            _config = config;

        }

        public string encriptarSHA256(string texto) { 
            using(SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                //convert bites array to string
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public string generarJWT(Usuarios model) {
            //informacion del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.id.ToString()),
                new Claim(ClaimTypes.Name, model.username)
            };

           
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: credentials
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
