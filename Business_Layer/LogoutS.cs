using Data_Access_Layer;
using Data_Accesst_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
