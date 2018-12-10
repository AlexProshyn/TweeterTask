using Automation.PageObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Automation.Steps
{
    [Binding]
    public sealed class TopBarPageSteps
    {
        private TopBarPageObject _topBarPageObject;

        public TopBarPageSteps()
        {
            _topBarPageObject = new TopBarPageObject();
        }

        [When(@"Navigate to the new tweet modal from top bar")]
        public void WhenNewTweetModalIsOpened()
        {
            _topBarPageObject.NavigateToNewTweetPage();
        }

        [Then(@"Top bar menu is shown")]
        public void ThenTopBarMenuIsShown()
        {
            bool isShown = _topBarPageObject.IsShown();
            Assert.IsTrue(isShown, "Top bar menu isn't opened");
        }
    }
}
