public class ExitEventHandler : IEventHandler
{
    private Nps _npsData;

    public void Dispose()
    {
        _npsData = null;
    }

    public void HandleEvent(NpsBehaviorLogic controller)
    {
        _npsData = controller.GetNpsData();

        controller.ChangeState(new ExitState(NpsWalkPositions.NpsWalkInstance.PositionsToWalk[1]));
        Dispose();
    }
}
