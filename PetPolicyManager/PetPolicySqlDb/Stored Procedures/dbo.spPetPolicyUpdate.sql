CREATE PROCEDURE [dbo].[spPetPolicyUpdateRemovalDate]
	@petId int,
	@policyId int,
	@removeFromPolicyDate date 

	AS
	BEGIN
		SET NOCOUNT ON

		UPDATE PetPolicy
		SET RemoveFromPolicyDate = @removeFromPolicyDate
		WHERE PetId = @petId
		AND PolicyId = @policyId
		
		RETURN 0
	END
