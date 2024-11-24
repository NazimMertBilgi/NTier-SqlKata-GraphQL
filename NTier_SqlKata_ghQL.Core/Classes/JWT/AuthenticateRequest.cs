using System.Text.Json.Serialization;

namespace NTier_SqlKata_ghQL.Core.Classes.JWT
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
