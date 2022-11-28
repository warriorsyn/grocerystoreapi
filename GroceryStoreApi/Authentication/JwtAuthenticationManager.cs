using GroceryStoreApi.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroceryStoreApi.Api.Authentication
{
    public static class JwtAuthenticationManager
    {
        public static string Authenticate(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(JwtSettings.Secret);

            Claim claimAdmin1 = new(ClaimTypes.NameIdentifier, user.Id.ToString());
            Claim claimAdmin2 = new(ClaimTypes.Name, user.Name);
            Claim claimAdmin3 = new(ClaimTypes.Email, user.Email);
            Claim claimAdmin5 = new (ClaimValueTypes.Boolean, $"{user.IsAdmin}");

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    claimAdmin1,
                    claimAdmin2,
                    claimAdmin3,
                    claimAdmin5

                }),
                Expires = DateTime.UtcNow.AddHours(JwtSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var securitytoken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securitytoken);

            return token;
        }
    }
}
