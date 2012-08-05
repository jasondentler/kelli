using TechTalk.SpecFlow;

namespace KelliPokerPlanning.Specs
{
    [Binding]
    public class DatabaseConfiguration
    {

        private RavenInstance _instance;

        [BeforeScenario("db")]
        public void Setup()
        {
            if (Settings.IsRunningAppHarborTest)
                return;

            _instance = new RavenInstance();
        }

        [AfterScenario("db")]
        public void TearDown()
        {
            if (Settings.IsRunningAppHarborTest)
                return;

            _instance.Dispose();
        }

    }
}
