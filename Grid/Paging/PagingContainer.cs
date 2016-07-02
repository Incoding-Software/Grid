namespace Grid.Paging
{
    using System.Collections.Generic;

    public class PagingContainer
    {
        public string Start { get; set; }

        public string End { get; set; }

        public string Total { get; set; }

        public bool IsFirst  { get; set; }

        public bool HasNext { get; set; }
        
        public bool IsLast  { get; set; }

        public List<PagingModel> Items { get; set; }
    }
}