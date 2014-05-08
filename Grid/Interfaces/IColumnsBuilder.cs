using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IColumnsBuilder<T> where T : class
    {
        IColumn<T> Bound(Expression<Func<T, object>> argExpression);
        IColumn<T> Template(Func<ITemplateSyntax<T>, HelperResult> template);
    }
}