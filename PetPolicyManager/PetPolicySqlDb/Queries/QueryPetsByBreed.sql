-- PROBLEM C: Question 1D
-- Query pets by breed

DECLARE @breedName NVARCHAR(40)

SET @breedName = 'Domestic Shorthair'

SELECT pet.PetId,
		pet.PetName,
		b.BreedName
	FROM Pet as pet
	INNER JOIN Breed b
		ON pet.BreedId = b.BreedId
	WHERE b.BreedName = @breedName
	





