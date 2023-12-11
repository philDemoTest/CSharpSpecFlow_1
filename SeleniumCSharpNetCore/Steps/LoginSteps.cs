using NUnit.Framework;
using SeleniumCSharpNetCore.Pages;
using SeleniumCSharpNetCore.Utilities.ResuableControls;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SeleniumCSharpNetCore.Steps
{

    [Binding]
    public class LoginSteps : DriverHelper
    {


        HomePage homePage = new HomePage();
        LoginPage loginPage = new LoginPage();

        ConfigProp _configProp = new ConfigProp();

        [Given(@"I navigate to application test(.*)")]
        public void GivenINavigateToApplicationTest(int p0)
        {
            Driver.Navigate().GoToUrl(_configProp.GetExecSetting("url"));
        }

        [Given(@"I navigate to application")]
        public void GivenINavigateToApplication()
        {
            Driver.Navigate().GoToUrl(_configProp.GetExecSetting("url"));
        }

        [Given(@"I click the Login link")]
        public void GivenIClickTheLoginLink()
        {
            homePage.ClickLogin();
        }

        [Given(@"I enter username and password")]
        public void GivenIEnterUsernameAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.EnterUserNameAndPassword(data.UserName, data.Password);
        }

        [When(@"I click login")]
        public void GivenIClickLogin()
        {
            loginPage.ClickLogin();
        }

        [Then(@"I should see user logged in to the application")]
        public void ThenIShouldSeeUserLoggedInToTheApplication()
        {
            Assert.That(homePage.IsLogOffExist(), Is.True, "Log off button did not displayed");
        }



    }
}
