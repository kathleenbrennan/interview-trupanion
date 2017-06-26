USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Policy] Script Date: 6/17/2017 5:14:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP TABLE POLICY
CREATE TABLE [dbo].[Policy] (
    [PolicyId]              INT          IDENTITY (1, 1) PRIMARY KEY,
    [PolicyNumberIncrement] INT          NOT NULL,
    [PolicyNumber]          VARCHAR (40) NOT NULL,
    [PolicyEnrollmentDate]  DATETIME     NOT NULL,
	[PolicyCancellationDate]  DATETIME   ,    
	[CountryId]             INT          NOT NULL,    
    [OwnerId]            INT          NOT NULL
	, CONSTRAINT [FK_Policy_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
	
	, CONSTRAINT [FK_Policy_toOwner] 
		FOREIGN KEY ([OwnerId])
		REFERENCES [Owner]([OwnerId]) 
);


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

