USE [PetPolicySqlDb]
GO

/****** Object: Table [dbo].[PetPolicy] Script Date: 6/24/2017 4:14:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[PetPolicy];


GO
CREATE TABLE [dbo].[PetPolicy] (
	[PetPolicyId]          INT identity(1,1)      PRIMARY KEY,
    [PetId]                INT      NOT NULL,
    [PolicyId]             INT      NOT NULL,
    [AddToPolicyDate]      DATE NOT NULL,
    [RemoveFromPolicyDate] DATE NULL,
    CONSTRAINT [FK_PetPolicy_ToPet] FOREIGN KEY ([PetId]) REFERENCES [dbo].[Pet] ([PetId]),
    CONSTRAINT [FK_PetPolicy_ToPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [dbo].[Policy] ([PolicyId]) 
);


GO
CREATE NONCLUSTERED INDEX [IX_PetPolicy_AddToPolicyDate]
    ON [dbo].[PetPolicy]([AddToPolicyDate] ASC);

