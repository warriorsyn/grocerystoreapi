using GroceryStoreApi.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroceryStoreApi.Api.Authentication
{
    public class JwtAuthenticationManager
    {

        /// <summary>
        /// Token authentication method.
        /// <description> 
        /// This method authenticates an user client by using the username and password crendetials.
        /// </description>
        /// </summary>
        /// <paramref name="user"/>
        /// <param name="user">The client user to authenticate.</param>
        /// <example>
        /// <code>
        /// UserDto user = new UserDto();
        /// var jwtAuthenticationResponse = new JwtAuthenticationResponse();
        /// jwtAuthenticationResponse.Authenticate(user);
        /// </code>
        /// Results in an auth response, indicating an user can be authenticated in the system.
        /// </example>
        /// <returns>
        /// A new Jwt Authentication response object with valued fields token, username and expire time.
        /// If the username and password are invalid, return null.
        /// </returns>
        public JwtAuthenticationResponse Authenticate(UserDto user)
        {
            // Validating the Username and Password
            if (user.Email != "user01" || user.Password != "123456") 
            {
                return null;
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenExpireTime = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);

            // Generating the token key based on server side secret
            var tokenKey = Encoding.ASCII.GetBytes(Settings.Secret);

            // Describing the administrator claims
            Claim claimAdmin1 = new Claim(ClaimTypes.PrimaryGroupSid, "Grupo de Usuário Administrador");
            Claim claimAdmin2 = new Claim(ClaimTypes.Name, user.Name);
            Claim claimAdmin3 = new Claim(ClaimTypes.Role, "Administrador");
            Claim claimAdmin4 = new Claim(ClaimTypes.Email, user.Email);
            Claim claimAdmin5 = new Claim(ClaimValueTypes.Boolean, $"{user.IsAdmin}");

            // Describing the security token descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    claimAdmin1,
                    claimAdmin2,
                    claimAdmin3,
                    claimAdmin4,
                    claimAdmin5

                }),
                Expires = tokenExpireTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // Creating the token
            var securitytoken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securitytoken);

            return new JwtAuthenticationResponse
            {
                token = token,
                user_name = user.Name,
                expires_in = (int) tokenExpireTime.Subtract(DateTime.Now).TotalSeconds
            };
        }

    }
}
