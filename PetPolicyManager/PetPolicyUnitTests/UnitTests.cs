
using System;
using NUnit.Framework;
using PetPolicyLibrary;

namespace PetPolicyUnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private static string _countryCode;
        private static PetPolicy _petPolicy;

        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCode()
        {
            string countryCode = "USA";
            GivenACountryCode(countryCode);
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
            string countryCode = "USA";
            GivenACountryCode(countryCode);
            WhenEnrollingAPolicy();
            ThenPolicyNumberContainsCountryCode(countryCode);
        }

        [Test]
        public static void PolicyNumberIsThirteenCharactersLong()
        {
            string countryCode = "USA";
            GivenACountryCode(countryCode);
            WhenEnrollingAPolicy();
            ThenPolicyNumberIsThirteenCharactersLong();

        }

        private static void ThenPolicyNumberIsThirteenCharactersLong()
        {
            Assert.Inconclusive();
        }
        #endregion

        #region Givens
        private static void GivenACountryCode(string countryCode)
        {
            _countryCode = countryCode;
        }


        private static void GivenAnEmptyCountryCode()
        {
            _countryCode = String.Empty;
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
            //_petPolicy = new PetPolicy(_countryCode);
            //todo: Factory method so can only create with country code
            _petPolicy = PetPolicyFactory.Enroll(_countryCode);

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
        #endregion

    }
}
