using Automation.PageObjects;
using TechTalk.SpecFlow;

namespace Automation.Steps
{
    [Binding]
    public sealed class GeneralSteps
    {
        private BasePageObject _basePageObject;

        public GeneralSteps()
        {
            _basePageObject = new BasePageObject();
        }

        [When(@"Page is refreshed")]
        public void ThenRefreshThePage()
        {
            _basePageObject.RefreshThePage();
        }
    }
}
