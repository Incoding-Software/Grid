using System;
using System.Web.Routing;
using System.Web.WebPages;
using Grid.GridParts;
using Grid.Styles;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IGridBuilderOptions<T> where T : class
    {
        IGridBuilderOptions<T> GridCssStyling(string @class);
        IGridBuilderOptions<T> GridCssStyling(BootstrapTable @class);
        IGridBuilderOptions<T> Columns(Action<ColumnsBuilder<T>> builderAction);
        IGridBuilderOptions<T> NextRow(Func<ITemplateSyntax<T>, HelperResult> content);
        IGridBuilderOptions<T> Scrolling(int height);
        IGridBuilderOptions<T> RowHtmlAttr(Func<ITemplateSyntax<T>, HelperResult> template);
        IGridBuilderOptions<T> RowHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilderOptions<T> RowHtmlAttr(object htmlAttributes);
        IGridBuilderOptions<T> RowHtmlAttr(BootstrapClass htmlAttributes);
        IGridBuilderOptions<T> HeadHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilderOptions<T> HeadHtmlAttr(object htmlAttributes);
        IGridBuilderOptions<T> HeadHtmlAttr(BootstrapClass htmlAttributes);
        IGridBuilderOptions<T> NextRowHtmlAttr(RouteValueDictionary htmlAttributes);
        IGridBuilderOptions<T> NextRowHtmlAttr(object htmlAttributes);
        IGridBuilderOptions<T> NextRowHtmlAttr(BootstrapClass htmlAttributes);
        IGridBuilderOptions<T> AjaxGet(string actionString);
        IGridBuilderOptions<T> Sortable();
        IGridBuilderOptions<T> OnBind(Action<IIncodingMetaLanguageCallbackBodyDsl> action);
        IGridBuilderOptions<T> NoRecords(string text);
        IGridBuilderOptions<T> Pageable();
        IGridBuilderOptions<T> Pageable(Selector customPagingTemplate);
        IGridBuilderOptions<T> Pageable(Action<PageableBuilder<T>> builderAction);
    }
}