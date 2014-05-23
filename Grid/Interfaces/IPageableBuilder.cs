using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using Incoding.MvcContrib;

namespace Grid.Interfaces
{
    public interface IPageableBuilder<T> where T : class
    {
        IPageableBuilder<T> ItemsCount(bool showItemsCount);

        IPageableBuilder<T> PageSizes(bool showPageSizes);

        IPageableBuilder<T> PageSizes(params int[] pageSizesArray);

        // IPageableBuilder<T> ButtonCount(int count);

    }
}