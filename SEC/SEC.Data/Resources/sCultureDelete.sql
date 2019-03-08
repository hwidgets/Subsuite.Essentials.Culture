--#########################################################
-- DESCRIPTION
-- This stored proc enables to delete a culture.
--#########################################################

create procedure SEC.sCultureDelete (
	-- Parameters listing.
	@ActorId int,
	@CultureId int,
	@DeletionResult bit = 0 output
)
as
begin;-- Procedure engagement.
	declare @stillExisting int;

	delete from SEC.tCulture where CultureId = @CultureId;
	set @stillExisting = 
		(
			select
				CultureId 
			from
				SEC.tCulture 
			where
				CultureId = @CultureId
		);

	if (@stillExisting is null)
		set @DeletionResult = 1;
end;