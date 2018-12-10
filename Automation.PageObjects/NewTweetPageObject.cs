using Automation.Infrastructure.WebDriverManager.Base;
using Automation.Utilities.WebDriverHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;

namespace Automation.PageObjects
{
    public class NewTweetPageObject : BasePageObject
    {
        IWebElement NewTweetModal => driver.FindElementById("Tweetstorm-dialog-dialog");
        IWebElement SendTweetButton => driver.FindElementByCssSelector(".is-tweet-box-focus .TweetBoxToolbar .js-send-tweets");
        IWebElement NewTweetForm => driver.FindElementByCssSelector(".RichEditor.is-fakeFocus .tweet-box");
        IWebElement AddImageButton => driver.FindElementByCssSelector(".is-tweet-box-focus .TweetBoxToolbar input[type=\"file\"]");
        IWebElement ImageBlock => driver.FindElementByClassName("ComposerThumbnail-image");

        public NewTweetPageObject() : base()
        {
        }

        public bool IsOpened()
        {
            bool isNewTweetModalDisplayed = IsElementDisplayed(NewTweetModal);
            bool isSendTweetButtonDisplayed = IsElementDisplayed(SendTweetButton);

            return isNewTweetModalDisplayed == true
                && isSendTweetButtonDisplayed == true
                && SendTweetButton.Enabled == false;
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
            driver.WaitForElementToBeHidden(NewTweetModal);
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
            SendTweetButton.Click();
            driver.WaitForElementToBeHidden(NewTweetModal);
            driver.WaitForAjax();
        }
    }
}
