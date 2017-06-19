USE [PetPolicySqlDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spPetPolicyInsert];

GO

CREATE PROCEDURE [dbo].[spPetPolicyInsert]
	@petId int,
	@policyId int

	AS
	BEGIN
		SET NOCOUNT ON

		/*	note: don't need to return the identity column here 
			since the purpose of the table 
			is just to hold the relationship
		*/

		INSERT INTO dbo.PetPolicy
			(
			PetId,
			PolicyId,
			AddToPolicyDate,
			RemoveFromPolicyDate
			)
		VALUES
			(
			@petId,
			@policyId,
			getdate(),
			null)

		RETURN 0
	END