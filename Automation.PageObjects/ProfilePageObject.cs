using Automation.Utilities.WebDriverHelper;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Automation.PageObjects
{
    public class ProfilePageObject : BasePageObject
    {
        private string tweetImageXpathTemplate = "//li[@data-item-type=\"tweet\"]//div[contains(normalize-space(),\"{0}\")]//parent::div[@class=\"content\"]//div[@class=\"AdaptiveMediaOuterContainer\"]//img";

        IWebElement ProfileAvatar => driver.FindElementByClassName("ProfileAvatar");
        IWebElement TweetsStatistic => driver.FindElementByCssSelector(".ProfileNav-item--tweets .ProfileNav-value");
        ReadOnlyCollection<IWebElement> Tweets => driver.FindElementsByCssSelector("#stream-items-id li[data-item-type=\"tweet\"] .tweet-text");

        public ProfilePageObject() : base()
        {
        }

        public bool IsOpened()
        {
            bool isProfileAvatarDisplayed = IsElementDisplayed(ProfileAvatar);

            return isProfileAvatarDisplayed == true;
        }

        public int GetNumberOfTweets()
        {
            return int.Parse(TweetsStatistic.Text);
        }

        public bool IsTweetCreated(string tweetText, bool isImageTweet)
        {
            if (string.IsNullOrEmpty(tweetText))
            {
                throw new NullReferenceException("TweetText is empty");
            }

            bool isCreated = true;

            isCreated = isCreated && Tweets.FirstOrDefault().Text.Contains(tweetText);

            if (isImageTweet)
            {
                isCreated = isCreated && driver.IsElementExists(By.XPath(string.Format(tweetImageXpathTemplate, tweetText)));
            }

            return isCreated;
        }
    }
}
