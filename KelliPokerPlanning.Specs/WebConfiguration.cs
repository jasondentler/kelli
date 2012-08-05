using System.Configuration;
using System.Linq;
using NUnit.Framework;
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
            if (Settings.IsRunningAppHarborTest)
                return;

            _iisProcess = new IISProcess(8082);
            _iisProcess.Start();
            _driver = Settings.CreateWebDriver();
            _mainWindow = _driver.CurrentWindowHandle;
        }

        [AfterTestRun]
        public static void Shutdown()
        {
            if (Settings.IsRunningAppHarborTest)
                return;

            _iisProcess.Stop();
            _iisProcess.Dispose();
            _driver.Dispose();
        }

        [BeforeScenario("web")]
        public void Setup()
        {
            if (Settings.IsRunningAppHarborTest)
            {
                Assert.Ignore();
                return;
            }

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
