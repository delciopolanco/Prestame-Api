using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prestame.Utils
{
    public static class Utils
    {
        public static decimal ToFixedDecimal(decimal value)
        {
            return decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}