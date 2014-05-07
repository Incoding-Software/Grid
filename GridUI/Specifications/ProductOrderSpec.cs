using System;
using GridUI.Persistance;
using GridUI.Queries;
using Incoding.Data;

namespace GridUI.Specifications
{
    #region << Using >>

    

    #endregion

    public class ProductOrderSpec : OrderSpecification<Product>
    {
        #region Fields

        readonly GetProductsQuery.SortType _sortBy;

        readonly OrderType orderType;

        #endregion

        #region Constructors

        public ProductOrderSpec(GetProductsQuery.SortType sortBy, bool desc)
        {
            this._sortBy = sortBy;
            this.orderType = desc ? OrderType.Descending : OrderType.Ascending;
        }

        #endregion

        public override Action<AdHocOrderSpecification<Product>> SortedBy()
        {
            switch (this._sortBy)
            {
                case GetProductsQuery.SortType.Name:
                    return specification => specification.Order(r => r.Name, this.orderType);

                case GetProductsQuery.SortType.Price:
                    return specification => specification.Order(r => r.Price, this.orderType);

                case GetProductsQuery.SortType.Date:
                    return specification => specification.Order(r => r.Date, this.orderType);

                default:
                    return specification => specification.Order(r => r.Name, this.orderType);
            }
        }
    }
}