﻿using System.Web.Routing;
using Grid.Styles;

namespace Grid.Interfaces
{
    public interface IColumn<T> where T : class
    {
        IColumn<T> HeaderHtmlAttr(RouteValueDictionary htmlAttributes);
        IColumn<T> HeaderHtmlAttr(object htmlAttributes);
        IColumn<T> HeaderHtmlAttr(BootstrapClass htmlAttributes);
        IColumn<T> HtmlAttr(RouteValueDictionary htmlAttributes);
        IColumn<T> HtmlAttr(object htmlAttributes); 
        IColumn<T> HtmlAttr(BootstrapClass htmlAttributes);
        IColumn<T> Title(string title);
        IColumn<T> Width(int width);
        IColumn<T> WidthPct(int width);
        IColumn<T> Visible(bool visible);
        IColumn<T> Raw();
        IColumn<T> Sortable(object sortBy, bool sortDefault = false);

    }
}