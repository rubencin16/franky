using Tweetinvi;

namespace TweetSearcher.Helpers
{
    public class AccountData
    {
        public AccountData()
        {
            var user = Auth.SetUserCredentials("NQKpOtz1V0xUvBMIMNT5nCfex", "pDj7vHOXZoYSRkC08qtydwg1GGmUPybsqbV6OVDC36lAHr6HvT", "88983495-mZr523Pc5xzFeU5RNdLXPkwC7FbdLPpb2izPZTQxr", "eQ0BMDlqL5XvWtF5d2wD1TuuoEtp0YLv5L8qNnGkg04w5");
            var userAuth = User.GetAuthenticatedUser();
        }
    }
}