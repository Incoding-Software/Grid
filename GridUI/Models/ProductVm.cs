using System;
using Grid.Attributes;
using GridUI.Persistance;
using GridUI.Queries;

namespace GridUI.Models
{
    public class ProductVm
    {
        #region Constructors

        public ProductVm(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price.ToString();
            Date = product.Date.ToShortDateString();
            IsSoldOut = product.IsSoldOut;
            Comment =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.<br /> Integer hendrerit placerat posuere.<br /> Sed vitae condimentum est.";
        }

        #endregion

        #region Properties

        [AutoBind(Hide = true)]
        public Guid Id { get; set; }

        [AutoBind(Title = "Product Name")]
        public string Name { get; set; }

        [AutoBind(Width = 100, SortBy = GetProductsQuery.SortType.Price, SortDefault = true)]
        public string Price { get; set; }

        [AutoBind(Title = "Date of made", SortBy = GetProductsQuery.SortType.Date)]
        public string Date { get; set; }

        [AutoBind(Raw = true, WidthPct = 35)]
        public string Comment { get; set; }

        [AutoBind(Title = "Status")]
        public bool IsSoldOut { get; set; }

        #endregion
    }
}