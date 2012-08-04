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
            _instance = new RavenInstance();
        }

        [AfterScenario("db")]
        public void TearDown()
        {
            _instance.Dispose();
        }

    }
}
