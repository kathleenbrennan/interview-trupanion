CREATE PROCEDURE [dbo].[spPetInsert]
	@ownerId int,
	@petName nvarchar(40),
	@breedId int,
	@petDateOfBirth date,
	@petId int = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @identity int

	INSERT INTO [dbo].[Pet] 
	(
		[OwnerId],
		[PetName],
		[BreedId],
		[PetDateOfBirth]
	) 
	VALUES 
	(
		@ownerId,
		@petName,
		@breedId,
		@petDateOfBirth
	)
				
	SET @petId = @@IDENTITY

	RETURN 0
		
END
GO
