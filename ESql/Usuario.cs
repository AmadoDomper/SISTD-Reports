using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESql
{
    public class Usuario
    {
        int _UserId;
        string _UserName;


        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
    }
}
