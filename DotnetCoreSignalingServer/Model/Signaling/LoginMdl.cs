using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Model.Signaling
{
    public class LoginObject
    {
        string _type;
        string _name;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    public class LoginResult
    {
        string _type;
        string _success;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string success
        {
            get { return _success; }
            set { _success = value; }
        }
    }
}
