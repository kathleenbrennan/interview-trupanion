﻿USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[spPolicyInsert] Script Date: 6/24/2017 4:01:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spPolicyInsert];


GO

CREATE PROCEDURE [dbo].[spPolicyInsert]
	@ownerId int,
	@countryIso3LetterCode char(3),
	@policyId int OUTPUT,
	@policyNumber varchar(100) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @countryId int
		DECLARE @policyNumberIncrement int
		DECLARE @policyNumberIncrementString varchar(10)
		DECLARE @len int
		DECLARE @fill varchar(10)
		DECLARE @rowCount int
		DECLARE @errorMessage nvarchar(250)

		SELECT @countryId = CountryId 
		FROM dbo.Country 
		WHERE CountryIso3LetterCode = @countryIso3LetterCode
		SELECT @rowCount = @@ROWCOUNT
		IF @rowCount = 1
			BEGIN

				EXEC @policyNumberIncrement = fnGeneratePolicyNumber
				SELECT @policyNumberIncrementString = CONVERT(varchar(10), @policyNumberIncrement)
				SELECT @len=LEN(@policyNumberIncrementString)
				SELECT @fill = REPLICATE('0', 10-@len)

				SET @policyNumber = CONCAT(@countryIso3LetterCode, @fill, @policyNumberIncrement)
				INSERT INTO dbo.Policy
				(
					PolicyNumber
					, PolicyNumberIncrement
					, PolicyEnrollmentDate
					, CountryId
					, OwnerId
				)
				VALUES
				(
					@policyNumber
					, @policyNumberIncrement					
					, getdate()
					, @countryId
					, @ownerId
				)
				SET @policyId = @@IDENTITY
				RETURN 0
				END;
		ELSE
			BEGIN
				SET @errorMessage = 'Country Id ' + CONVERT(char(3), @countryId) + ' not found.'
				RAISERROR(@errorMessage, 11, -1, 'spPolicyInsert')
				RETURN 99
			END;
		
END
