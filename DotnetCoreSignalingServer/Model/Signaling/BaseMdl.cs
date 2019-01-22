using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalingServer.Model.Signaling
{
    public class BaseObject
    {
        string _type;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
