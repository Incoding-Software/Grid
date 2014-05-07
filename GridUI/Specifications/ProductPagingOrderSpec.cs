using System;
using GridUI.Persistance;
using GridUI.Queries;
using Incoding.Data;

namespace GridUI.Specifications
{
    #region << Using >>

    

    #endregion

    public class ProductPagingOrderSpec : OrderSpecification<Product>
    {
        #region Fields

        readonly GetProductsPagingQuery.SortType _sortBy;

        readonly OrderType orderType;

        #endregion

        #region Constructors

        public ProductPagingOrderSpec(GetProductsPagingQuery.SortType sortBy, bool desc)
        {
            this._sortBy = sortBy;
            this.orderType = desc ? OrderType.Descending : OrderType.Ascending;
        }

        #endregion

        public override Action<AdHocOrderSpecification<Product>> SortedBy()
        {
            switch (this._sortBy)
            {
                case GetProductsPagingQuery.SortType.Name:
                    return specification => specification.Order(r => r.Name, this.orderType);

                case GetProductsPagingQuery.SortType.Price:
                    return specification => specification.Order(r => r.Price, this.orderType);

                default:
                    return specification => specification.Order(r => r.Name, this.orderType);
            }
        }
    }
}