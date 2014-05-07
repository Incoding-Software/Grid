namespace Grid.Interfaces
{
    public interface ISortable<TSortBy>
    {
        TSortBy SortBy { get; set; }
        bool Desc { get; set; }
    }
}