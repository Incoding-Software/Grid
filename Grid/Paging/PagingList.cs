namespace Grid.Paging
{
    #region << Using >>

    using System;
    using System.Collections.Generic;

    #endregion

    public class PagingList : List<PagingModel>
    {
        #region Api Methods

        public static List<PagingModel> ToPaginated(IRoutableQuery query, int totalCount)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);
            var list = new List<PagingModel>();
            for (int i = 0; i < totalPages; i++)
                list.Add(new PagingModel { Text = i.ToString(), Page = i + 1, Active = i == query.Page });

            return list;
        }

        #endregion
    }
}