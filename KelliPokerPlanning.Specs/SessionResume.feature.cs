﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.544
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace KelliPokerPlanning.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Session Resume")]
    public partial class SessionResumeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SessionResume.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Session Resume", "In order to moderate a session\r\nAs a moderator\r\nI want to resume an existing sess" +
                    "ion", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Display accounts")]
        [NUnit.Framework.CategoryAttribute("web")]
        [NUnit.Framework.CategoryAttribute("db")]
        public virtual void DisplayAccounts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Display accounts", new string[] {
                        "web",
                        "db"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have browsed to the home page");
#line 9
 testRunner.When("I click \"Log in with Stack Exchange\"");
#line 10
 testRunner.Then("my 4 accounts are listed");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View list of available sessions")]
        [NUnit.Framework.CategoryAttribute("web")]
        [NUnit.Framework.CategoryAttribute("db")]
        public virtual void ViewListOfAvailableSessions()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View list of available sessions", new string[] {
                        "web",
                        "db"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
 testRunner.Given("I have an active session");
#line 15
 testRunner.And("I have browsed to the home page");
#line 16
 testRunner.And("I have clicked \"Log in with Stack Exchange\"");
#line 17
 testRunner.When("I click the Stack Overflow account");
#line 18
 testRunner.Then("I get redirected to the session list page");
#line 19
 testRunner.And("1 session is available to resume");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Select an existing session")]
        [NUnit.Framework.CategoryAttribute("web")]
        [NUnit.Framework.CategoryAttribute("db")]
        public virtual void SelectAnExistingSession()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Select an existing session", new string[] {
                        "web",
                        "db"});
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I have an active session");
#line 24
 testRunner.And("I have browsed to the home page");
#line 25
 testRunner.And("I have clicked \"Log in with Stack Exchange\"");
#line 26
 testRunner.And("I have clicked the Stack Overflow account");
#line 27
 testRunner.When("I choose the existing session to resume");
#line 28
 testRunner.Then("I am redirected to the session index page");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion