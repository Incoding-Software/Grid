using System;
using System.Web.WebPages;
using Incoding.MvcContrib;

namespace Grid.GridParts
{
    public class Row<T> where T : class
    {
        #region Constructors

        public Row(Func<ITemplateSyntax<T>, HelperResult> content)
        {
            this.Content = content;
        }

        #endregion

        #region Properties

        public Func<ITemplateSyntax<T>, HelperResult> Content { get; private set; }

        #endregion
    }
}