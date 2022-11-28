namespace GroceryStoreApi.Api.Authentication
{
    [Serializable]
    public class JwtAuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
