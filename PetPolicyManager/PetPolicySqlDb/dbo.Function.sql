CREATE FUNCTION [dbo].[fnGeneratePolicyNumber]
(
)
RETURNS int
AS
BEGIN

	DECLARE @rowCount int
	DECLARE @prevPolicyNumberIncrement int
	DECLARE @newPolicyNumberIncrement int

	SELECT @prevPolicyNumberIncrement = MAX(PolicyNumberIncrement)
	FROM dbo.Policy

	SELECT @rowCount = @@ROWCOUNT

	IF @prevPolicyNumberIncrement IS NULL
		BEGIN
			SET @newPolicyNumberIncrement = 1
		END
	ELSE
		BEGIN
			SET @newPolicyNumberIncrement = @prevPolicyNumberIncrement + 1
		END

	RETURN @newPolicyNumberIncrement
END
