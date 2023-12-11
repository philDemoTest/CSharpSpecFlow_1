using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCSharpNetCore.Utilities.ResuableControls
{
    public static class WebElementExtensions
    {
        public static void EnterText(this IWebElement webElement, string value)
        {
            webElement.WaitElement();
            webElement.SendKeys(value);
        }

        public static void ClickItem(this IWebElement webElement)
        {
            webElement.WaitElement();
            webElement.Click();
        }
        public static void SelectByValue(this IWebElement webElement, string value)
        {
            webElement.WaitElement();
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByValue(value);
        }

        public static void SelectByText(this IWebElement webElement, string text)
        {
            webElement.WaitElement();
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByText(text);
        }



         public static void WaitElement(this IWebElement webElement)
        {
          
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    // If the element is visible, break out of the loop
                    if (webElement.Displayed)
                    {
                        Console.WriteLine("Element is visible!");
                        break;
                    }

                    // Wait for 1 second before the next iteration
                    System.Threading.Thread.Sleep(1000);
                }
                catch (WebDriverTimeoutException)
                {
                    // Handle timeout exception if needed
                    Console.WriteLine($"Timeout: Element not visible after {i + 1} attempts.");
                }

            }

 
        }
    }

}

