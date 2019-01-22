using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace SignalingServer.Library.Helper
{
    public static class StringHelper
    {
        public static string FixCommand(string strcommnad)
        {
            
            if (strcommnad.Contains("\"type\":\"offer\"") || strcommnad.Contains("\"type\":\"answer\"") || strcommnad.Contains("\"type\":\"login\"") )
                return strcommnad;

            string fixStrcommnad = string.Empty;
            int fixIndex = 0;
            var strTemp = strcommnad.GetIndexList("\"}");

            foreach (var index in strTemp)
            {
                if (strcommnad.Substring(index + 2, 1) == ",")
                    fixIndex = index + 2;
                else
                {
                    fixIndex = index + 2;
                    break;
                }
                
            }

            fixStrcommnad = strcommnad.Substring(0, fixIndex);


            return fixStrcommnad;
        }


        private static IList<int> GetIndexList(this string str, string Input)
        {
            string s = str;
            var foundIndexes = new List<int>();


            for (int i = s.IndexOf(Input); i > -1; i = s.IndexOf(Input, i + 1))
            {
                foundIndexes.Add(i);
            }

            return foundIndexes;
        }
    }
}
