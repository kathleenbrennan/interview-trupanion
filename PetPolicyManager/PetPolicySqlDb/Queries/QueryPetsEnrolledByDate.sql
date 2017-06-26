-- PROBLEM C: Question 1A
-- Count how many pets were enrolled on the day Mount St. Helens erupted

DECLARE @searchDate DATE

SET @searchDate = '1980-05-18'

SELECT COUNT(pet.PetId) As PetsCount
	FROM Pet as pet
	INNER JOIN PetPolicy as pp 	
		ON pet.PetId = pp.PetId
	INNER JOIN Policy as pol
		ON pp.PolicyId = pol.PolicyId
	--search date should be on or after the policy enrollment date
	WHERE pol.PolicyEnrollmentDate IS NOT NULL
	AND @searchDate >= pol.PolicyEnrollmentDate 
	--pet should have been added to the policy on or previous to the search date
	AND pp.AddToPolicyDate IS NOT NULL
	AND @searchDate >= pp.AddToPolicyDate 
	--policy should not have been cancelled previous to the search date
	--ok if policy was cancelled the same day
	AND (pol.PolicyCancellationDate IS NULL OR @searchDate <= pol.PolicyCancellationDate) 
	--pet should not have been removed from policy previous to the search date
	--ok if pet was removed the same day
	AND (pp.RemoveFromPolicyDate IS NULL OR @searchDate <= pp.RemoveFromPolicyDate)







