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
GO