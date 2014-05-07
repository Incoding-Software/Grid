namespace Grid.Paging
{
    public interface IRoutableQuery
    {
        int Page { get; set; }
        int PageSize { get; }
        string BuildUrl(int page = 0); 
    }
}