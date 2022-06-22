using DevInSales.Api.Dtos;
using DevInSales.Core.Entities.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevInSales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public record Colaborador(string Area);

        [HttpPost()]
        public IActionResult Post(Authentication autenticacao)
        {
            /* Montar autenticação do JWT */
            /* Simulando o acesso ao banco de dados e trazendo os dados do usuário */
            var colaborador = new Colaborador("Administrador");

            var claimUsuario = new Claim("id", autenticacao.Usuario);
            var claimCountry = new Claim(ClaimTypes.Country, "Brasil");
            var claimArea = new Claim(ClaimTypes.Role, colaborador.Area);

            List<Claim> listaClaims = new List<Claim>();
            listaClaims.Add(claimUsuario);
            listaClaims.Add(claimCountry);
            listaClaims.Add(claimArea);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityTokenDescriptor securityTokenDescriptor = new();
            securityTokenDescriptor.Subject = new ClaimsIdentity(listaClaims);
            securityTokenDescriptor.Expires = DateTime.UtcNow.AddDays(1);
            securityTokenDescriptor.Issuer = JwtSettings.Issuer;
            securityTokenDescriptor.Audience = JwtSettings.Audience;

            /* Chave Simetrica */
            SymmetricSecurityKey symmetricSecurityKey = new(JwtSettings.Key);

            securityTokenDescriptor.SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenCriado = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var tokenNoFormatoString = jwtSecurityTokenHandler.WriteToken(tokenCriado);

            return Ok(tokenNoFormatoString);
        }
    }
}
