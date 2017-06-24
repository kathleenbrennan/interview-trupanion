USE [PetPolicySqlDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP TABLE [dbo].[Owner];


GO
create table Owner
(
		OwnerId		int identity(1,1) PRIMARY KEY
	,	OwnerName		nvarchar(200)
    , CountryId int
	, 
	CONSTRAINT [FK_Owner_ToCountry] 
		FOREIGN KEY ([CountryId]) 
		REFERENCES [dbo].[Country]([CountryId])
);


