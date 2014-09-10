using System;
using System.Collections.Generic;
using Incoding.Data;
using Incoding.Quality;
using JetBrains.Annotations;

namespace GridUI.Persistance
{
    public class Product : IncEntityBase
    {
        #region Properties

        public new virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual bool IsSoldOut { get; set; }

        public virtual IList<User> Users{ get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor)]
        public class ProductMap : NHibernateEntityMap<Product>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected ProductMap()
            {
                Id(r => r.Id).GeneratedBy.Guid();
                Map(r => r.Name).Length(100);
                Map(r => r.Price);
                Map(r => r.Date);
                Map(r => r.IsSoldOut);
                HasManyToMany(r => r.Users);
            }

            #endregion

            ////ncrunch: no coverage end
        }

        #endregion
    }
}