using System;
using GridUI.Persistance;
using Incoding.CQRS;

namespace GridUI.Operations
{
    #region << Using >>

    

    #endregion

    public class AddProductCommand : CommandBase
    {
        #region Properties

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        #endregion

        public override void Execute()
        {
            var product = new Product();
            product.Name = Name;
            product.Price = Price;
            product.Date = Date;
            Repository.Save(product);
        }
    }
}