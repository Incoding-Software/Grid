using System;
using System.Linq;
using System.Linq.Expressions;
using GridUI.Persistance;
using Incoding;

namespace GridUI.Specifications
{
    public class UsersByProductWhereSpec : Specification<User>
    {
        private readonly Guid _productId;

        public UsersByProductWhereSpec(Guid productId)
        {
            _productId = productId;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return user => user.Products.Any(r => r.Id == _productId);
        }
    }
}