using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordlePOM
{
    public class PageObjectModel
    {
        private const string WordleURL = "https://www.nytimes.com/games/wordle/index.html";
        IWebDriver WebDriver;
        public PageObjectModel()
        {
            WebDriver = new FirefoxDriver();
        }


        public void NavigateToPage()
        {
            WebDriver.Navigate().GoToUrl(WordleURL);
        }


        public void RejectCookies()
        {
            IWebElement element = WebDriver.FindElement(By.Id("pz-gdpr-btn-reject"));
            
            element.Click();
        }


        public void CloseHowToPlay()
        {
            IWebElement element = WebDriver.FindElement(By.ClassName("game-icon"));

            element.Click();
        }


    }

}