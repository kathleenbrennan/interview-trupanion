CREATE TABLE [dbo].[PetPolicy] (
	[PetPolicyId]          INT identity(1,1)      PRIMARY KEY,
    [PetId]                INT      NOT NULL,
    [PolicyId]             INT      NOT NULL,
    [AddToPolicyDate]      DATETIME NOT NULL,
    [RemoveFromPolicyDate] DATETIME NULL,
    CONSTRAINT [FK_PetPolicy_ToPet] FOREIGN KEY ([PetId]) REFERENCES [dbo].[Pet] ([PetId]),
    CONSTRAINT [FK_PetPolicy_ToPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [dbo].[Policy] ([PolicyId]) 
);


GO
CREATE NONCLUSTERED INDEX [IX_PetPolicy_AddToPolicyDate]
    ON [dbo].[PetPolicy]([AddToPolicyDate] ASC);

