using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordlePOM
{
    public class PageObjectModel
    {
        List<LetterState> letterStates = new List<LetterState>();

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

            //WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5.00));

            //wait.Until(ExpectedConditions.ElementToBeClickable(element));

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

            System.Threading.Thread.Sleep(5000);
        }

        public void GetLetterState()
        {
            IReadOnlyCollection<IWebElement> tiles = WebDriver.FindElements(By.XPath("//div[@aria-label='Row 1']//div[@data-testid='tile']"));

            for (int i = 0; i < tiles.Count; i++)
            {
                string letter = tiles.ElementAt(i).Text;
                string state = tiles.ElementAt(i).GetAttribute("data-state");
                int letterPosition = i;

                LetterState LetterState = new LetterState(letter, state, letterPosition);

                letterStates.Add(LetterState);
            }
        }
    }
}