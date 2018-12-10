using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Automation.Utilities.WebDriverHelper;

namespace Automation.PageObjects
{
    public class NewsFeedPageObject : BasePageObject
    {
        private string tweetImageXpathTemplate = "//li[@data-item-type=\"tweet\"]//div[contains(normalize-space(),\"{0}\")]//parent::div[@class=\"content\"]//div[@class=\"AdaptiveMediaOuterContainer\"]//img";

        IWebElement ProfileCard => driver.FindElementByClassName("DashboardProfileCard");
        IWebElement TweetForm => driver.FindElementByCssSelector(".timeline-tweet-box .tweet-form");
        IWebElement TweetsStatistic => driver.FindElementByCssSelector("a[data-element-term=\"tweet_stats\"] .ProfileCardStats-statValue");
        IWebElement ProfileArea => driver.FindElementByClassName("DashboardProfileCard");
        IWebElement NewTweetForm => driver.FindElementById("tweet-box-home-timeline");
        IWebElement SendTweetButton => driver.FindElementByCssSelector(".home-tweet-box .js-tweet-btn");
        IWebElement AlertMessage => driver.FindElementById("message-drawer");
        IWebElement UploadProgressBar => driver.FindElementByCssSelector(".timeline-tweet-box .TweetBoxUploadProgress-bar");
        IWebElement AddImageButton => driver.FindElementByCssSelector(".timeline-tweet-box input[type=\"file\"]");
        IWebElement ImageBlock => driver.FindElementByClassName("ComposerThumbnail-image");
        ReadOnlyCollection<IWebElement> Tweets => driver.FindElementsByCssSelector("#stream-items-id li[data-item-type=\"tweet\"] .tweet-text");

        public NewsFeedPageObject() : base()
        {
        }

        public bool IsOpened()
        {
            bool isProfileCardDisplayed = IsElementDisplayed(ProfileCard);
            bool isTweetFormDisplayed = IsElementDisplayed(TweetForm);

            return isProfileCardDisplayed == true 
                && isTweetFormDisplayed == true;
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

            if(isImageTweet)
            {
                isCreated = isCreated && driver.IsElementExists(By.XPath(string.Format(tweetImageXpathTemplate, tweetText)));
            }
            
            return isCreated;
        }

        public void NavigateToProfilePage()
        {
            driver.WaitForElementToBeDisplayed(ProfileArea);
            ProfileArea.Click();
            driver.WaitForAjax();
        }

        public void PostTextTweet(string tweetBody)
        {
            if (string.IsNullOrEmpty(tweetBody))
            {
                throw new NullReferenceException("Tweet data is empty");
            }

            NewTweetForm.Click();
            driver.SendKeys(NewTweetForm, tweetBody);
            driver.WaitForElementToBeEnabled(SendTweetButton);
            SendTweetButton.Click();
            driver.WaitForAjax();
        }

        public void PostImageTweet(string tweetBody, string imagePath)
        {
            if (string.IsNullOrEmpty(tweetBody) || string.IsNullOrEmpty(imagePath))
            {
                throw new NullReferenceException("Tweet data is empty");
            }

            NewTweetForm.Click();
            driver.SendKeys(NewTweetForm, tweetBody);
            AddImageButton.SendKeys(imagePath);
            driver.WaitForAjax();
            driver.WaitForElementToBeDisplayed(ImageBlock);
            driver.WaitForElementToBeEnabled(SendTweetButton);
            SendTweetButton.Click();
            driver.WaitForAjax();
            driver.WaitForElementToBeDisplayed(UploadProgressBar);
            driver.WaitForElementToBeHidden(UploadProgressBar);
        }

        public void WaitAlertMessageIsDisappeared()
        {
            driver.WaitForClassAppeared(AlertMessage, "hidden");
        }
    }
}
