using System;
using System.Web.Routing;
using System.Web.WebPages;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IColumn<T> where T : class
    {
        IColumn<T> HeaderHtmlAttr(RouteValueDictionary htmlAttributes);
        IColumn<T> HeaderHtmlAttr(object htmlAttributes);
        IColumn<T> HeaderClass(B htmlClass);
        IColumn<T> HeaderClass(string htmlClass);
        IColumn<T> HtmlAttr(RouteValueDictionary htmlAttributes);
        IColumn<T> HtmlAttr(object htmlAttributes);
        IColumn<T> Class(B htmlClass);
        IColumn<T> Class(string htmlClass);
        IColumn<T> Title(string title);
        IColumn<T> Title(Func<object, HelperResult> title);
        IColumn<T> Width(int width);
        IColumn<T> WidthPct(int width);
        IColumn<T> Visible(bool visible);
        IColumn<T> Raw();
        IColumn<T> Sortable(object sortBy, bool sortDefault = false);
    }
}