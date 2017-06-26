USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Species] Script Date: 6/24/2017 4:15:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Species];


GO
CREATE TABLE [dbo].[Species] (
    [SpeciesId]   INT           IDENTITY (1, 1) NOT NULL,
    [SpeciesName] NVARCHAR (40) NULL
);


