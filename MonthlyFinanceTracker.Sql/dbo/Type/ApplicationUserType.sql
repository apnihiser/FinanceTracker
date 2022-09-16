CREATE TYPE [dbo].[ApplicationUserType] AS TABLE
(
	[Fullname] VARCHAR(30) NOT NULL,
	[Username] VARCHAR(20) NOT NULL,
	[NormalizedUsername] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(30) NOT NULL,
	[NormalizedEmail] VARCHAR(30) NULL,
	[EmailConfirmed] BIT NULL,
	[PasswordHash] VARCHAR(MAX) NULL,
	[PhoneNumber] NVARCHAR(20) NULL,
	[PhoneNumberConfirmed] BIT NULL,
	[TwoFactorEnabled] BIT NULL
);
