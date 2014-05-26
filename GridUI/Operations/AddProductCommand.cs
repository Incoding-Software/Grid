using System;
using System.Collections.Generic;
using GridUI.Persistance;
using Incoding.CQRS;
using NHibernate.Mapping;

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

        public bool IsSoldOut { get; set; }

        public List<User> Users { get; set; }

        #endregion

        public override void Execute()
        {
            var product = new Product();
            product.Name = Name;
            product.Price = Price;
            product.Date = Date;
            product.IsSoldOut = IsSoldOut;
            product.Users = Users;
            Repository.Save(product);
        }
    }
}