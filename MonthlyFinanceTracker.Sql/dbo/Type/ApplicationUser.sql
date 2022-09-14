CREATE TYPE [dbo].[ApplicationUserType] AS TABLE
(
	[Username] VARCHAR(20) NOT NULL,
	[NormalizedUsername] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(30) NOT NULL,
	[NormalizedEmail] VARCHAR(30) NOT NULL,
	[Fullname] VARCHAR(30) NOT NULL,
	[PasswordHash] VARCHAR(MAX) NOT NULL
);
