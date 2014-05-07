using Grid.Attributes;
using GridUI.Persistance;
using GridUI.Queries;

namespace GridUI.Models
{
    #region << Using >>

    

    #endregion

    public class ProductVm
    {
        #region Constructors

        public ProductVm(Product product)
        {
            Id = product.Id.ToString();
            Name = product.Name;
            Price = product.Price.ToString();
            Date = product.Date.ToShortDateString();
            Comment =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.<br /> Integer hendrerit placerat posuere.<br /> Sed vitae condimentum est.";
        }

        #endregion

        #region Properties

        [AutoBind(Hide = true)]
        public string Id { get; set; }

        [AutoBind(Title = "Product Name from attr")]
        public string Name { get; set; }

        [AutoBind(Width = 100, SortBy = GetProductsQuery.SortType.Price, SortDefault = true)]
        public string Price { get; set; }

        [AutoBind(Title = "Date of made from attr", SortBy = GetProductsQuery.SortType.Date)]
        public string Date { get; set; }

        [AutoBind(Raw = true, WidthPct = 35)]
        public string Comment { get; set; }

        #endregion
    }
}