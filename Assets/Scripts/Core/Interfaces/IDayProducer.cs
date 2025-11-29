public interface IDayProducer
{
    System.Action<IEventHandler> OnEventTriggered { get; }
    void StartScheduler();
    void StopScheduler();
}
