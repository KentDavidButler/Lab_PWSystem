using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_PWSystm
{
    class User
    {
        private string userName;
        private string password;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }


        public User(string uN, string pW)
        {
            this.userName = uN;
            this.password = pW;
        }
        public User()
        {
            this.userName = null;
            this.password = null;
        }

        public bool PassMatch(string input)
        {
            if(String.Equals(input, this.password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
