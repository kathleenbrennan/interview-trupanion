USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[spPetInsert] Script Date: 6/24/2017 3:59:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spPetInsert];


GO


CREATE PROCEDURE [dbo].[spPetInsert]
	@ownerId int,
	@petName nvarchar(40),
	@speciesId int, -- 1 = cat, 2 = dog
	@breedName varchar(50),
	@petDateOfBirth date,
	@petId int = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @breedId int
	DECLARE @identity int
	DECLARE @rowCount int
	DECLARE @errorMessage nvarchar(250)

	-- get or insert breed
	SELECT @breedId = BreedId 
		FROM dbo.Breed 
		WHERE LOWER(BreedName) = LOWER(@breedName)
		SELECT @rowCount = @@ROWCOUNT
		IF @rowCount = 0
		BEGIN
			INSERT INTO Breed (BreedName, SpeciesId)
			VALUES (@breedName, @speciesId)
			SET @breedId = @@IDENTITY
		END


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
