CREATE TABLE [dbo].[Country]
(
	[CountryId] int identity(1,1) PRIMARY KEY, 
    [CountryIso3LetterCode] CHAR(3) NOT NULL, 
    [CountryName] VARCHAR(50) NOT NULL
)
GO

create table Owner
(
		OwnerId		int identity(1,1) PRIMARY KEY
	,	OwnerName		nvarchar(200)
    , CountryId int NOT NULL
	, 
	CONSTRAINT [FK_Owner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
)
go


CREATE TABLE [dbo].[Policy] (
    [PolicyId]              INT          IDENTITY (1, 1) PRIMARY KEY,
    [PolicyNumberIncrement] INT    NOT NULL,
    [PolicyNumber]          VARCHAR (40) NOT NULL,
    [PolicyEnrollmentDate]  DATE     NOT NULL,
    [PolicyCancellationDate]  DATE,
    [CountryId]             INT          NOT NULL,
    [OwnerId]            INT          NOT NULL
	, CONSTRAINT [FK_Policy_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
	
	, CONSTRAINT [FK_Policy_toOwner] 
		FOREIGN KEY ([OwnerId])
		REFERENCES [Owner]([OwnerId]) 
)
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyEnrollmentDate]
    ON [dbo].[Policy]([PolicyEnrollmentDate] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyCancellationDate] 
ON [dbo].[Policy] ([PolicyCancellationDate])
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyNumberIncrement]
    ON [dbo].[Policy]([PolicyNumberIncrement] ASC);
GO

create table Pet
(
		PetId		int identity(1,1) PRIMARY KEY
	,	OwnerId	int
	,	PetName		nvarchar(40)
	,	PetDateOfBirth	date
	, BreedId int, 
    CONSTRAINT [FK_Pet_toOwner] 
		FOREIGN KEY ([OwnerId])
		REFERENCES [Owner]([OwnerId])
	, CONSTRAINT [FK_Pet_toBreed] 
		FOREIGN KEY ([BreedId])
		REFERENCES [Breed]([BreedId])
)
GO

CREATE NONCLUSTERED INDEX [IX_Pet_PetDateOfBirth]
    ON [dbo].[Pet]([PetDateOfBirth] ASC);
GO

create table Breed
(
		BreedId		int identity(1,1) PRIMARY KEY
	,	BreedName		nvarchar(40)
	, SpeciesId int
	, CONSTRAINT [FK_Breed_toSpecies] 
		FOREIGN KEY ([SpeciesId])
		REFERENCES [Species]([SpeciesId])
)
go

create table Species
(
		SpeciesId		int identity(1,1) PRIMARY KEY
	,	SpeciesName		nvarchar(40)	
)
go

CREATE TABLE [dbo].[PetPolicy] (
    [PetPolicyId]          INT identity(1,1)      PRIMARY KEY,
    [PetId]                INT      NOT NULL,
    [PolicyId]             INT      NOT NULL,
    [AddToPolicyDate]      DATE NOT NULL,
    [RemoveFromPolicyDate] DATE NULL,
    CONSTRAINT [FK_PetPolicy_ToPet] FOREIGN KEY ([PetId]) REFERENCES [dbo].[Pet] ([PetId]),
    CONSTRAINT [FK_PetPolicy_ToPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [dbo].[Policy] ([PolicyId])
	);
GO

CREATE NONCLUSTERED INDEX [IX_PetPolicy_AddToPolicyDate]
    ON [dbo].[PetPolicy]([AddToPolicyDate] ASC);
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
	-- for the purposes of this exercise we are not doing thorough
	--     checking to make sure the breed names are controlled
	--     but we would in production, or have them selected from a data-driven list
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
GO


CREATE PROCEDURE [dbo].[spPolicyInsert]
	@ownerId int,
	@countryIso3LetterCode char(3),
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

				RETURN 0
				END;
		ELSE
			BEGIN
				SET @errorMessage = 'Country Id ' + CONVERT(char(3), @countryId) + ' not found.'
				RAISERROR(@errorMessage, 11, -1, 'spPolicyInsert')
				RETURN 99
			END;
		
END
GO

CREATE PROCEDURE [dbo].[spPetsMoveBetweenOwners]
	@prevOwnerId int,
	@newOwnerId int
AS
BEGIN
	SET NOCOUNT ON

/*
NOTE: Easy set-based implementation would be
	UPDATE Pet
	SET OwnerId = @newOwnerId
	WHERE OwnerId = @prevOwnerId
but that does not meet the requirement of moving pets one at a time
*/

DECLARE @petId int
DECLARE @err int

DECLARE pet_cursor CURSOR STATIC LOCAL
    FOR SELECT PetId FROM PET WHERE ownerId = @prevOwnerId 

BEGIN TRANSACTION

OPEN pet_cursor

WHILE 1 = 1
BEGIN
   FETCH NEXT FROM pet_cursor INTO @petId 
   IF @@fetch_status <> 0
      BREAK

   UPDATE Pet
   SET    OwnerId = @newOwnerId
   WHERE  PetId = @petId
   SELECT @err = @@error
   IF @err <> 0
      BREAK
END

DEALLOCATE pet_cursor

IF @err = 0
   COMMIT TRANSACTION
ELSE
   ROLLBACK TRANSACTION
		
END
GO


