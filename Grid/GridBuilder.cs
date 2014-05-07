using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;
using System.Web.WebPages;
using Grid.Attributes;
using Grid.GridParts;
using Grid.Interfaces;
using Grid.Options;
using Grid.Paging;
using Grid.Styles;
using Incoding.Extensions;
using Incoding.MvcContrib;

namespace Grid
{
    #region << Using >>

    

    #endregion

    public class GridBuilder<T> : IGridBuilder<T>, IHtmlString where T : class
    {
        #region Constructors

        public GridBuilder(HtmlHelper htmlHelper)
        {
            this._htmlHelper = htmlHelper;
        }

        #endregion

        #region Fields

        readonly HtmlHelper _htmlHelper;

        string templateDiv = "templateDiv_" + Guid.NewGuid();

        string templateId = "templateId_" + Guid.NewGuid();

        string pagingTemplateId = "pagingTemplateId_" + Guid.NewGuid();

        string noRecords = "noRecords_" + Guid.NewGuid();

        string sortBySelector = "sortBySelector_" + Guid.NewGuid();

        string descSelector = "descSelector_" + Guid.NewGuid();

        string pagingContainer = "pagingContainer_" + Guid.NewGuid();

        string gridClass;

        string idMainDiv;

        string noRecordsTemplate;

        string arrowsType;

        bool isPageable;

        bool customColumns;

        IDictionary<string, object> RowAttributes;

        IDictionary<string, object> HeaderRowAttributes;

        Func<ITemplateSyntax<T>, HelperResult> RowTemplateAttributes;

        readonly List<Column<T>> _сolumnList = new List<Column<T>>();

        List<Row<T>> rowList = new List<Row<T>>();

        string ajaxGetAction;

        Selector customPagingTemplate;

        Action<IIncodingMetaLanguageCallbackBodyDsl> onBindAction = dsl => {};

        #endregion

        #region Api Methods

        public IGridBuilder<T> GridClass(string @class)
        {
            this.gridClass = @class;
            return this;
        }

        public IGridBuilder<T> GridClass(BootstrapTable @class)
        {
            return GridClass(@class.ToLocalization());
        }

        public IGridBuilder<T> Columns(Action<ColumnsBuilder<T>> builderAction)
        {
            customColumns = true;
            builderAction(new ColumnsBuilder<T>(this));
            return this;
        }

        public IGridBuilder<T> NextRow(Func<ITemplateSyntax<T>, HelperResult> content)
        {
            this.rowList.Add(new Row<T>(content));
            return this;
        }

        //for tbody tr
        public IGridBuilder<T> RowHtmlAttr(Func<ITemplateSyntax<T>, HelperResult> template)
        {
            this.RowTemplateAttributes = template;
            return this;
        }

        public IGridBuilder<T> RowHtmlAttr(RouteValueDictionary htmlAttributes)
        {
            this.RowAttributes = htmlAttributes;
            return this;
        }

        public IGridBuilder<T> RowHtmlAttr(object htmlAttributes)
        {
            return RowHtmlAttr(AnonymousHelper.ToDictionary(htmlAttributes));
        }

        public IGridBuilder<T> RowHtmlAttr(BootstrapClass htmlAttributes)
        {
            return RowHtmlAttr(AnonymousHelper.ToDictionary(new { @class = htmlAttributes.ToLocalization() }));
        }

        //end for tbody tr


        //for thead tr
        public IGridBuilder<T> HeadHtmlAttr(RouteValueDictionary htmlAttributes)
        {
            this.HeaderRowAttributes = htmlAttributes;
            return this;
        }

        public IGridBuilder<T> HeadHtmlAttr(object htmlAttributes)
        {
            return HeadHtmlAttr(AnonymousHelper.ToDictionary(htmlAttributes));
        }

        public IGridBuilder<T> HeadHtmlAttr(BootstrapClass htmlAttributes)
        {
            return HeadHtmlAttr(AnonymousHelper.ToDictionary(new { @class = htmlAttributes.ToLocalization() }));
        }

        //end for thead tr


        public IGridBuilder<T> AjaxGet(string actionString)
        {
            this.ajaxGetAction = actionString.AppendToQueryString(new
                                                     {
                                                         Desc = Selector.Jquery.Name(this.descSelector),
                                                         SortBy = Selector.Jquery.Name(this.sortBySelector)
                                                     });
            return this;
        }

        public IGridBuilder<T> OnBind(Action<IIncodingMetaLanguageCallbackBodyDsl> action)
        {
            this.onBindAction = action;
            return this;
        }

        public IGridBuilder<T> NoRecordsTemplate(string text)
        {
            var div = new TagBuilder("div");
            div.GenerateId(this.noRecords);
            div.Attributes.Add("style", "display: none;");
            var table = new TagBuilder("table");
            var tbody = new TagBuilder("tbody");
            var tr = new TagBuilder("tr");
            var td = new TagBuilder("td");
            td.InnerHtml = text;
            tr.InnerHtml = td.ToString();
            tbody.InnerHtml = tr.ToString();
            table.InnerHtml = tbody.ToString();
            div.InnerHtml = table.ToString();

            this.noRecordsTemplate = div.ToString();
            return this;
        }

        public IGridBuilder<T> Pageable()
        {
            this.isPageable = true;
            return this;
        }

        public IGridBuilder<T> Pageable(Selector customPagingTemplate)
        {
            this.customPagingTemplate = customPagingTemplate;
            return Pageable();
        }

        #endregion
        
        #region PrivateMethods

        string NoRecordsTemplateDefault()
        {
            NoRecordsTemplate(GridOptions.Default.NoRecordsText);
            return this.noRecordsTemplate;
        }

        MvcHtmlString Render()
        {
            var divMain = new TagBuilder("div");
            divMain.AddCssClass("inc-grid");

            var table = new TagBuilder("table");
            if (!string.IsNullOrWhiteSpace(this.gridClass))
                table.AddCssClass(this.gridClass);
            table.AddCssClass("table");


            var thead = new TagBuilder("thead");
            var tr = new TagBuilder("tr");

            if (!customColumns)
                AutoBind();

            foreach (var column in this._сolumnList)
                tr.InnerHtml += AddTh(column);

            thead.InnerHtml = tr.ToString();
            table.InnerHtml = thead.ToString();
            divMain.InnerHtml = table.ToString();

            divMain.InnerHtml += AddTemplate();
            divMain.InnerHtml += this.noRecordsTemplate ?? this.NoRecordsTemplateDefault();

            if (this._сolumnList.Any(r => r.SortBy != null))
            {
                divMain.InnerHtml += this._htmlHelper.Hidden(this.sortBySelector, "");
                divMain.InnerHtml += this._htmlHelper.CheckBox(this.descSelector, true, new { style = "display: none;" });
            }

            return new MvcHtmlString(divMain.ToString());
        }

        private void AutoBind()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo propInfo in properties)
            {
                var attrs = propInfo.FirstOrDefaultAttribute<AutoBindAttribute>();
                AddColumn(new Column<T>(propInfo.Name)
                {
                    Expression = propInfo.Name,
                    ColumnWidth = attrs != null ? 
                                  attrs.Width == 0 ? String.Empty : attrs.Width.ToString() :
                                  String.Empty,
                   
                    ColumnWidthPct = attrs != null ?
                                     attrs.WidthPct == 0 ? String.Empty : attrs.WidthPct.ToString() :
                                     String.Empty,

                    IsRaw = attrs != null && attrs.Raw,
                    IsVisible = attrs == null || !attrs.Hide,
                    Name = attrs != null ? 
                           attrs.Title ?? propInfo.Name :
                           propInfo.Name,

                    SortBy = attrs != null ? attrs.SortBy : null,
                    SortDefault = attrs != null && attrs.SortDefault
                });     
            }
                 
        }

        MvcHtmlString AddTh(Column<T> column)
        {
            var attributes = column.ColumnHeaderAttributes ?? column.ColumnAttributes;
            var thWidth = String.IsNullOrWhiteSpace(column.ColumnWidthPct) ? column.ColumnWidth : column.ColumnWidthPct;

            var th = new TagBuilder("th");

            if (column.SortBy != null)
            {
                var link = SortArrow(setting =>
                {
                    setting.Content = column.Name;
                    setting.TargetId = this.templateDiv;
                    setting.By = column.SortBy;
                    setting.SortDefault = column.SortDefault;
                });

                th.InnerHtml = link.ToString();
            }
            else
                th.InnerHtml = column.Name;

            //thead tr attributes first
            if (this.HeaderRowAttributes != null)
                th.MergeAttributes(this.HeaderRowAttributes);

            //unique th attributes
            if (attributes != null)
                th.MergeAttributes(attributes, true);

            if (!String.IsNullOrWhiteSpace(thWidth) && (attributes == null || !attributes.ContainsKey("style")))
                th.MergeAttribute("style", String.Format("width:{0}{1};", thWidth, String.IsNullOrWhiteSpace(column.ColumnWidthPct) ? "px" : "%"));

            if (!column.IsVisible)
                th.AddCssClass("hide");

            return new MvcHtmlString(th.ToString());
        }

        MvcHtmlString SortArrow(Action<SortArrowSetting> action)
        {
            var setting = new SortArrowSetting();
            action(setting);

            var link = this._htmlHelper.When(JqueryBind.Click)
                    .DoWithPreventDefaultAndStopPropagation().Direct()
                    .OnSuccess(dsl =>
                    {
                        dsl.With(selector => selector.Name(this.sortBySelector)).Core().JQuery.Attributes.Val(setting.By);
                        dsl.With(selector => selector.Name(this.descSelector)).Core().Trigger.Invoke(JqueryBind.Click);
                        dsl.With(selector => selector.Class("sort-arrow")).Core().Trigger.Incoding();
                        dsl.With(selector => selector.Id(setting.TargetId)).Core().Trigger.Incoding();
                    })
                    .AsHtmlAttributes()
                    .ToLink(setting.Content);

            var builder = new StringBuilder();
            builder.Append(link);
            builder.Append(RenderSortArrow(setting.By, true, setting.SortDefault));
            builder.Append(RenderSortArrow(setting.By, false, setting.SortDefault));
            return new MvcHtmlString(builder.ToString());
        }


        MvcHtmlString RenderSortArrow<TEnum>(TEnum sort, bool desc, bool sortDefault)
        {
            string arrowsDefault = "inc-icon " + (desc ? "inc-arrow-up" : "inc-arrow-down");

            string arrowsBootstrap = desc ? "icon-arrow-up" : "icon-arrow-down";

            return this._htmlHelper.When("initincoding resetgrid")
                    .DoWithStopPropagation().Direct()
                    .OnSuccess(dsl =>
                    {
                        dsl.Self().Core().JQuery.Attributes.AddClass("hide");
                        dsl.Self().Core().JQuery.Attributes.RemoveClass("hide")
                                .If(builder => builder
                                        .Is(() => Selector.Jquery.Name(this.sortBySelector) == sort.ToString())
                                        .And
                                        .Is(() => Selector.Jquery.Name(this.descSelector) == desc)
                                );

                        dsl.Self().Core().JQuery.Attributes.RemoveClass("hide")
                                .If(builder => builder
                                        .Is(() => Selector.Jquery.Name(this.sortBySelector) == "")
                                        .And
                                        .Is(() => Selector.Jquery.Name(this.descSelector) == desc)
                                        .And
                                        .Is(() => sortDefault == true));

                        dsl.Self().Core().JQuery.Attributes.AddClass("hide")
                                .If(builder => builder
                                        .Is(() => Selector.Jquery.Name(this.sortBySelector) == sort.ToString())
                                        .And
                                        .Is(() => Selector.Jquery.Name(this.descSelector) != desc)
                                );
                    })
                                .AsHtmlAttributes(new { @class = (GridOptions.Default.IsArrowsBootstrap() ? arrowsBootstrap : arrowsDefault) + " sort-arrow hide" })
                                .ToTag(GridOptions.Default.IsArrowsBootstrap() ? HtmlTag.I : HtmlTag.Span);
        }

        MvcHtmlString AddTemplate()
        {
            var divPageable = this._htmlHelper.When(JqueryBind.InitIncoding | JqueryBind.IncChangeUrl)
                    .DoWithPreventDefaultAndStopPropagation()
                    .AjaxHashGet(this.ajaxGetAction)
                    .OnSuccess(dsl =>
                    {
                        dsl.Self().Core().Insert.For<PagingResult<T>>(result => result.Items)
                                .WithTemplate(Selector.Jquery.Id(this.templateId)).Html();

                        dsl.With(selector => selector.Id(pagingContainer)).Core().Insert.For<PagingResult<T>>(result => result.Paging)
                                .WithTemplate(customPagingTemplate != null ? customPagingTemplate : Selector.Jquery.Id(this.pagingTemplateId)).Html();

                        dsl.Self().Core().JQuery.Manipulation.Html(Selector.Jquery.Id(this.noRecords).Text())
                                .If(r => r.Data<List<T>>(data => data.IsEmpty()));
                    })
                    .OnSuccess(onBindAction)
                    .OnError(dsl => dsl.Self().Core().JQuery.Manipulation.Html("Error ajax get"))
                    .AsHtmlAttributes(new { id = this.templateDiv, @class = "inc-grid-content" })
                    .ToDiv();
            
            var div = this._htmlHelper.When(JqueryBind.InitIncoding)
                    .DoWithPreventDefaultAndStopPropagation()
                    .AjaxGet(this.ajaxGetAction)
                    .OnSuccess(dsl =>
                    {
                        dsl.Self().Core().Insert.WithTemplate(Selector.Jquery.Id(this.templateId)).Html();

                        dsl.Self().Core().JQuery.Manipulation.Html(Selector.Jquery.Id(this.noRecords).Text())
                                .If(r => r.Data<List<T>>(data => data.IsEmpty()));
                    })
                    .OnSuccess(onBindAction)
                    .OnError(dsl => dsl.Self().Core().JQuery.Manipulation.Html("Error ajax get"))
                    .AsHtmlAttributes(new { id = this.templateDiv, @class = "inc-grid-content" })
                    .ToDiv();

            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                var writerHtmlHelper = new HtmlHelper<T>(new ViewContext
                {
                    Writer = new HtmlTextWriter(writer)
                }, new ViewPage());

                using (var template = writerHtmlHelper.Incoding().ScriptTemplate<T>(this.templateId))
                {
                    if (!string.IsNullOrWhiteSpace(this.gridClass))
                        sb.Append("<table class=\"table " + this.gridClass + "\">");
                    else
                        sb.Append("<table class=\"table\">");

                    using (var each = template.ForEach())
                    {
                        var tbody = new TagBuilder("tbody");
                        var tr = new TagBuilder("tr");

                        if (this.RowAttributes != null)
                            tr.MergeAttributes(this.RowAttributes);

                        if (this.RowTemplateAttributes != null)
                            tr.AddCssClass(this.RowTemplateAttributes.Invoke(each).ToString());

                        foreach (var column in this._сolumnList)
                        {
                            var tdWidth = String.IsNullOrWhiteSpace(column.ColumnWidthPct) ? column.ColumnWidth : column.ColumnWidthPct;
                            var td = new TagBuilder("td");

                            if (column.Expression != null)
                            {
                                if (column.IsRaw)
                                    td.InnerHtml = "{{{" + column.Expression + "}}}";
                                else
                                    td.InnerHtml = "{{" + column.Expression + "}}";
                            }

                            if (column.Template != null)
                                td.InnerHtml += column.Template.Invoke(each);

                            if (column.ColumnAttributes != null)
                                td.MergeAttributes(column.ColumnAttributes);

                            if (!String.IsNullOrWhiteSpace(tdWidth) && (column.ColumnAttributes == null || !column.ColumnAttributes.ContainsKey("style")))
                                td.MergeAttribute("style", String.Format("width:{0}{1};", tdWidth, String.IsNullOrWhiteSpace(column.ColumnWidthPct) ? "px" : "%"));

                            if (!column.IsVisible)
                                td.AddCssClass("hide");

                            tr.InnerHtml += td.ToString();
                        }

                        tbody.InnerHtml += tr.ToString();

                        if (this.rowList.Count > 0)
                        {
                            foreach (var row in this.rowList)
                            {
                                var trNext = new TagBuilder("tr");
                                if (this.RowAttributes != null)
                                {
                                    foreach (var att in this.RowAttributes)
                                    {
                                        if (att.Key == "class")
                                            trNext.AddCssClass(att.Value.ToString());
                                    }
                                    trNext.MergeAttributes(this.RowAttributes);
                                }
                                if (this.RowTemplateAttributes != null)
                                    trNext.AddCssClass(this.RowTemplateAttributes.Invoke(each).ToString());

                                trNext.InnerHtml = row.Content.Invoke(each).ToString();
                                tbody.InnerHtml += trNext.ToString();
                            }
                        }

                        sb.Append(tbody);
                    }
                    sb.Append("</table>");
                }

                if (isPageable)
                {
                    if (customPagingTemplate == null)
                    {
                     using (var template = writerHtmlHelper.Incoding().ScriptTemplate<PagingModel>(this.pagingTemplateId))
                        {
                            sb.Append("<ul class=\"pagination\">");
                            using (var each = template.ForEach())
                            {
                                using (each.Is(r => r.Active))
                                {
                                    var li = new TagBuilder("li");
                                    var link = new TagBuilder("a");
                                    link.MergeAttribute("href", "#!" + each.For(r => r.Url));
                                    link.InnerHtml = each.For(r => r.Text);
                                    li.InnerHtml = link.ToString();
                                    li.AddCssClass("active");
                                    sb.Append(li);
                                }
                                using (each.Not(r => r.Active))
                                {
                                    var li = new TagBuilder("li");
                                    var link = new TagBuilder("a");
                                    link.MergeAttribute("href", "#!" + each.For(r => r.Url));
                                    link.InnerHtml = each.For(r => r.Text);
                                    li.InnerHtml = link.ToString();
                                    sb.Append(li);
                                }

                            }
                        }
                        sb.Append("</ul>");
                    }
                }
            }

            var divContent = new TagBuilder("div");
            divContent.InnerHtml = isPageable ? divPageable.ToHtmlString() : div.ToHtmlString();
            divContent.InnerHtml += sb.ToString();
            if (isPageable)
            {
                var divPagingContainer = new TagBuilder("div");
                divPagingContainer.GenerateId(pagingContainer);
                divPagingContainer.AddCssClass("pagination");
                divContent.InnerHtml += divPagingContainer.ToString();
            }
                

            return new MvcHtmlString(divContent.ToString());
        }

        #endregion

        public void AddColumn(Column<T> column)
        {
            this._сolumnList.Add(column);
        }

        public string ToHtmlString()
        {
            return Render().ToString();
        }
    }

}