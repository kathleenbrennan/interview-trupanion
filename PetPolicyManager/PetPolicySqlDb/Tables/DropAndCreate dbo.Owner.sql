USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[Owner] Script Date: 6/24/2017 4:13:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Owner];


GO
CREATE TABLE [dbo].[Owner] (
    [OwnerId]   INT            IDENTITY (1, 1) NOT NULL,
    [OwnerName] NVARCHAR (200) NULL,
    [CountryId] INT            NULL
	, 
	CONSTRAINT [FK_Owner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
);


