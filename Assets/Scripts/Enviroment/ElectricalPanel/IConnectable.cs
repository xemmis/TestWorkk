public interface IConnectable
{
    // Метод для получения состояния от щитка
    void OnPowerStateChanged(bool isPowered);
    bool IsPowered { get; }
    // Опционально: можно добавить идентификатор
    string ConnectionID { get; }
}
