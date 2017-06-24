SET IDENTITY_INSERT [dbo].[Policy] ON
INSERT INTO [dbo].[Policy] ([PolicyId], [PolicyNumberIncrement], [PolicyNumber], [PolicyEnrollmentDate], [PolicyCancellationDate], [CountryId], [OwnerId]) VALUES (2, 1, N'USA0000000001', N'2017-06-17 00:00:00', N'2017-06-18 00:00:00', 3, 1)
INSERT INTO [dbo].[Policy] ([PolicyId], [PolicyNumberIncrement], [PolicyNumber], [PolicyEnrollmentDate], [PolicyCancellationDate], [CountryId], [OwnerId]) VALUES (3, 2, N'USA0000000002', N'1976-01-01 00:00:00', N'1996-12-31 00:00:00', 3, 1)
SET IDENTITY_INSERT [dbo].[Policy] OFF
