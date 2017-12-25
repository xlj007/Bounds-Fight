using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Bounds.Utils
{
    public static class Extension
    {
        public static int to_i(this object obj)
        {
            int num;
            if (obj == null) return 0;
            bool boCan = int.TryParse(obj.ToString().Trim(), out num);
            if (!boCan)
                num = 0;
            return num;
        }
        public static double to_double(this object obj)
        {
            double num;
            if (obj == null) return 0;
            bool boCan = double.TryParse(obj.ToString().Trim(), out num);
            if (!boCan)
                num = 0;
            return num;
        }

        public static bool to_bool(this object obj)
        {
            if (obj == null) return false;
            if (obj.ToString() == "0" || obj.ToString().ToLower() == "false") return false;
            else return true;
        }
        public static bool is_num(this string str)
        {
            if (Encoding.Default.GetByteCount(str) != str.Length)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool is_time(this string str)
        {
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
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