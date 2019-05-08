namespace TelemetryTests.SupportedGames
{
    public class EventPublisher
    {
        public delegate void OnEvent();
        public event OnEvent TestEvent;

        public void RaiseEvent()
        {
            TestEvent?.Invoke();
        }
    }
}