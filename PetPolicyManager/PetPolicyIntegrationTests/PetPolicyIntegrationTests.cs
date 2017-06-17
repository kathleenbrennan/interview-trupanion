using NUnit.Framework;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyIntegrationTests
{
    [TestFixture]
    public class PetPolicyIntegrationTests
    {
        private static string _countryCode;
        private static IPetPolicy _petPolicy;

        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCode()
        {
            GivenACountryCode("USA");
            WhenEnrollingAPolicy();
            ThenPolicyExists();
        }
        #endregion

        #region Givens
        private static void GivenACountryCode(string countryCode)
        {
            _countryCode = countryCode;
        }
        #endregion

        #region Whens

        private static void WhenEnrollingAPolicy()
        {
            _petPolicy = PetPolicyFactory.Enroll(_countryCode);

        }
        #endregion

        #region Thens

        private static void ThenPolicyExists()
        {
            Assert.That(_petPolicy, Is.Not.Null);
        }
        #endregion

    }
}
