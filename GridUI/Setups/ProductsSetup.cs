using System;
using System.Linq;
using GridUI.Operations;
using GridUI.Persistance;
using Incoding.CQRS;

namespace GridUI.Setups
{
    #region << Using >>

    

    #endregion

    public class ProductsSetup : ISetUp
    {
        #region Fields

        readonly IDispatcher dispatcher;

        #endregion

        #region Constructors

        public ProductsSetup(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #endregion

        #region ISetUp Members

        public int GetOrder()
        {
            return 1;
        }

        public void Execute()
        {
            if (this.dispatcher.Query(new GetEntitiesQuery<Product>()).Any())
                return;

            for (int i = 0; i < 50; i++)
            {
                this.dispatcher.Push(new AddProductCommand
                                     {
                                             Name = "Продукт " + i,
                                             Price = (decimal)55.5 + (decimal)i,
                                             Date = DateTime.Now.AddDays(-i)
                                     });
            }
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}