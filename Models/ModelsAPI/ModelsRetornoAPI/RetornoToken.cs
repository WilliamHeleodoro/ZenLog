
namespace ZenLog.Models.ModelsAPI.ModelsRetornoAPI
{
    public class RetornoToken
    {
        public string? accessToken { get; set; }
        public string? encryptedAccessToken { get; set; }
        public int expireInSeconds { get; set; }
        public bool termosAceitos { get; set; }
        public bool changePasswordNextLogin { get; set; }
        public int userId { get; set; }
    }
}
