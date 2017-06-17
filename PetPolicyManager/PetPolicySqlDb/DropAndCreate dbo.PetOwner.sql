USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[PetOwner] Script Date: 6/17/2017 3:50:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP TABLE [dbo].[PetOwner];


GO
create table PetOwner
(
		PetOwnerId		int identity(1,1) PRIMARY KEY
	,	PetOwnerName		nvarchar(200)
    , CountryId int
	, 
	CONSTRAINT [FK_PetOwner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
);


