USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Pet] Script Date: 6/17/2017 5:10:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Pet];


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
go


