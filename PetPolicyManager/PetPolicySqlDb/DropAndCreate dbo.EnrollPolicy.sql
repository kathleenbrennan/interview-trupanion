USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[EnrollPolicy] Script Date: 6/16/2017 9:09:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROCEDURE [dbo].[EnrollPolicy];


GO



CREATE PROCEDURE [dbo].[EnrollPolicy]
	@petOwnerId int,
	@countryIso3LetterCode char(3),
	@policyNumber varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @countryId int
		DECLARE @rowCount int
		DECLARE @errorMessage nvarchar(250)


		-- todo: check existence and throw if not found
		SELECT @countryId = CountryId 
		FROM dbo.Country 
		WHERE CountryIso3LetterCode = @countryIso3LetterCode
		SELECT @rowCount = @@ROWCOUNT
		-- TODO: check if this error is being reached
		IF @rowCount = 1
			BEGIN

				SET @policyNumber = CONCAT(@countryIso3LetterCode, '1234567890')
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
				END;
		ELSE
			BEGIN
				SET @errorMessage = 'Country Id ' + CONVERT(char(3), @countryId) + ' not found.'
				RAISERROR(@errorMessage, 11, -1, 'EnrollPolicy')
				RETURN 99
			END;
		
	RETURN 0
END
