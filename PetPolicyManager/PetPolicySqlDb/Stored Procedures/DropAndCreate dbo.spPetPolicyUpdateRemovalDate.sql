USE [PetPolicySqlDb]
GO

/****** Object: SqlProcedure [dbo].[spPetPolicyUpdateRemovalDate] Script Date: 6/24/2017 4:00:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spPetPolicyUpdateRemovalDate];


GO
CREATE PROCEDURE [dbo].[spPetPolicyUpdateRemovalDate]
	@petId int,
	@policyId int,
	@petPolicyRemovalDate date output

	AS
	BEGIN
		SET NOCOUNT ON

		SELECT @petPolicyRemovalDate = getdate()

		UPDATE PetPolicy
		SET RemoveFromPolicyDate = @petPolicyRemovalDate
		WHERE PetId = @petId
		AND PolicyId = @policyId
		
		RETURN 0
	END
