CREATE TABLE [dbo].[PetPolicy] (
    [PetPolicyId]          INT      NOT NULL,
    [PetId]                INT      NOT NULL,
    [PolicyId]             INT      NOT NULL,
    [AddToPolicyDate]      DATETIME NOT NULL,
    [RemoveFromPolicyDate] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([PetPolicyId] ASC),
    CONSTRAINT [FK_PetPolicy_ToPet] FOREIGN KEY ([PetId]) REFERENCES [dbo].[Pet] ([PetId]),
    CONSTRAINT [FK_PetPolicy_ToPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [dbo].[Policy] ([PolicyId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PetPolicy_AddToPolicyDate]
    ON [dbo].[PetPolicy]([AddToPolicyDate] ASC);

