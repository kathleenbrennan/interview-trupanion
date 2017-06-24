USE [PetPolicySqlDb]
GO

/****** Object: Scalar Function [dbo].[fnGeneratePolicyNumber] Script Date: 6/24/2017 4:02:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP FUNCTION [dbo].[fnGeneratePolicyNumber];


GO
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
