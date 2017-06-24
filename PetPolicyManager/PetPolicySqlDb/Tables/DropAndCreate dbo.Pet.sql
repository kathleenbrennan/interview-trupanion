USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Pet] Script Date: 6/24/2017 4:14:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Pet];


GO
CREATE TABLE [dbo].[Pet] (
    [PetId]          INT           IDENTITY (1, 1) PRIMARY KEY,
    [OwnerId]        INT           NULL,
    [PetName]        NVARCHAR (40) NULL,
    [PetDateOfBirth] DATE          NULL,
    [BreedId]        INT           NULL
    , CONSTRAINT [FK_Pet_toOwner] 
		FOREIGN KEY ([OwnerId])
		REFERENCES [Owner]([OwnerId])
	, CONSTRAINT [FK_Pet_toBreed] 
		FOREIGN KEY ([BreedId])
		REFERENCES [Breed]([BreedId])
);



