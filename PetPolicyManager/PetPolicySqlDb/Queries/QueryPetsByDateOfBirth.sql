-- PROBLEM C: Question 1B
-- Get list of pets to send birthday cards
-- Assumption - only send cards to pets that are enrolled and active in a policy
-- Assumption - compile list of all birthdays in an upcoming month
-- Assumption - will be compiling card lists the month prior to pet's birthday but not later than that date
-- Assumption - dogs and cats get different card designs
-- Assumption - Owner address info is in or can be derived from the Owner table
-- NOTE: If the use case requires only sending cards to pets that are currently enrolled,
--	the results of this query should be intersected with the dataset from QueryPetsEnrolledByDate

DECLARE @birthMonth smallint
SET @birthMonth = 7

SELECT pet.PetId,
		pet.PetName,
		pet.PetDateOfBirth,
		s.SpeciesName, 
		b.BreedName,
		pol.PolicyId,
		pol.PolicyNumber,
		o.OwnerId,
		o.OwnerName
	FROM Pet as pet
	INNER JOIN Breed b
		ON pet.BreedId = b.BreedId
	INNER JOIN Species s
		ON b.SpeciesId = s.SpeciesId
	INNER JOIN PetPolicy as pp 	
		ON pet.PetId = pp.PetId
	INNER JOIN Owner o
		On pet.OwnerId = o.OwnerId
	INNER JOIN Policy as pol
		ON pp.PolicyId = pol.PolicyId
	WHERE MONTH(pet.PetDateOfBirth) = @birthMonth
	





