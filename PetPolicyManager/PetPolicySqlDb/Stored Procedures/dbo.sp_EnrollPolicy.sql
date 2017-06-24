CREATE PROCEDURE [dbo].[EnrollPolicy]
	@petOwnerId int,
	@countryIso3LetterCode char(3)
AS
	INSERT INTO dbo.Policy
	(
		PolicyNumber
		, PolicyEnrollmentDate
		, CountryIso3LetterCode
		, PetOwnerId
	)
	VALUES
	(
		
	



RETURN 0
