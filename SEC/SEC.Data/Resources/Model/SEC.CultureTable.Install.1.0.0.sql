--#########################################################
-- DESCRIPTION
-- This table centralizes cultures.
--#########################################################

create table SEC.tCulture (
	-- Fields listing.
	CultureId int not null identity(0, 1),
	LanguageCode int not null

	-- Constraints listing.
	constraint PK_tCulture_CultureId primary key (CultureId),
	constraint UK_tCulture_LanguageCode unique (LanguageCode)
);

-- Insertion of the Default profile.
insert into SEC.tCulture (LanguageCode)
values (0), (1);