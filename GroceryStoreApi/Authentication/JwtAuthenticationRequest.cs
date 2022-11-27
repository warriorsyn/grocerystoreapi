namespace GroceryStoreApi.Api.Authentication
{
    [Serializable]
    public class JwtAuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
