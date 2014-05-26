using System;
using System.Collections.Generic;
using System.Linq;
using GridUI.Operations;
using GridUI.Persistance;
using GridUI.Queries;
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

            if (this.dispatcher.Query(new GetEntitiesQuery<User>()).Any())
                return;
            
            var IgorCmd = new AddUserCommand
                              {
                                  FirstName = "Igor",
                                  LastName = "Valukhov"
                              };

            var VladCmd = new AddUserCommand
                              {
                                  FirstName = "Vlad",
                                  LastName = "Kopachinsky"
                              };

            var VictorCmd = new AddUserCommand
                              {
                                  FirstName = "Victor",
                                  LastName = "Gelmutdinov"
                              };

            dispatcher.Push(IgorCmd);
            dispatcher.Push(VictorCmd);
            dispatcher.Push(VladCmd);

            for (int i = 0; i < 50; i++)
            {
                this.dispatcher.Push(new AddProductCommand
                                     {
                                             Name = "Продукт " + i,
                                             Price = (decimal)55.5 + (decimal)i,
                                             Date = DateTime.Now.AddDays(-i),
                                             IsSoldOut = i%3 == 0,
                                             Users = i%2 == 0 ? 
                                             new List<User>() 
                                             {
                                                 (User)IgorCmd.Result, 
                                                 (User)VictorCmd.Result, 
                                                 (User)VladCmd.Result
                                             } : 
                                             new List<User>() 
                                             {
                                                 (User)IgorCmd.Result, 
                                             } 
                                     });
            }
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}