using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace KelliPokerPlanning.Specs
{
    [Binding]
    public class WebConfiguration
    {

        private static IISProcess _iisProcess;
        private static IWebDriver _driver;
        private static string _mainWindow;

        [BeforeTestRun]
        public static void Startup()
        {
            _iisProcess = new IISProcess(8082);
            _iisProcess.Start();
            _driver = Settings.CreateWebDriver();
            _mainWindow = _driver.CurrentWindowHandle;
        }

        [AfterTestRun]
        public static void Shutdown()
        {
            _iisProcess.Stop();
            _iisProcess.Dispose();
            _driver.Dispose();
        }

        [BeforeScenario("web")]
        public void Setup()
        {
            ScenarioContext.Current.Set(_driver);
            _mainWindow = _driver.WindowHandles.Contains(_mainWindow)
                              ? _mainWindow
                              : _driver.CurrentWindowHandle;

            _driver.WindowHandles.Except(new[] {_mainWindow})
                .ToList()
                .ForEach(handle =>
                             {
                                 _driver.SwitchTo().Window(handle);
                                 _driver.Close();
                             });

            _driver.Navigate().GoToUrl("about:blank");
            _driver.Manage().Cookies.DeleteAllCookies();
        }
        
    }
}
