DECLARE @prevOwnerId int
DECLARE @newOwnerId int

SET @prevOwnerId = 3
SET @newOwnerId = 2

SELECT * FROM PET WHERE ownerId = @newOwnerId

/*
NOTE: Easy set-based implementation would be
	UPDATE Pet
	SET OwnerId = @newOwnerId
	WHERE OwnerId = @prevOwnerId
but that does not meet the requirement of moving pets one at a time
*/

DECLARE @petId int
DECLARE @err int

DECLARE pet_cursor CURSOR STATIC LOCAL
    FOR SELECT PetId FROM PET WHERE ownerId = @prevOwnerId 

BEGIN TRANSACTION

OPEN pet_cursor

WHILE 1 = 1
BEGIN
   FETCH NEXT FROM pet_cursor INTO @petId 
   IF @@fetch_status <> 0
      BREAK

   UPDATE Pet
   SET    OwnerId = @newOwnerId
   WHERE  PetId = @petId
   SELECT @err = @@error
   IF @err <> 0
      BREAK
END

DEALLOCATE pet_cursor

IF @err = 0
   COMMIT TRANSACTION
ELSE
   ROLLBACK TRANSACTION

