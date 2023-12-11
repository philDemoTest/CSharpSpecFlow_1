using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using BoDi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using NUnit.Framework.Internal;
using System.ComponentModel;
using System.Configuration;
using OpenQA.Selenium.Edge;
using SeleniumCSharpNetCore.Utilities.ResuableControls;
using NUnit.Framework;
[assembly:Parallelizable(ParallelScope.Fixtures)]
[assembly:LevelOfParallelism(1)]


namespace SeleniumCSharpNetCore.Hooks
{
    [Binding]
    public sealed class Hooks : DriverConfig
    {
        private static ExtentReports _extentReports;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly CustomControl _customControl;
        private ExtentTest _scenario;
      

        ConfigProp _configProp = new ConfigProp();
        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext, CustomControl customControl)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _customControl = customControl;
   
        }

        [BeforeTestRun]
        public static void InitializeExtentReports(ConfigProp _configProp)
        {
            var extentReport =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/extentreport.html";
            _extentReports = new ExtentReports();
            var spark = new ExtentSparkReporter(extentReport);
            _extentReports.AttachReporter(spark);
            _configProp.runCmd();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            GetWebDriver();

        }

    [AfterScenario]
    public void AfterScenario()
    {
            WBdriverQuit();
    }

    [AfterStep]
    public void AfterStep()
    {
            
        var fileName =
            $"{_featureContext.FeatureInfo.Title.Trim()}_{Regex.Replace(_scenarioContext.ScenarioInfo.Title, @"\s", "")}";


        if (_scenarioContext.TestError == null)
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);

                    break;
                case StepDefinitionType.When:
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.Then:
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        else
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                        _scenario
                            .CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                            {
                                Path = _customControl.TakeScreenshotAsPathFull(fileName),
                                //Path = _driverFixture.TakeScreenshotAsPath(fileName),
                                Title = "Error screenshot_" + DateTime.Now.ToString()
                            }) ;

                    break;
                case StepDefinitionType.When:
                    _scenario
                        .CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                        {
                            Path = _customControl.TakeScreenshotAsPathFull(fileName),
                            //Path = _driverFixture.TakeScreenshotAsPath(fileName),
                            Title = "Error screenshot_" + DateTime.Now.ToString()
                        });
                    break;
                case StepDefinitionType.Then:
                    _scenario
                        .CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, new ScreenCapture()
                        {
                            Path = _customControl.TakeScreenshotAsPathFull(fileName),
                            //Path = _driverFixture.TakeScreenshotAsPath(fileName),
                            Title = "Error screenshot_" + DateTime.Now.ToString()
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }



        [AfterTestRun]
        public static void TearDownReport(ConfigProp _configProp)
        {
            _configProp.CloseCmd();
            _extentReports.Flush();

                    }

}



}
