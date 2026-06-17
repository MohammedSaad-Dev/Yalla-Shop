using Data_Accesst_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class UserDetail
    {

        public class UserProfileDTO
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
        }

        public partial class clsUser
        {
            public static UserProfileDTO GetMyDetails(int userId)
            {
                DataRow row = UserDetailsD.GetUserDetailsById(userId);

                if (row == null) return null;

                return new UserProfileDTO
                {
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString()
                };
            }

            // Validation and Add
            public static bool UpdateMyDetails(int userId, UserProfileDTO updatedData)
            {
                return UserDetailsD.UpdateUserDetails(
                    userId,
                    updatedData.FirstName,
                    updatedData.LastName,
                    updatedData.Phone,
                    updatedData.Address
                );
            }
        }

    }
}
