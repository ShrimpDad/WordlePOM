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

        public void SelectLetter(char letter)
        {
            char lowerCaseLetter = Char.ToLower(letter);
            
            IWebElement element = WebDriver.FindElement(By.XPath($"//button[text()='{lowerCaseLetter}']"));
            
            element.Click();
        }

        public void SelectEnter()
        {
            IWebElement element = WebDriver.FindElement(By.XPath("//button[text()='enter']"));

            element.Click();
        }

        public void SelectBackspace()
        {
            IWebElement element = WebDriver.FindElement(By.XPath("//button[@aria-label='backspace']"));

            element.Click();
        }

        public void EnterWord(string word)
        {
            for (int i = 0; i < 5; i++)
            {
                SelectLetter(word[i]);
            }

            SelectEnter();
        }

    }

}