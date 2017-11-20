using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class Text
    {
        public static int CountNonSpaceChars(string value)
        {
            int result = 0;
            foreach (char c in value)
            {
                if (!char.IsWhiteSpace(c))
                {
                    result++;
                }
            }
            return result;
        }
    }
}