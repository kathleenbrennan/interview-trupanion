CREATE TABLE [dbo].[Country]
(
	[CountryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CountryIso3LetterCode] NCHAR(3) NOT NULL, 
    [CountryName] NVARCHAR(50) NOT NULL
)
GO

create table PetOwner
(
		PetOwnerId		int identity(1,1) PRIMARY KEY
	,	PetOwnerName		nvarchar(200)
    , CountryId int NOT NULL
	, 
    /*
	CONSTRAINT [FK_PetOwner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo.Country]([CountryId])
	*/
)
go

create table Policy
(
		PolicyId		int identity(1,1) PRIMARY KEY
	,	PolicyNumber	varchar(40) NOT NULL
	,	PolicyEnrollmentDate	datetime NOT NULL
    , CountryId int NOT NULL
	, PetOwnerId int NOT NULL
	/*
	, CONSTRAINT [FK_Policy_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo.Country]([CountryId])
	*/
	, CONSTRAINT [FK_Policy_toPetOwner] 
		FOREIGN KEY ([PetOwnerId])
		REFERENCES [PetOwner]([PetOwnerId])
)
go

create table Pet
(
		PetId		int identity(1,1) PRIMARY KEY
	,	PetOwnerId	int
	,	PetName		nvarchar(40)
	,	PetDateOfBirth	date
	, BreedId int
	, CONSTRAINT [FK_Pet_toPetOwner] 
		FOREIGN KEY ([PetOwnerId])
		REFERENCES [PetOwner]([PetOwnerId])
	, CONSTRAINT [FK_Pet_toBreed] 
		FOREIGN KEY ([BreedId])
		REFERENCES [Breed]([BreedId])
)
go

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
	@countryIso3LetterCode char(3)
AS
	DECLARE @countryId int
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
		CONCAT(@countryIso3LetterCode, '1234567890')
		, getdate()
		, @countryId
		, @petOwnerId
	)
RETURN 0
