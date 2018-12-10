using Automation.Infrastructure;
using Automation.PageObjects;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Automation.Steps
{
    [Binding]
    public sealed class NewTweetPageSteps
    {
        private NewTweetPageObject _newTweetPageObject;
        private DataContext _dataContext;

        private const string NEW_TWEET_TEXT = "New tweet";
        private const string TEST_IMAGE_DATA_PATH = "\\TestData\\test.jpg";

        public NewTweetPageSteps(DataContext dataContext)
        {
            _newTweetPageObject = new NewTweetPageObject();
            _dataContext = dataContext;
        }

        [When(@"Post text tweet from top bar")]
        public void WhenPostTweetFromTopBar()
        {
            Random random = new Random();
            _dataContext.TweetText = NEW_TWEET_TEXT + random.Next(0, 1000000);
            _newTweetPageObject.PostTextTweet(_dataContext.TweetText);
        }

        [When(@"Post image tweet from top bar")]
        public void WhenPostImageTweetFromTopBar()
        {
            string testImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + TEST_IMAGE_DATA_PATH;

            Random random = new Random();
            _dataContext.TweetText = NEW_TWEET_TEXT + random.Next(0, 1000000);
            _newTweetPageObject.PostImageTweet(_dataContext.TweetText, testImagePath);
        }

        [Then(@"New tweet modal is opened")]
        public void ThenNewTweetModalIsOpened()
        {
            bool isOpened = _newTweetPageObject.IsOpened();
            Assert.IsTrue(isOpened, "New tweet popup isn't opened");
        }
    }
}
