using System;
using System.Collections;
using System.Collections.Generic;
using Incoding.Data;
using Incoding.Quality;
using JetBrains.Annotations;

namespace GridUI.Persistance
{
    public class User : IncEntityBase
    {
        #region Properties

        public new virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual IList<Product> Products { get; set; }


        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor)]
        public class UserMap : NHibernateEntityMap<User>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected UserMap()
            {
                Id(r => r.Id).GeneratedBy.Guid();
                Map(r => r.FirstName);
                Map(r => r.LastName);
                HasManyToMany(r => r.Products);
            }

            #endregion

            ////ncrunch: no coverage end
        }

        #endregion
    }
}