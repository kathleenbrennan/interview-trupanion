using NUnit.Framework;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyIntegrationTests
{
    [TestFixture]
    public class PetPolicyIntegrationTests
    {
        private static string _countryCode;
        private static int? _ownerId;
        private static IPetPolicy _petPolicy;

        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCode()
        {
            GivenACountryCode("USA");
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyExists();
        }
        #endregion

        #region Givens
        private static void GivenACountryCode(string countryCode)
        {
            _countryCode = countryCode;
        }

        private static void GivenAnOwnerId(int ownerId)
        {
            _ownerId = ownerId;
        }
        #endregion

        #region Whens

        private static void WhenEnrollingAPolicy()
        {
            _petPolicy = PetPolicyFactory.Enroll(_countryCode, _ownerId);

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
