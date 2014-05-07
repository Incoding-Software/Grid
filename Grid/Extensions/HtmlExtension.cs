using System.Web.Mvc;
using Grid.Interfaces;

namespace Grid.Extensions
{
    public static class HtmlExtension
    {
        public static IGridBuilder<TModel> Grid<TModel>(this HtmlHelper htmlHelper) where TModel : class
        {
            return new GridBuilder<TModel>(htmlHelper);
        }
    }
}