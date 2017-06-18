CREATE TABLE [dbo].[Country]
(
	[CountryId] int identity(1,1) PRIMARY KEY, 
    [CountryIso3LetterCode] CHAR(3) NOT NULL, 
    [CountryName] VARCHAR(50) NOT NULL
)
GO

create table PetOwner
(
		PetOwnerId		int identity(1,1) PRIMARY KEY
	,	PetOwnerName		nvarchar(200)
    , CountryId int NOT NULL
	, 
	CONSTRAINT [FK_PetOwner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
)
go


CREATE TABLE [dbo].[Policy] (
    [PolicyId]              INT          IDENTITY (1, 1) PRIMARY KEY,
    [PolicyNumberIncrement] INT    NOT NULL,
    [PolicyNumber]          VARCHAR (40) NOT NULL,
    [PolicyEnrollmentDate]  DATETIME     NOT NULL,
    [PolicyCancellationDate]  DATETIME     NOT NULL,
    [CountryId]             INT          NOT NULL,
    [PetOwnerId]            INT          NOT NULL
	, CONSTRAINT [FK_Policy_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
	
	, CONSTRAINT [FK_Policy_toPetOwner] 
		FOREIGN KEY ([PetOwnerId])
		REFERENCES [PetOwner]([PetOwnerId]), 
    CONSTRAINT [PK_Policy] PRIMARY KEY ([PolicyId])
)
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyNumberIncrement] ON [dbo].[Policy] ([PolicyNumberIncrement])
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyEnrollmentDate] ON [dbo].[Policy] ([PolicyEnrollmentDate])
GO

CREATE NONCLUSTERED INDEX [IX_Policy_PolicyCancellationDate] ON [dbo].[Policy] ([PolicyCancellationDate])
GO


create table Pet
(
		PetId		int identity(1,1) PRIMARY KEY
	,	PetOwnerId	int
	,	PetName		nvarchar(40)
	,	PetDateOfBirth	date
	, BreedId int, 
    CONSTRAINT [FK_Pet_toPetOwner] 
		FOREIGN KEY ([PetOwnerId])
		REFERENCES [PetOwner]([PetOwnerId])
	, CONSTRAINT [FK_Pet_toBreed] 
		FOREIGN KEY ([BreedId])
		REFERENCES [Breed]([BreedId])
)

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



CREATE PROCEDURE [dbo].[EnrollPolicy]
	@petOwnerId int,
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
					, PetOwnerId
				)
				VALUES
				(
					@policyNumber
					, @policyNumberIncrement					
					, getdate()
					, @countryId
					, @petOwnerId
				)

				RETURN 0
				END;
		ELSE
			BEGIN
				SET @errorMessage = 'Country Id ' + CONVERT(char(3), @countryId) + ' not found.'
				RAISERROR(@errorMessage, 11, -1, 'EnrollPolicy')
				RETURN 99
			END;
		
END
		

GO


