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
	@policyId int,
	@petPolicyAddDate date output

	AS
	BEGIN
		SET NOCOUNT ON

		/*	note: don't need to return the identity column here 
			since the purpose of the table 
			is just to hold the relationship
		*/
		DECLARE @addDate date
		SELECT @addDate = getdate()

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
			@addDate,
			null)

			SET @petPolicyAddDate = @addDate

		RETURN 0
	END