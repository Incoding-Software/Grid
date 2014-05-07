using System;
using System.Collections.Generic;

namespace Grid.Paging
{
    public class PagingList : List<PagingModel>
    {
        #region Api Methods

        public static List<PagingModel> ToPaginated(IRoutableQuery query, int totalCount)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);
            var list = new List<PagingModel>();
            if (totalPages == 1)
                return list;
            var page = query.Page == 0 ? 1 : query.Page;
            if (page > 1)
                list.Add(new PagingModel { Text = "«", Url = query.BuildUrl(page - 1) });

            for (int i = page - 5; i <= page + 5; i++)
            {
                if (i < 1) continue;
                if (i > totalPages) continue;
                list.Add(new PagingModel { Text = i.ToString(), Url = query.BuildUrl(i), Active = i == page });
            }

            if (page < totalPages)
                list.Add(new PagingModel { Text = "»", Url = query.BuildUrl(page + 1) });

            return list;
        }

        #endregion
    }
}