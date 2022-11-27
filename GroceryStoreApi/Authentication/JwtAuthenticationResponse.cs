using System.ComponentModel;

namespace GroceryStoreApi.Api.Authentication
{
    [Serializable]
    public class JwtAuthenticationResponse
    {
        [Description("Demonstrates the token key to authenticate an user in the system")]
        public string token { get; set; }

        [Description("Demonstrates the username client to authenticate in the system")]
        public string user_name { get; set; }

        [Description("Demonstrates an expiration period (in minutes)" +
            " which token can be used to disconnect users in the system")]
        public int expires_in { get; set; }
    }
}
