using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace SeleniumCSharpNetCore.Utilities.ResuableControls
{
    public class CustomControl : DriverHelper
    {

        public static void ComboBox(string controlName, string value)
        {
            IWebElement comboControl = Driver.FindElement(By.XPath($"//input[@id='{controlName}-awed']"));
            comboControl.Clear();
            comboControl.SendKeys(value);
            Driver.FindElement(By.XPath($"//div[@id='{controlName}-dropmenu']//li[text()='{value}']")).Click();
        }
      
        public string TakeScreenshotAsPathFull(string fileName)
        {
            var screenshot = Driver.TakeScreenshot();
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";
            screenshot.SaveAsFile(path);
            return path;
        }
    }



}
