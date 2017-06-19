USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Policy] Script Date: 6/17/2017 4:28:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Policy];


GO
CREATE TABLE [dbo].[Policy] (
    [PolicyId]              INT          IDENTITY (1, 1) NOT NULL,
    [PolicyNumberIncrement] INT    NOT NULL,
    [PolicyNumber]          VARCHAR (40) NOT NULL,
    [PolicyEnrollmentDate]  DATETIME     NOT NULL,
    [CountryId]             INT          NOT NULL,
    [PetOwnerId]            INT          NOT NULL
);


