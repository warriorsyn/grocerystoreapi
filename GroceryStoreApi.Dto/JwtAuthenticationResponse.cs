namespace GroceryStoreApi.Api.Authentication
{
    public class JwtAuthenticationResponse
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
