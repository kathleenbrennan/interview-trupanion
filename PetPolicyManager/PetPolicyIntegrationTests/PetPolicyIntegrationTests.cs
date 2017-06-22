using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyIntegrationTests
{
    [TestFixture]
    public class PetPolicyIntegrationTests
    {
        private static string _countryCode;
        private static int? _ownerId = 1; //default unless specified otherwise
        private static IPetPolicy _petPolicy;
        private static string _ownerName;
        private static IOwner _owner;
        private static string _petName;
        private static int _speciesId;
        private static string _breedName;
        private static DateTime _petDateOfBirth;
        private static IPet _pet;
        private static List<IPetPolicySummary> _petPolicySummaryList;

        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCode()
        {
            GivenACountryCode("USA");
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyExists();
        }

        [Test]
        public static void CreatingPolicyWithNotFoundCountryCodeThrowsException()
        {
            GivenANotFoundCountryCode();
            ThenCreatingPolicyIsNotPossible();
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
            var petName = "Mina";
            var speciesId = 2;
            var breedName = "Rat Terrier";
            var petDateOfBirth = new DateTime(2014, 02, 01);
            GivenAnOwnerId(ownerId);
            GivenAPet(petName, speciesId, breedName, petDateOfBirth);
            WhenAddingAPet();
            ThenPetHasPetId();
            ThenPetHasData(petName, speciesId, breedName, petDateOfBirth, ownerId);
        }

        [Test]
        public static void CanGetPolicyAndOwnerSummary()
        {
            var ownerId = 1;
            GivenAnOwnerId(ownerId);
            WhenRetrievingPolicySummary();
            ThenPetPolicySummaryHasData(ownerId);
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

        private static void GivenANotFoundCountryCode()
        {
            _countryCode = "XYZ";
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

        private static void WhenRetrievingPolicySummary()
        {
            _petPolicySummaryList = PetPolicyFactory.GetPolicySummaryListByOwner(_ownerId.GetValueOrDefault());
        }
        #endregion

        #region Thens

        private static void ThenPolicyExists()
        {
            Assert.That(_petPolicy, Is.Not.Null);
        }

        private static void ThenCreatingPolicyIsNotPossible()
        {
            Assert.That(() => WhenEnrollingAPolicy(), Throws.Exception);
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


        private static void ThenPetHasPetId()
        {
            Assert.NotNull(_pet.PetId);
        }

        private static void ThenPetHasData(string petName, int speciesId, string breedName, DateTime petDateOfBirth, int ownerId)
        {
            Assert.AreEqual(petName, _pet.PetName);
            Assert.AreEqual(speciesId, _pet.SpeciesId);
            Assert.AreEqual(breedName, _pet.BreedName);
            Assert.AreEqual(petDateOfBirth, _pet.PetDateOfBirth);
            Assert.AreEqual(ownerId, _pet.OwnerId);
        }

        private static void ThenPetPolicySummaryHasData(int ownerId)
        {
            Assert.IsNotNull(_petPolicySummaryList);
            Assert.AreNotEqual(0, _petPolicySummaryList.Count);
            var ownerIds = _petPolicySummaryList
                .Select(l => l.OwnerId)
                .Distinct();
            Assert.AreEqual(1, ownerIds.Count(), "Should only have one ownerId");
            int distinctOwnerId = ownerIds.First();
            Assert.AreEqual(ownerId, distinctOwnerId);
        }


        #endregion

    }
}
