USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[spOwnerInsert] Script Date: 6/24/2017 3:59:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spOwnerInsert];


GO
CREATE PROCEDURE [dbo].[spOwnerInsert]
	@ownerName nvarchar(200),
	@countryIso3LetterCode char(3),
	@ownerId int = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @countryId int
	DECLARE @identity int
	DECLARE @rowCount int
	DECLARE @errorMessage nvarchar(250)

SELECT @countryId = CountryId 
		FROM dbo.Country 
		WHERE CountryIso3LetterCode = @countryIso3LetterCode
		SELECT @rowCount = @@ROWCOUNT
		IF @rowCount = 1
			BEGIN
				INSERT INTO [dbo].[Owner] 
				(
					[OwnerName], 
					[CountryId]
				) 
				VALUES 
				(
					@ownerName,
					@countryId
				)
				
				SET @ownerId = @@IDENTITY

				RETURN 0
				END;
		ELSE
			BEGIN
				SET @errorMessage = 'Country Id ' + CONVERT(char(3), @countryId) + ' not found.'
				RAISERROR(@errorMessage, 11, -1, 'spOwnerInsert')
				RETURN 99
			END;
END
