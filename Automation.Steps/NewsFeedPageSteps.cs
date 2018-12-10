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
    public sealed class NewsFeedPageSteps
    {
        private NewsFeedPageObject _newsFeedPageObject;
        private DataContext _dataContext;

        private const string TEST_IMAGE_DATA_PATH = "\\TestData\\test.jpg";
        private const string NEW_TWEET_TEXT = "New tweet";

        public NewsFeedPageSteps(DataContext dataContext)
        {
            _newsFeedPageObject = new NewsFeedPageObject();
            _dataContext = dataContext;
        }

        [When(@"Alert message is disappeared")]
        public void WhenAlertMessageIsDisappeared()
        {
            _newsFeedPageObject.WaitAlertMessageIsDisappeared();
        }

        [When(@"Number of tweets is saved")]
        public void GivenNumberOfTweetsIsSaved()
        {
            _dataContext.NumberOfTweets = _newsFeedPageObject.GetNumberOfTweets();
        }

        [When(@"Navigate to the Profile page")]
        public void WhenNavigateToTheProfilePage()
        {
            _newsFeedPageObject.NavigateToProfilePage();
        }

        [When(@"Post text tweet from news feed")]
        public void WhenPostTextTweetFromNewsFeed()
        {
            Random random = new Random();
            _dataContext.TweetText = NEW_TWEET_TEXT + random.Next(0, 100000);
            _newsFeedPageObject.PostTextTweet(_dataContext.TweetText);
        }

        [When(@"Post image tweet from news feed")]
        public void WhenPostImageTweetFromNewsFeed()
        {
            string testImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + TEST_IMAGE_DATA_PATH;

            Random random = new Random();
            _dataContext.TweetText = NEW_TWEET_TEXT + random.Next(0, 100000);
            _newsFeedPageObject.PostImageTweet(_dataContext.TweetText, testImagePath);
        }

        [Then(@"News feed page is opened")]
        public void ThenNewsFeedPageIsOpened()
        {
            bool isOpened = _newsFeedPageObject.IsOpened();
            Assert.IsTrue(isOpened, "News feed page isn't opened");
        }

        [Then(@"New text tweet is appeared in news feed")]
        public void ThenNewTweetIsAppearedInNewsFeed()
        {
            bool isTweetCreated = _newsFeedPageObject.IsTweetCreated(_dataContext.TweetText, false);

            Assert.IsTrue(isTweetCreated, @"Tweet with text {0} isn't created", _dataContext.TweetText);
        }

        [Then(@"Number of tweets is increased on news feed page")]
        public void ThenNumberOfTweetsIsIncreasedOnNewsFeedPage()
        {
            int numberOfTweetsAfterPost = _newsFeedPageObject.GetNumberOfTweets();

            Assert.AreEqual(_dataContext.NumberOfTweets + 1, numberOfTweetsAfterPost, "Expected number of tweets isn't equal to actual");
        }

        [Then(@"New image tweet is appeared in news feed")]
        public void ThenNewImageTweetIsAppearedInNewsFeed()
        {
            bool isTweetCreated = _newsFeedPageObject.IsTweetCreated(_dataContext.TweetText, true);

            Assert.IsTrue(isTweetCreated, @"Tweet with text {0} and image isn't created", _dataContext.TweetText);
        }

    }
}
