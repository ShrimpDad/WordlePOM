
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

        public PageObjectModel() //this is a constructor, this is a special method that is called by default when the class is as it shares the same name
        {
            WebDriver = new FirefoxDriver(); //here we create a WebDriver to be a new FirefoxDriver (We could easily switch this out to chrome here without having to update the rest of the methods here)
        }


        public void NavigateToPage() //this is a method that uses the WordleURL private constant string to navigate to the wordle homepage
        {
            WebDriver.Navigate().GoToUrl(WordleURL);
        }


        public void RejectCookies() //this is a method that rejects cookies
        {
            IWebElement element = WebDriver.FindElement(By.Id("pz-gdpr-btn-reject")); //this finds the reject button

            element.Click(); //this clicks the reject button
        }


        public void CloseHowToPlay() //this is a method that closes the how to play popup
        {
            IWebElement element = WebDriver.FindElement(By.ClassName("game-icon")); //this finds the cross button on the popup

            element.Click(); //this clicks the cross button
        }

        public void SelectLetter(char letter) //this is a method that takes a letter, converts it to lowercase and clicks the related letterTile on the page
        {
            char lowerCaseLetter = Char.ToLower(letter); //this converts the letter passed in method to a lowercase version of the letter

            IWebElement letterTile = WebDriver.FindElement(By.XPath($"//button[text()='{lowerCaseLetter}']")); //this finds the correct tile based on the lowercase letter input

            letterTile.Click(); //this clicks the letter we passed into the method
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

        public string[] EnterWord(string word)
        {
            for (int i = 0; i < 5; i++)
            {
                SelectLetter(word[i]);
            }

            SelectEnter();

            System.Threading.Thread.Sleep(5000);

            string[] resultsArray = new string[5];

            GetLetterState();

            for (int i = 0; i < letterStates.Count; i++)
            {
                resultsArray[i] = letterStates[i].State;
            }

            return resultsArray;
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