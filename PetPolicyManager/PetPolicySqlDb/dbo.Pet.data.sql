SET IDENTITY_INSERT [dbo].[Pet] ON
INSERT INTO [dbo].[Pet] ([PetId], [OwnerId], [PetName], [PetDateOfBirth], [BreedId]) 
VALUES (2, 1, N'Frank', N'2011-07-13', 1)
SET IDENTITY_INSERT [dbo].[Pet] OFF

