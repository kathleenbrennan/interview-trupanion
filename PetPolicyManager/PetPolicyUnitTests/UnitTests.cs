
using System;
using NUnit.Framework;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyUnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private static string _countryCode;
        private static int? _ownerId = 1; //default if not set explicitly
        private static IPetPolicy _petPolicy;

        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCodeAndOwnerId()
        {
            GivenACountryCode("USA");
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyExists();
        }

        [Test]
        public static void CreatingPolicyWithEmptyCountryCodeThrowsException()
        {
            GivenAnEmptyCountryCode();
            ThenCreatingPolicyIsNotPossible();
        }


        [Test]
        public static void CreatingPolicyWithNullOwnerIdThrowsException()
        {
            GivenANullOwnerId();
            ThenCreatingPolicyIsNotPossible();
        }



        [Test]
        public static void CreatingPolicyWithTooShortCountryCodeThrowsException()
        {
            GivenATooShortCountryCode();
            ThenCreatingPolicyIsNotPossible();
        }

        [Test]
        public static void CreatingPolicyWithTooLongCountryCodeThrowsException()
        {
            GivenATooLongCountryCode();
            ThenCreatingPolicyIsNotPossible();
        }

        [Test]
        public static void PolicyContainsCountryCode()
        {
            var countryCode = "USA";
            GivenACountryCode(countryCode);
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyNumberContainsCountryCode(countryCode);
        }

        [Test]
        public static void PolicyNumberIsThirteenCharactersLong()
        {
            var countryCode = "USA";
            GivenACountryCode(countryCode);
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyNumberIsThirteenCharactersLong();

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

        private static void GivenANullOwnerId()
        {
            _ownerId = null;
        }

        private static void GivenAnEmptyCountryCode()
        {
            _countryCode = string.Empty;
        }

        private static void GivenATooShortCountryCode()
        {
            _countryCode = "us";
        }

        private static void GivenATooLongCountryCode()
        {
            _countryCode = "uscountry";
        }

        #endregion

        #region Whens

        private static void WhenEnrollingAPolicy()
        {
            _petPolicy = PetPolicyFactory.Enroll(_countryCode, _ownerId);

        }
        #endregion

        #region Thens


        private static void ThenCreatingPolicyIsNotPossible()
        {
            Assert.That(() => WhenEnrollingAPolicy(), Throws.Exception);
        }

        private static void ThenPolicyExists()
        {
            Assert.That(_petPolicy, Is.Not.Null);
        }


        private static void ThenPolicyNumberContainsCountryCode(string countryCode)
        {
            Assert.That(_petPolicy.PolicyNumber, Does.Contain(countryCode));
        }


        private static void ThenPolicyNumberIsThirteenCharactersLong()
        {
            Assert.That(_petPolicy.PolicyNumber.Length == 13);
        }
        #endregion

    }
}
