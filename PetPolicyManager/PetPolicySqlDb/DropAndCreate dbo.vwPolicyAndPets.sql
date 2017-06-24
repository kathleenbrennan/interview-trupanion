USE [PetPolicySqlDb]
GO

/****** Object: View [dbo].[vwPolicyAndPets] Script Date: 6/24/2017 1:06:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW [dbo].[vwPolicyAndPets];


GO
	CREATE VIEW [dbo].[vwPolicyAndPets]
	AS 
	
	SELECT  
		pol.PolicyId,
		pol.PolicyNumber,
		p.PetId,
		p.PetName,
		p.PetDateOfBirth,
		s.SpeciesId,
		s.SpeciesName,
		b.BreedId,
		b.BreedName, 
		pp.AddToPolicyDate,
		pp.RemoveFromPolicyDate

	FROM Policy pol
	LEFT JOIN PetPolicy pp
	ON pol.PolicyId = pp.PolicyId
	LEFT JOIN Pet p
	ON pp.PetId = p.PetId
	LEFT JOIN Breed b
	ON p.BreedId = b.BreedId
	LEFT JOIN Species s
	ON b.SpeciesId = s.SpeciesId
