using System;
using System.Collections.Generic;
using System.Linq;
using Grid.Interfaces;
using GridUI.Models;
using GridUI.Specifications;
using Incoding.CQRS;

namespace GridUI.Queries
{
    public class GetUsersByProductQuery : QueryBase<List<UserVm>>
    {
        public string ProductId { get; set; }
        protected override List<UserVm> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new UsersByProductWhereSpec(Guid.Parse(ProductId))).ToList()
                    .Select(r => new UserVm(r)).ToList();
        }
    }
}