public interface IFood
{
    bool IsReady { get; }
    string Name { get; }
    void SetReady(bool conditon);
}
