SET IDENTITY_INSERT [dbo].[Pet] ON
INSERT INTO [dbo].[Pet] ([PetId], [OwnerId], [PetName], [PetDateOfBirth], [BreedId]) VALUES (2, 1, N'Frank', N'2011-07-13', 1)
INSERT INTO [dbo].[Pet] ([PetId], [OwnerId], [PetName], [PetDateOfBirth], [BreedId]) VALUES (3, 1, N'InvisibleCat', N'1976-01-01', 1)
INSERT INTO [dbo].[Pet] ([PetId], [OwnerId], [PetName], [PetDateOfBirth], [BreedId]) VALUES (4, 1, N'OtherCat', N'1980-01-01', 1)
INSERT INTO [dbo].[Pet] ([PetId], [OwnerId], [PetName], [PetDateOfBirth], [BreedId]) VALUES (5, 1, N'ThirdCat', N'1981-01-01', NULL)
SET IDENTITY_INSERT [dbo].[Pet] OFF
