USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Country] Script Date: 6/17/2017 3:48:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Country];


GO
CREATE TABLE [dbo].[Country] (
    [CountryId] int identity(1,1) PRIMARY KEY,
    [CountryIso3LetterCode] NCHAR (3)     NOT NULL,
    [CountryName]           VARCHAR (50) NOT NULL
);


