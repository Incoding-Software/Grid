using System;
using System.Web.Routing;
using System.Web.WebPages;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IColumn<T> where T : class
    {
        IColumn<T> HeadAttr(RouteValueDictionary htmlAttributes);
        IColumn<T> HeadAttr(object htmlAttributes);
        IColumn<T> HeadClass(B htmlClass);
        IColumn<T> HeadClass(string htmlClass);
        IColumn<T> Attr(RouteValueDictionary htmlAttributes);
        IColumn<T> Attr(object htmlAttributes);
        IColumn<T> Class(B htmlClass);
        IColumn<T> Class(string htmlClass);
        IColumn<T> Title(string title);
        IColumn<T> Title(Func<object, HelperResult> title);
        IColumn<T> Width(int width);
        IColumn<T> WidthPct(int width);
        IColumn<T> Visible(bool visible);
        IColumn<T> Raw();
        IColumn<T> Sortable(object sortBy, bool sortDefault = false);
        IColumn<T> DescDefault();
    }
}