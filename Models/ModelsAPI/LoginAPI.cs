
namespace ZenLog.Models.ModelsAPI
{
    public class LoginAPI
    {
        public required string userNameOrEmailAddress { get; set; }

        public required string password { get; set; }
        public required string personalAccessToken { get; set; }
        public required string tenantName { get; set; }
    }
}
