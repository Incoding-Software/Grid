namespace Grid.Paging
{
    #region << Using >>

    using System.Collections.Generic;

    #endregion

    public class PagingResult<TModel>
    {
        #region Constructors

        public PagingResult(List<TModel> items, IRoutableQuery query, int totalCount)
        {
            Items = items;
            int page = query.Page == 0 ? 1 : query.Page;
            int startRows = (query.PageSize * (page - 1) + 1);
            int endRows = query.PageSize * page;
            PagingRange = "<div class=\"itemscount\">" + startRows + " - " + (endRows > totalCount ? totalCount : endRows) + " of " + totalCount + " items</div>";
            Paging = new PagingContainer()
                     {
                             Items = PagingList.ToPaginated(query, totalCount),
                             Total = totalCount.ToString(),
                             Start = startRows.ToString(),
                             End = endRows.ToString()
                     };
        }

        #endregion

        #region Properties

        public List<TModel> Items { get; set; }

        public PagingContainer Paging { get; set; }

        public string PagingRange { get; set; }

        #endregion
    }
}