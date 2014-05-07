using System.Collections.Generic;

namespace Grid.Paging
{
    public class PagingResult<TModel>
    {
        #region Constructors

        public PagingResult(List<TModel> items, IRoutableQuery query, int totalCount)
        {
            Items = items;
            Paging = PagingList.ToPaginated(query, totalCount);
        }

        #endregion

        #region Properties

        public List<TModel> Items { get; set; }

        public List<PagingModel> Paging { get; set; }

        #endregion
    }
}