using System.Linq;

namespace PSPlywoodWeb.Common
{
    public static class Common
    {
        public static string ToStringSmart(this string str)
        {
            if (str == null) { return ""; }

            return str.Replace(" ", "_");
        }
        public static string ToTagsStringSmart(this string str)
        {
            if (str == null) { return ""; }

            var ls  = str.Split(',');
            var lss = new List<string>();
            foreach (var s in ls) {
                lss.Add(s.Trim().Replace(" ", "_"));
            }

            return string.Join(" ", lss);
        }
    }
}
