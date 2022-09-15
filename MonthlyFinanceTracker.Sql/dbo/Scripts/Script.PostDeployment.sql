﻿/* TEST DATA */

IF NOT EXISTS(SELECT 1 from dbo.[ApplicationUser])
BEGIN
    INSERT INTO dbo.ApplicationUser (
        [Fullname],
        [UserName],
        [NormalizedUserName],
        [Email],
        [NormalizedEmail],
        [EmailConfirmed],
        [PasswordHash],
        [PhoneNumber],
        [PhoneNumberConfirmed],
        [TwoFactorEnabled])
    Values 
        ('test 01',
         'test01@test.com',
         'TEST01@TEST.COM',
         'test01@test.com',
         'TEST01@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAEKKM+a4iuZmbmtEVIjRiUqTZDoJ3mUvpWjTUH2hxhv9selIdpQtqPxIeTdEycVNzKQ==',
         '440-444-4444',
         'True',
         'True'),
         ('test 02',
         'test02@test.com',
         'TEST02@TEST.COM',
         'test02@test.com',
         'TEST02@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAEKKM+a4iuZmbmtEVIjRiUqTZDoJ3mUvpWjTUH2hxhv9selIdpQtqPxIeTdEycVNzKQ==',
         '440-444-4444',
         'True',
         'True'),
         ('test 03',
         'test03@test.com',
         'TEST03@TEST.COM',
         'test03@test.com',
         'TEST03@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAEKKM+a4iuZmbmtEVIjRiUqTZDoJ3mUvpWjTUH2hxhv9selIdpQtqPxIeTdEycVNzKQ==',
         '440-444-4444',
         'True',
         'True'),
         ('test 04',
         'test04@test.com',
         'TEST04@TEST.COM',
         'test04@test.com',
         'TEST04@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAEKKM+a4iuZmbmtEVIjRiUqTZDoJ3mUvpWjTUH2hxhv9selIdpQtqPxIeTdEycVNzKQ==',
         '440-444-4444',
         'True',
         'True'),
         ('test 05',
         'test05@test.com',
         'TEST05@TEST.COM',
         'test05@test.com',
         'TEST05@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAEKKM+a4iuZmbmtEVIjRiUqTZDoJ3mUvpWjTUH2hxhv9selIdpQtqPxIeTdEycVNzKQ==',
         '440-444-4444',
         'True',
         'True')
END

IF NOT EXISTS(SELECT 1 FROM dbo.[Provider])
BEGIN
    DECLARE @UserId1 int;
    DECLARE @UserId2 int;
    DECLARE @UserId3 int;
    DECLARE @UserId4 int;
    DECLARE @UserId5 int;

    SELECT @UserId1 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [UserName] = 'test01';
    SELECT @UserId2 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [UserName] = 'test02';
    SELECT @UserId3 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [UserName] = 'test03';
    SELECT @UserId4 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [UserName] = 'test04';
    SELECT @UserId5 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [UserName] = 'test05';

    INSERT INTO dbo.[Provider] ([Title],[Service],[URL],[UserId])
    VALUES ('Cable Company','Cable','http://Comcast.com',@UserId1)
          ,('Energy Company','Electricity','http://FirstEnergy.com', @UserId2)
          ,('Gas Company','Gas','http://ColumbiaGas.com', @UserId3)
          ,('Local Municipality','Utilities','http://FirstEnergy.com', @UserId4)
          ,('Netflix','Movie Streaming','http://Netflix.com', @UserId5)

    INSERT INTO dbo.[Account] ([Title],[Description],[Type],[Balance],[ApplicationUserId])
    VALUES ('Credit Union','Local Credit Union Checking','Checking',5596.65,@UserId1)
          ,('Discover Card','Primary Credit Card','Credit',256.55,@UserId2)
          ,('Chase','Grocery Rewards Card','Credit',25.68,@UserId3)
          ,('Key Bank','Savings Account','Savings',125684.22,@UserId4)
          ,('Barclays','CD account','Investment',5000.00,@UserId5)
END

IF NOT EXISTS(SELECT 1 FROM dbo.[Transaction])
BEGIN
    DECLARE @AccountId1 int;
    DECLARE @AccountId2 int;
    DECLARE @AccountId3 int;
    DECLARE @AccountId4 int;
    DECLARE @AccountId5 int;
    
    SELECT @AccountId1 = Id FROM dbo.[Account] WHERE Title = 'Credit Union';
    SELECT @AccountId2 = Id FROM dbo.[Account] WHERE Title = 'Discover Card';
    SELECT @AccountId3 = Id FROM dbo.[Account] WHERE Title = 'Chase';
    SELECT @AccountId4 = Id FROM dbo.[Account] WHERE Title = 'Key Bank';
    SELECT @AccountId5 = Id FROM dbo.[Account] WHERE Title = 'Barclays';

    DECLARE @ProviderId1 int;
    DECLARE @ProviderId2 int;
    DECLARE @ProviderId3 int;
    DECLARE @ProviderId4 int;
    DECLARE @ProviderId5 int;

    SELECT @ProviderId1 = Id FROM dbo.[Provider] WHERE Title = 'Cable Company';
    SELECT @ProviderId2 = Id FROM dbo.[Provider] WHERE Title = 'Energy Company';
    SELECT @ProviderId3 = Id FROM dbo.[Provider] WHERE Title = 'Gas Company';
    SELECT @ProviderId4 = Id FROM dbo.[Provider] WHERE Title = 'Local Municipality';
    SELECT @ProviderId5 = Id FROM dbo.[Provider] WHERE Title = 'Netflix';

    INSERT INTO dbo.[Transaction] ([AccountId],[PayeeId],[Amount],[DueDate],[Status])
    VALUES (@AccountId1,@ProviderId1,100.00,'01/01/2022','Due')
          ,(@AccountId2,@ProviderId2,200.00,'01/02/2022','Past Due')
          ,(@AccountId3,@ProviderId3,300.00,'01/03/2022','Reconcilled')
          ,(@AccountId4,@ProviderId4,400.00,'01/04/2022','Due')
          ,(@AccountId5,@ProviderId5,500.00,'01/05/2022','Reconcilled')
END