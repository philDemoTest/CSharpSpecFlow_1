using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Remote;

namespace SeleniumCSharpNetCore.Utilities.ResuableControls
{
    public class DriverConfig : DriverHelper
    {
        ConfigProp _configProp = new ConfigProp();

        public void GetWebDriver()
        {

            string browser = _configProp.GetExecSetting("browser");

            switch (browser)
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("start-maximized");
                    // chromeOptions.AddArguments("--disable-gpu");
                    // chromeOptions.AddArguments("--headless");

                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Console.WriteLine("Setup");
                    Driver = new ChromeDriver(chromeOptions);
                    break;

                case "edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("start-maximized");
                    // edgeOptions.AddArguments("--disable-gpu");
                    // edgeOptions.AddArguments("--headless");

                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Console.WriteLine("Setup");
                    Driver = new EdgeDriver(edgeOptions);
                    break;

                case "gridEdge":
                    EdgeOptions gridEdgeOptions = new EdgeOptions();
                    gridEdgeOptions.AddArguments("start-maximized");
                    gridEdgeOptions.AddArguments("--disable-gpu");
                    gridEdgeOptions.AddArguments("--headless");

                    Driver = new RemoteWebDriver(new Uri(_configProp.GetExecSetting("gridUrl")), gridEdgeOptions);
                    break;

                case "gridChrome":
                    ChromeOptions gridChromeOptions = new ChromeOptions();
                    gridChromeOptions.AddArguments("start-maximized");
                    // gridChromeOptions.AddArguments("--disable-gpu");
                    // gridChromeOptions.AddArguments("--headless");

                    Driver = new RemoteWebDriver(new Uri(_configProp.GetExecSetting("gridUrl")), gridChromeOptions);
                    break;

                // Add more cases if needed

                default:
                    // Handle default case or throw an exception
                    throw new InvalidOperationException($"Unsupported browser: {browser}");
            }




        }

        public void WBdriverQuit()
        {
            Driver.Quit();
        }
    }
}
