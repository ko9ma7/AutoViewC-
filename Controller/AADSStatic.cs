using AutoViewWebAdsCSharp.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoViewWebAdsCSharp.Controller
{
    public static class AADSStatic
    {
        public static string Website = "http://newsit247.xyz";
        // Step 1
        public static object FirefoxMeta = new AppMeta("org.mozilla.firefox");
        // Step 2
        public static List<ColorCompareMeta> ColorFirefoxInitSuccess = new()
        {
            new ColorCompareMeta { SuccessColor = Color.FromArgb(220, 16, 95), X = 20, Y = 50 },
            new ColorCompareMeta { SuccessColor = Color.FromArgb(115, 90, 231), X = 20, Y = 40 },
        };
        // Step 3
        public static SimplePoint AddressBarPoint = new() { X = 59, Y = 304 };
        public static SimplePoint WebsitePoint = new() { X = 43, Y = 36 };
        // Step 4
        public static SimplePoint CloseSecurityTipPoint = new() { X = 149, Y = 239 };

    }
}
