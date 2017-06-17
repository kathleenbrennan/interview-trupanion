/* 
NOTE: Normally would set up this script so it only runs once on database creation
or if it is part of the regular build script, use some form of existence check
to ensure that duplicate data does not get inserted.
*/


INSERT INTO [dbo].[Country] ([CountryIso3LetterCode], [CountryName]) VALUES (N'DEU', N'Germany')
INSERT INTO [dbo].[Country] ([CountryIso3LetterCode], [CountryName]) VALUES (N'FRA', N'France')
INSERT INTO [dbo].[Country] ([CountryIso3LetterCode], [CountryName]) VALUES (N'USA', N'United States of America')
INSERT INTO [dbo].[Country] ([CountryIso3LetterCode], [CountryName]) VALUES (N'GBR', N'United Kingdom')
INSERT INTO [dbo].[Country] ([CountryIso3LetterCode], [CountryName]) VALUES (N'JPN', N'Japan')
