using GroceryStoreApi.Api.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        /// <summary>
        /// User login authentication method.
        /// /// <description> 
        /// This method check whether the result of user authentication request contains valid crendentials.
        /// If username or password is not valid, user can´t authenticate in the system.
        /// </description>
        /// </summary>
        /// <paramref name="authenticationRequest"></paramref>
        /// <param name="authenticationRequest">The user authentication request.</param>
        /// <example>
        /// <code>
        /// JwtAuthenticationRequest authenticationRequest = new JwtAuthenticationRequest();
        /// authenticationRequest.UserName = "Airlton";
        /// authenticationRequest.Password = "12131416";
        /// var login = Login(authenticationRequest); 
        /// </code>
        /// User can login, producing HTTP status code 200, if these crendetials are valid.
        /// </example>
        /// <returns>
        /// A new HTTP response status. If username or password from authentication request
        /// are valid, user can login and produces a HTTP status code 200 (authorized).
        /// else, user can not authenticate and produces HTTP status code 401.
        /// </returns>
        public IActionResult Login(JwtAuthenticationRequest authenticationRequest)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager();
            var authResult = jwtAuthenticationManager.Authenticate(authenticationRequest.UserName, 
                                                                   authenticationRequest.Password);

            // Checking the value of authentication result and producing a HTTP status code.
            if (authResult == null)
                return Unauthorized();
            else
                return Ok(authResult);
        }

    }
}
