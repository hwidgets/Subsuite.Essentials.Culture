--#########################################################
-- DESCRIPTION
-- This stored proc enables to create a culture.
--#########################################################

create procedure SEC.sCultureCreate (
	-- Parameters listing.
	@ActorId int,
	@LanguageCode int,
	@CultureIdResult int output
)
as
begin;-- Procedure engagement.
	insert into SEC.tCulture (LanguageCode)
	values (@LanguageCode);

	set @CultureIdResult = scope_identity();
end;