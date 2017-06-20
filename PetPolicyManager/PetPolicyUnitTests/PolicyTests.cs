
using System;
using NUnit.Framework;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyUnitTests
{
    [TestFixture]
    public class PolicyTests
    {
        private static string _countryCode;
        private static int? _ownerId = 1; //default if not set explicitly
        private static IPetPolicy _petPolicy;
        private static string _ownerName;
        private static IOwner _owner;
        private static string _petName;
        private static int _speciesId;
        private static string _breedName;
        private static DateTime _petDateOfBirth;
        private static IPet _pet;

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

        [Test]
        public static void CanCreateAnOwner()
        {
            var countryCode = "USA";
            GivenACountryCode(countryCode);
            var ownerName = "Larry Brennan";
            GivenAnOwnerName(ownerName);
            WhenAddingAnOwner();
            ThenOwnerHasCountryId();
            ThenOwnerHasName(ownerName);
            ThenOwnerHasOwnerId();
        }

        [Test]
        public static void CanAddAPet()
        {
            var ownerId = 3;
            var petName = "Stella";
            var speciesId = 2;
            var breedName = "Boxer";
            var petDateOfBirth = new DateTime(2014,02,01);
            GivenAnOwnerId(ownerId);
            GivenAPet(petName, speciesId, breedName, petDateOfBirth);
            WhenAddingAPet();
            ThenPetHasPetId();
            ThenPetHasData(petName, speciesId, breedName, petDateOfBirth, ownerId);
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

        private static void GivenAnOwnerName(string ownerName)
        {
            _ownerName = ownerName;
        }


        private static void GivenAPet(string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            _petName = petName;
            _speciesId = speciesId;
            _breedName = breedName;
            _petDateOfBirth = petDateOfBirth;
        }

        #endregion

        #region Whens

        private static void WhenEnrollingAPolicy()
        {
            _petPolicy = PetPolicyFactory.Enroll(_countryCode, _ownerId);

        }


        private static void WhenAddingAnOwner()
        {
            _owner = OwnerFactory.RegisterOwner(_countryCode, _ownerName);
        }


        private static void WhenAddingAPet()
        {
            _pet = PetFactory.AddPet(_ownerId.GetValueOrDefault(), _petName, _speciesId, _breedName, _petDateOfBirth);
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

        private static void ThenOwnerHasName(string ownerName)
        {
            Assert.Multiple(() =>
            {
                Assert.NotNull(_owner.OwnerName);
                Assert.IsNotEmpty(_owner.OwnerName);
                Assert.AreEqual(ownerName, _owner.OwnerName);
            });
        }

        private static void ThenOwnerHasOwnerId()
        {
            Assert.NotNull(_owner.OwnerId);
        }
        private static void ThenOwnerHasCountryId()
        {
            Assert.NotNull(_owner.CountryId);
        }


        private static void ThenPetHasData(string petName, int speciesId, string breedName, DateTime petDateOfBirth, int ownerId)
        {
            Assert.AreEqual(petName, _pet.PetName);
            Assert.AreEqual(speciesId, _pet.SpeciesId);
            Assert.AreEqual(breedName, _pet.BreedName);
            Assert.AreEqual(petDateOfBirth, _pet.PetDateOfBirth);
            Assert.AreEqual(ownerId, _pet.OwnerId);
        }

        private static void ThenPetHasPetId()
        {
            Assert.NotNull(_pet.PetId);
        }
        #endregion

    }

    
}
