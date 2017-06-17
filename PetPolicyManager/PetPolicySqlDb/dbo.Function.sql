CREATE FUNCTION [dbo].[fnGeneratePolicyNumber]
(
	@countryCode nchar(3)
)
RETURNS VARCHAR(10)
AS
BEGIN
	RETURN @countryCode
END
