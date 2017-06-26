USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Country] Script Date: 6/24/2017 4:13:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Country];


GO
CREATE TABLE [dbo].[Country] (
    [CountryId]             INT           IDENTITY (1, 1) PRIMARY KEY,
    [CountryIso3LetterCode] CHAR (3)     NOT NULL,
    [CountryName]           NVARCHAR (50) NOT NULL
);


