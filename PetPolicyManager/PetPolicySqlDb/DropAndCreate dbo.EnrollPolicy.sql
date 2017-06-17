USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[EnrollPolicy] Script Date: 6/16/2017 9:09:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[EnrollPolicy];


GO



CREATE PROCEDURE [dbo].[EnrollPolicy]
	@petOwnerId int,
	@countryIso3LetterCode char(3),
	@policyNumber nvarchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @countryId int

		SET @policyNumber = CONCAT(@countryIso3LetterCode, '1234567890')

		-- todo: check existence and throw if not found
		SELECT @countryId = CountryId 
		FROM dbo.Country 
		WHERE CountryIso3LetterCode = @countryIso3LetterCode


		INSERT INTO dbo.Policy
		(
			PolicyNumber
			, PolicyEnrollmentDate
			, CountryId
			, PetOwnerId
		)
		VALUES
		(
			@policyNumber
			, getdate()
			, @countryId
			, @petOwnerId
		)
	RETURN 0
END
