using System;
using System.Collections.Generic;
using System.Text;

namespace SetlistNet
{
    class TagHelper
    {
        public static string Href(string src, string value)
        {
            return $"<a href=\"{src}\">{value}</a>";
        }
    }
}
