namespace Application.Ports.Outgoing
{
    public interface IDataMapper<T>
    {
        T? FromJson(string json);
    }
}
