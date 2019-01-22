using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalingServer.Services
{
    public class ClsEnumeration
    {
        public enum EnumCommandType
        {
            login = 0,
            offer = 1,
            answer = 2,
            candidate = 3,
            leave = 4,
        }
    }
}
