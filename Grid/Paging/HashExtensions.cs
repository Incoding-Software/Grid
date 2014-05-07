using System.Linq;
using System.Reflection;
using System.Web.Routing;

namespace Grid.Paging
{
    public static class HashExtensions
    {
        #region Api Methods

        public static string HashAction(this object query, params string[] include)
        {
            var dictionary = new RouteValueDictionary();
            var properties = query.GetType().GetProperties();
            foreach (PropertyInfo propInfo in properties)
            {
                if (include.Length > 0 && !include.Contains(propInfo.Name))
                    continue;

                var attrs = propInfo.GetCustomAttributes(typeof(HashUrlAttribute), false);
                if (attrs.Length > 0)
                {
                    var val = propInfo.GetValue(query, null);
                    if (val != null && !string.IsNullOrWhiteSpace(val.ToString()) || (propInfo.Name == "Page" && (val.Equals(0) || val.Equals(1))))
                        dictionary.Add(propInfo.Name, val.ToString());
                }
            }

            var hashAction = string.Join("/", dictionary.Select(r => r.Key + "=" + r.Value));
            return hashAction;
        }

        #endregion
    }
}