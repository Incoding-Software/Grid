using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.WebPages;
using Grid.Interfaces;
using Grid.Styles;
using Incoding.Extensions;
using Incoding.MvcContrib;

namespace Grid.GridParts
{
    public class Column<T> : IColumn<T> where T : class
    {
        #region Constructors

        public Column() { }

        public Column(string name)
        {
            this.Name = name;
            this.IsVisible = true;
            this.ColumnWidth = String.Empty;
            this.ColumnWidthPct = String.Empty;
            this.SortBy = null;
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string ColumnWidth { get; set; }

        public string ColumnWidthPct { get; set; }

        public bool IsVisible { get; set; }

        public bool IsRaw { get; set; }

        public object SortBy { get; set; }

        public bool SortDefault { get; set; }

        public Func<ITemplateSyntax<T>, HelperResult> Template { get; set; }

        // public Expression<Func<T, object>> Expression { get; set; }
        public string Expression { get; set; }

        public IDictionary<string, object> ColumnHeaderAttributes { get; set; }

        public IDictionary<string, object> ColumnAttributes { get; set; }

        #endregion

        public IColumn<T> HeaderHtmlAttr(RouteValueDictionary htmlAttributes)
        {
            this.ColumnHeaderAttributes = htmlAttributes;
            return this;
        }

        public IColumn<T> HeaderHtmlAttr(object htmlAttributes)
        {
            return HeaderHtmlAttr(AnonymousHelper.ToDictionary(htmlAttributes));
        }

        public IColumn<T> HeaderHtmlAttr(BootstrapClass htmlAttributes)
        {
            return HeaderHtmlAttr(AnonymousHelper.ToDictionary(new { @class = htmlAttributes.ToLocalization() }));
        }

        public IColumn<T> HtmlAttr(RouteValueDictionary htmlAttributes)
        {
            this.ColumnAttributes = htmlAttributes;
            return this;
        }

        public IColumn<T> HtmlAttr(object htmlAttributes)
        {
            return HtmlAttr(AnonymousHelper.ToDictionary(htmlAttributes));
        }

        public IColumn<T> HtmlAttr(BootstrapClass htmlAttributes)
        {
            return HtmlAttr(AnonymousHelper.ToDictionary(new { @class = htmlAttributes.ToLocalization() }));
        }

        public IColumn<T> Title(string title)
        {
            this.Name = title;
            return this;
        }

        public IColumn<T> Title(Func<object, HelperResult> title)
        {
            this.Name = title.Invoke(null).ToHtmlString();
            return this;
        }

        public IColumn<T> Width(int width)
        {
            this.ColumnWidth = width.ToString();
            return this;
        }

        public IColumn<T> WidthPct(int widthPct)
        {
            this.ColumnWidthPct = widthPct.ToString();
            return this;
        }

        public IColumn<T> Visible(bool visible)
        {
            this.IsVisible = visible;
            return this;
        }

        public IColumn<T> Raw()
        {
            this.IsRaw = true;
            return this;
        }

        public IColumn<T> Sortable(object sortBy, bool sortDefault = false)
        {
            this.SortBy = sortBy;
            this.SortDefault = sortDefault;
            return this;
        }
    }
}