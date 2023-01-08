/* TEST DATA */

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
         'AQAAAAEAACcQAAAAENw/mvERdmbGWkFmHMdW07uEDqo/DNeGQj/YAwpOJxpi3QFRMHx3VdiFGB64oDxUOQ==',
         '440-444-4444',
         'False',
         'False'),
         ('test 02',
         'test02@test.com',
         'TEST02@TEST.COM',
         'test02@test.com',
         'TEST02@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAENw/mvERdmbGWkFmHMdW07uEDqo/DNeGQj/YAwpOJxpi3QFRMHx3VdiFGB64oDxUOQ==',
         '440-444-4444',
         'True',
         'True'),
         ('test 03',
         'test03@test.com',
         'TEST03@TEST.COM',
         'test03@test.com',
         'TEST03@TEST.COM',
         'True',
         'AQAAAAEAACcQAAAAENw/mvERdmbGWkFmHMdW07uEDqo/DNeGQj/YAwpOJxpi3QFRMHx3VdiFGB64oDxUOQ==',
         '440-444-4444',
         'False',
         'False'),
         ('demoUser@demo.com',
         'demoUser@demo.com',
         'DEMOUSER@DEMO.COM',
         'demoUser@demo.com',
         'DEMOUSER@DEMO.COM',
         'True',
         'AQAAAAEAACcQAAAAEAZRw6LRO4qGE++hYwPV7dV4dEVQ9KTwis3I0mAMdZ0F5mLo8LjOPzdcCNInvYjgUw==',
         'NULL',
         'False',
         'False')
END

IF NOT EXISTS(SELECT 1 FROM dbo.[Provider])
BEGIN
    DECLARE @UserId1 int;
    DECLARE @UserId2 int;
    DECLARE @UserId3 int;

    SELECT @UserId1 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [NormalizedUsername] = 'TEST01@TEST.COM';
    SELECT @UserId2 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [NormalizedUsername] = 'TEST02@TEST.COM';
    SELECT @UserId3 = ApplicationUserId FROM dbo.[ApplicationUser] WHERE [NormalizedUsername] = 'TEST03@TEST.COM';

    INSERT INTO dbo.[Provider] ([Title],[Service],[URL],[UserId])
    VALUES ('Cable Company','Cable','http://Comcast.com',@UserId1)
          ,('Energy Company','Electricity','http://FirstEnergy.com', @UserId1)
          ,('Gas Company','Gas','http://ColumbiaGas.com', @UserId1)
          ,('Local Municipality','Utilities','http://FirstEnergy.com', @UserId1)
          ,('Netflix','Movie Streaming','http://Netflix.com', @UserId1)

    INSERT INTO dbo.[Account] ([Title],[Description],[Type],[Balance],[ApplicationUserId])
    VALUES ('Credit Union','Local Credit Union Checking','Checking',5596.65,@UserId1)
          ,('Discover Card','Primary Credit Card','Credit',256.55,@UserId1)
          ,('Chase','Grocery Rewards Card','Credit',25.68,@UserId1)
          ,('Key Bank','Savings Account','Savings',125684.22,@UserId1)
          ,('Barclays','CD account','Investment',5000.00,@UserId1)
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

    INSERT INTO dbo.[Transaction] ([AccountId],[PayeeId],[TransactionReason],[Type],[Amount],[DueDate],[Status])
    VALUES (@AccountId1,@ProviderId1,'Bill','Withdrawal',100.00,'2022-11-25 00:00:00.00','Due')
          ,(@AccountId2,@ProviderId2,'Bill','Withdrawal',200.00,'2022-11-01 00:00:00.00','Due')
          ,(@AccountId3,@ProviderId3,'Bill','Withdrawal',300.00,'2022-11-30 12:00:00.00','Due')
          ,(@AccountId4,@ProviderId4,'Bill','Withdrawal',400.00,'2022-11-15 12:00:00.00','Due')
          ,(@AccountId5,@ProviderId5,'Bill','Withdrawal',500.00,'2022-11-02 12:00:00.00','Due')
END