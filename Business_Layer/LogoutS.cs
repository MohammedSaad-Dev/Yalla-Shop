using Data_Access_Layer;


namespace Business_Layer
{
    public class LogoutS
    {

        public static bool IsTokenBlacklisted(string token)
        {
            return logoutD.IsTokenInBlacklist(token);
        }
        public static bool AddTokenToBlacklist(string token)
        {
            return logoutD.AddTokenToBlacklist(token);
        }

    }
}
