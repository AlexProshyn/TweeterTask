using Automation.Infrastructure;
using Automation.PageObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Automation.Steps
{
    [Binding]
    public sealed class ProfilePageSteps
    {
        private DataContext _dataContext;
        private ProfilePageObject _profilePageObject;

        public ProfilePageSteps(DataContext dataContext)
        {
            _dataContext = dataContext;
            _profilePageObject = new ProfilePageObject();
        }

        [Then(@"Profile page is opened")]
        public void ThenProfilePageIsOpened()
        {
            bool isOpened = _profilePageObject.IsOpened();

            Assert.IsTrue(isOpened, "Profile page isn't opened");
        }

        [Then(@"New text tweet is appeared on profile page")]
        public void ThenNewTweetIsAppearedOnProfilePage()
        {
            bool isTweetCreated = _profilePageObject.IsTweetCreated(_dataContext.TweetText, false);

            Assert.IsTrue(isTweetCreated, @"Tweet with text {0} isn't created", _dataContext.TweetText);
        }

        [Then(@"New image tweet is appeared on profile page")]
        public void ThenNewImageTweetIsAppearedOnProfilePage()
        {
            bool isTweetCreated = _profilePageObject.IsTweetCreated(_dataContext.TweetText, true);

            Assert.IsTrue(isTweetCreated, @"Tweet with text {0} and image isn't created", _dataContext.TweetText);
        }

        [Then(@"Number of tweets is increased on profile page")]
        public void ThenNumberOfTweetsIsIncreasedOnProfilePage()
        {
            int numberOfTweetsAfterPost = _profilePageObject.GetNumberOfTweets();

            Assert.AreEqual(_dataContext.NumberOfTweets + 1, numberOfTweetsAfterPost, "Expected number of tweets isn't equal to actual");
        }
    }
}
