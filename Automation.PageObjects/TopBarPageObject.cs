using Automation.Utilities.WebDriverHelper;
using OpenQA.Selenium;

namespace Automation.PageObjects
{
    public class TopBarPageObject : BasePageObject
    {
        IWebElement HomeButton => driver.FindElementById("global-nav-home");
        IWebElement NewTweetButton => driver.FindElementById("global-new-tweet-button");

        public TopBarPageObject() : base()
        {
        }

        public bool IsShown()
        {
            bool isHomeButtonDisplayed = IsElementDisplayed(HomeButton);
            bool isNewTweetButtonDisplayed = IsElementDisplayed(NewTweetButton);

            return isHomeButtonDisplayed == true
                && isNewTweetButtonDisplayed == true;
        }

        public void NavigateToNewTweetPage()
        {
            NewTweetButton.Click();
            driver.WaitForAjax();
        }
    }
}
