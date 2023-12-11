using OpenQA.Selenium;
using SeleniumCSharpNetCore.Utilities.ResuableControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCSharpNetCore.Pages
{
    public class LoginPage : DriverHelper
    {


        IWebElement txtUserName => Driver.FindElement(By.Name("UserName"));
        IWebElement txtPassword => Driver.FindElement(By.Name("Password"));
        IWebElement btnLogin => Driver.FindElement(By.CssSelector(".btn-default"));


        public void EnterUserNameAndPassword(string userName, string password)
        {

          //  CustomControl.EnterText(txtUserName,userName);
          //  CustomControl.EnterText(txtPassword,password);
            txtUserName.EnterText(userName);
            txtPassword.EnterText(password);
          //  txtUserName.SendKeys(userName);
          //  txtPassword.SendKeys(password);
        }

        public void ClickLogin()
        {

           // CustomControl.Click(btnLogin);
            btnLogin.ClickItem();
          //  btnLogin.Click();
        }
    }
}
