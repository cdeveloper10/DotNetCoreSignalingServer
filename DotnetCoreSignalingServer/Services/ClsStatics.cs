using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalingServer.Services
{
    public class ClsStatics
    {
        public static object MessageLockObject = new Object();
        public static object MessageLockCandidate = new Object();
        public static IDictionary<string, dynamic> UsersStatic = new Dictionary<string, dynamic>();
    }
}
