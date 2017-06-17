USE [PetPolicySqlDb]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[EnrollPolicy]
		@petOwnerId = 1,
		@countryIso3LetterCode = N'USA'

SELECT	@return_value as 'Return Value'

GO
