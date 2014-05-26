using System;
using System.Web.Routing;
using System.Web.WebPages;
using Grid.GridParts;
using Grid.Styles;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IGridBuilder<T> where T : class
    {
        IGridBuilder<T> GridClass(string @class);
        IGridBuilder<T> GridClass(BootstrapTable @class);
        IGridBuilder<T> Columns(Action<ColumnsBuilder<T>> builderAction);
        IGridBuilder<T> NextRow(Func<ITemplateSyntax<T>, HelperResult> content);
        IGridBuilder<T> RowHtmlAttr(Func<ITemplateSyntax<T>, HelperResult> template);
        IGridBuilder<T> RowHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilder<T> RowHtmlAttr(object htmlAttributes);
        IGridBuilder<T> RowHtmlAttr(BootstrapClass htmlAttributes);
        IGridBuilder<T> HeadHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilder<T> HeadHtmlAttr(object htmlAttributes);
        IGridBuilder<T> HeadHtmlAttr(BootstrapClass htmlAttributes);
        IGridBuilder<T> NextRowHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilder<T> NextRowHtmlAttr(object htmlAttributes);
        IGridBuilder<T> AjaxGet(string actionString);
        IGridBuilder<T> Sortable();
        IGridBuilder<T> OnBind(Action<IIncodingMetaLanguageCallbackBodyDsl> action);
        IGridBuilder<T> NoRecords(string text);
        IGridBuilder<T> Pageable();
        IGridBuilder<T> Pageable(Selector customPagingTemplate);
        IGridBuilder<T> Pageable(Action<PageableBuilder<T>> builderAction);
    }
}