using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services.Library.Helper
{
    public static class ObjectHelper
    {
        public static Tuple<byte[],int> GetObjectBuffer(object obj)
        {
            string resstring = SerializationHelper.Serialize(obj);
            var Byffer = Encoding.ASCII.GetBytes(resstring);
            return new Tuple<byte[], int>(Byffer, Byffer.Length);
        }
    }
}
