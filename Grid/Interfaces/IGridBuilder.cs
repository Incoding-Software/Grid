
namespace Grid.Interfaces
{
    public interface IGridBuilder<T> where T : class
    {
        IGridBuilderOptions<T> Name(string name);
    }
}