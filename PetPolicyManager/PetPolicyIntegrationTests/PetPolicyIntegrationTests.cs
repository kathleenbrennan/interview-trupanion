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
        private static List<IPolicyAndOwnerSummary> _policyAndOwnerSummaryList;
        private static List<IPolicyAndPetSummary> _policyAndPetSummaryList;

        private static int _petId;
        private static int _policyId;
        private static DateTime? _petPolicyAddDate;
        private static DateTime? _petPolicyRemoveDate;

        //NOTE: in a production environment,
        // we would want these tests to run their own data setup and cleanup
        // to ensure that the data is in a reproducible, predictable form
        // and that the tests didn't leave inconsistent data behind
        #region Tests
        [Test]
        public static void CanCreateNewPolicyWithCountryCode()
        {
            GivenACountryCode("USA");
            GivenAnOwnerId(1);
            WhenEnrollingAPolicy();
            ThenPolicyIsCreated();
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
            WhenRetrievingPolicyAndOwnerSummary();
            ThenPolicyAndOwnerSummaryHasData(ownerId);
        }

        [Test]
        public static void CanGetPolicyAndPetSummary()
        {
            var policyId = 8;
            GivenAPolicy(policyId);
            WhenRetrievingPolicyAndPetSummary();
            ThenPolicyAndPetSummaryHasData(policyId);
        }

        [Test]
        public static void CanAddPetToPolicy()
        {
            GivenAPet(7);
            GivenAPolicy(12);
            WhenAddingPetToPolicy();
            ThenPetIsAddedToPolicy();
        }

        [Test]
        public static void CanRemovePetFromPolicy()
        {
            GivenAPet(7);
            GivenAPolicy(12);
            WhenRemovingPetPolicy();
            ThenPetIsRemovedFromPolicy();
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

        private static void GivenAPet(int petId)
        {
            _petId = petId;
        }

        private static void GivenAPolicy(int policyId)
        {
            _policyId = policyId;
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

        private static void WhenRetrievingPolicyAndOwnerSummary()
        {
            _policyAndOwnerSummaryList = PetPolicyFactory.GetPolicyAndOwnerSummaryListByOwner(_ownerId.GetValueOrDefault());
        }


        private static void WhenRetrievingPolicyAndPetSummary()
        {
            _policyAndPetSummaryList = PetPolicyFactory.GetPolicyAndPetSummaryListByPolicyId(_policyId);
        }

        private static void WhenAddingPetToPolicy()
        {
            _petPolicyAddDate = PetPolicyFactory.AddPetToPolicy(_petId, _policyId);
        }
        private static void WhenRemovingPetPolicy()
        {
            _petPolicyRemoveDate = PetPolicyFactory.RemovePetFromPolicy(_petId, _policyId);
        }
        #endregion

        #region Thens

        private static void ThenPolicyIsCreated()
        {
            Assert.That(_petPolicy, Is.Not.Null);
            Assert.That(_petPolicy.PolicyId, Is.Not.Null);
            Assert.That(_petPolicy.PolicyNumber, Is.Not.Null.Or.Empty);
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

        private static void ThenPolicyAndOwnerSummaryHasData(int ownerId)
        {
            Assert.IsNotNull(_policyAndOwnerSummaryList);
            Assert.AreNotEqual(0, _policyAndOwnerSummaryList.Count);
            var ownerIds = _policyAndOwnerSummaryList
                .Select(l => l.OwnerId)
                .Distinct();
            Assert.AreEqual(1, ownerIds.Count(), "Should only have one ownerId");
            int distinctOwnerId = ownerIds.First();
            Assert.AreEqual(ownerId, distinctOwnerId);
        }

        private static void ThenPolicyAndPetSummaryHasData(int policyId)
        {
            Assert.IsNotNull(_policyAndPetSummaryList);
            Assert.AreNotEqual(0, _policyAndPetSummaryList.Count);
            var distinctPolicyIds = _policyAndPetSummaryList
                .Select(l => l.PolicyId)
                .Distinct();
            Assert.AreEqual(1, distinctPolicyIds.Count(), "Should only have one ownerId");
            int distinctPolicyId = distinctPolicyIds.First();
            Assert.AreEqual(policyId, distinctPolicyId);
        }

        private static void ThenPetIsAddedToPolicy()
        {
            Assert.IsNotNull(_petPolicyAddDate);
        }

        private static void ThenPetIsRemovedFromPolicy()
        {
            Assert.IsNotNull(_petPolicyRemoveDate);
        }

        #endregion

    }
}
