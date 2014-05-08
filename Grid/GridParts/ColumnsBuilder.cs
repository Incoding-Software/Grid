using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using Grid.Interfaces;
using Incoding.Extensions;
using Incoding.MvcContrib;

namespace Grid.GridParts
{
    public class ColumnsBuilder<T> : IColumnsBuilder<T> where T : class
    {
        #region Fields

        readonly GridBuilder<T> _gridBuilder;

        #endregion

        #region Constructors

        public ColumnsBuilder(GridBuilder<T> gridBuilder)
        {
            this._gridBuilder = gridBuilder;
        }

        #endregion

        #region Api Methods

        public IColumn<T> Bound(Expression<Func<T, object>> argExpression)
        {
            var name = argExpression.GetMemberName();
            var column = new Column<T>(name) { Expression = name };
            this._gridBuilder.AddColumn(column);
            return column;
        }

        public IColumn<T> Template(Func<ITemplateSyntax<T>, HelperResult> template)
        {
            var column = new Column<T>("") { Template = template };
            this._gridBuilder.AddColumn(column);
            return column;
        }

        #endregion

    }
}