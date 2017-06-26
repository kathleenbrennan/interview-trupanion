USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Breed] Script Date: 6/24/2017 4:13:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Breed];


GO
CREATE TABLE [dbo].[Breed] (
    [BreedId]   INT           IDENTITY (1, 1) NOT NULL,
    [BreedName] NVARCHAR (40) NULL,
    [SpeciesId] INT           NULL
);


