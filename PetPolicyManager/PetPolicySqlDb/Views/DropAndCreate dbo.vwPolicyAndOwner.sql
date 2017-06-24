USE [PetPolicySqlDb]
GO

/****** Object: View [dbo].[vwPolicyAndOwner] Script Date: 6/24/2017 4:02:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW [dbo].[vwPolicyAndOwner];


GO
CREATE VIEW [dbo].[vwPolicyAndOwner]
	AS 
	SELECT 
		pol.PolicyId,
		pol.PolicyNumber,
		pol.PolicyEnrollmentDate,
		pol.PolicyCancellationDate,
		pol.CountryId,
		Country.CountryIso3LetterCode,
		o.OwnerId,
		o.OwnerName
	FROM Policy pol
	INNER JOIN Country
	ON pol.CountryId = Country.CountryId
	LEFT JOIN Owner o
	ON pol.OwnerId = o.OwnerId
