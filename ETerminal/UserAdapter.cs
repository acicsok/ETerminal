using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETerminal
{
    class UserAdapter
    {
        User user;
        List<string> userIDs;

        public UserAdapter() { }

        public UserAdapter(byte[] userData)
        {
            user = new User();
            Encoding en = Encoding.Default;
            byte[] aEmpID = new byte[15];
            int nx = 1;

            for (int nk = 51; nk < 65; nk++)
            {
                Array.Resize(ref aEmpID, nx);
                if (userData[nk] == 0)
                {
                    Array.Resize(ref aEmpID, nx);
                    break;
                }
                else
                    aEmpID[nx - 1] = userData[nk];
                nx++;
            }

            string employeeID = Encoding.GetEncoding(en.HeaderName).GetString(aEmpID).Trim().Replace("\0", "");
            user.EmployeeID = employeeID;

        }

        public void ComputeUserID(Array userIDsByte)
        {
            if (userIDsByte != null)
            {
                userIDs = new List<string>();
                foreach (var id in userIDsByte)
                {
                    userIDs.Add(id.ToString().PadLeft(10, '0'));
                }
            }
        }

        public List<string> GetIDlist()
        {
            return userIDs;
        }

        public User GetUser()
        {
            return user;
        }
    }

    class User
    {
        public string EmployeeID { get; set; }
        public string CardID { get; set; }

        public User() { }


    }
}
