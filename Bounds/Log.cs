using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds
{
    public class Log
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}