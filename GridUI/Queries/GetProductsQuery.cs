using System.Collections.Generic;
using System.Linq;
using Grid.Interfaces;
using GridUI.Models;
using GridUI.Specifications;
using Incoding.CQRS;

namespace GridUI.Queries
{
    public class GetProductsQuery : QueryBase<List<ProductVm>>, ISortable<GetProductsQuery.SortType>
    {
        #region ISortable<SortType> Members

        public SortType SortBy { get; set; }
        public bool Desc { get; set; }

        #endregion

        #region Enums

        public enum SortType
        {
            Name,
            Price,
            Date
        }

        #endregion

        protected override List<ProductVm> ExecuteResult()
        {
            return Repository.Query(orderSpecification: new ProductOrderSpec(SortBy, Desc)).ToList().Take(10)
                    .Select(r => new ProductVm(r)).ToList();
        }
    }
}