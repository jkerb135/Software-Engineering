USE [ipawsTeamB]
GO
/****** Object:  Database [ipawsTeamB]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE DATABASE [ipawsTeamB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ipawsTeamB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ipawsTeamB.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ipawsTeamB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ipawsTeamB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ipawsTeamB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ipawsTeamB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ipawsTeamB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ipawsTeamB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ipawsTeamB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ipawsTeamB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ipawsTeamB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ipawsTeamB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ipawsTeamB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ipawsTeamB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ipawsTeamB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ipawsTeamB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ipawsTeamB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ipawsTeamB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ipawsTeamB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ipawsTeamB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ipawsTeamB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ipawsTeamB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ipawsTeamB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ipawsTeamB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ipawsTeamB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ipawsTeamB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ipawsTeamB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ipawsTeamB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ipawsTeamB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ipawsTeamB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ipawsTeamB] SET  MULTI_USER 
GO
ALTER DATABASE [ipawsTeamB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ipawsTeamB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ipawsTeamB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ipawsTeamB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ipawsTeamB]
GO
/****** Object:  DatabaseRole [aspnet_WebEvent_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_WebEvent_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Roles_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Roles_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Roles_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Profile_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Profile_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Profile_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Personalization_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Personalization_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Membership_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Membership_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE ROLE [aspnet_Membership_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.ApplicationId = a.ApplicationId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.ApplicationId = a.ApplicationId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END

GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](255) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryAssignments]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryAssignments](
	[AssignedUser] [varchar](255) NULL,
	[CategoryID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompletedMainSteps]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompletedMainSteps](
	[MainStepID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[MainStepName] [varchar](255) NOT NULL,
	[AssignedUser] [varchar](255) NOT NULL,
	[DateTimeComplete] [datetime] NOT NULL,
	[TotalTime] [float] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CompletedMainSteps_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompletedTasks]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompletedTasks](
	[TaskID] [int] NOT NULL,
	[TaskName] [varchar](255) NOT NULL,
	[AssignedUser] [varchar](255) NOT NULL,
	[DateTimeCompleted] [datetime] NOT NULL,
	[TotalTime] [float] NOT NULL,
	[TotalDetailedStepsUsed] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CompletedTasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DetailedSteps]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DetailedSteps](
	[DetailedStepID] [int] IDENTITY(1,1) NOT NULL,
	[MainStepID] [int] NOT NULL,
	[DetailedStepName] [varchar](255) NOT NULL,
	[DetailedStepText] [varchar](255) NULL,
	[ImageFilename] [varchar](255) NULL,
	[ImagePath] [varchar](255) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[ListOrder] [int] NOT NULL,
 CONSTRAINT [PK_DetailedSteps] PRIMARY KEY CLUSTERED 
(
	[DetailedStepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MainSteps]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MainSteps](
	[MainStepID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[MainStepName] [varchar](255) NOT NULL,
	[MainStepText] [varchar](255) NULL,
	[MainStepTime] [float] NULL,
	[AudioFilename] [varchar](255) NULL,
	[AudioPath] [varchar](255) NULL,
	[VideoFilename] [varchar](255) NULL,
	[VideoPath] [varchar](255) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[ListOrder] [int] NOT NULL,
 CONSTRAINT [PK_MainSteps] PRIMARY KEY CLUSTERED 
(
	[MainStepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberAssignments]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberAssignments](
	[AssignedUser] [varchar](255) NOT NULL,
	[AssignedSupervisor] [varchar](255) NULL,
	[IsUserLoggedIn] [bit] NOT NULL,
	[UsersIp] [char](15) NOT NULL,
 CONSTRAINT [PK_MemberAssignments] PRIMARY KEY CLUSTERED 
(
	[AssignedUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Picture] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestedCategories]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestedCategories](
	[CategoryID] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[RequestingUser] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestedTasks]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestedTasks](
	[CategoryID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[Requester] [nvarchar](256) NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskAssignments]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaskAssignments](
	[CategoryID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[AssignedUser] [varchar](255) NULL,
	[TaskTime] [float] NOT NULL,
	[DetailedStepsUsed] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TaskName] [varchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserName] [nvarchar](256) NOT NULL,
	[ConnectionID] [nvarchar](256) NOT NULL,
	[Connected] [bit] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTaskRequest]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTaskRequest](
	[ID] [int] NOT NULL,
	[TaskName] [nvarchar](256) NOT NULL,
	[TaskDescription] [nvarchar](256) NOT NULL,
	[DateCompleted] [datetime] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]

GO
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]

GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]

GO
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]

GO
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]

GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]

GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Paths]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]

GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Shared]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]

GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_User]    Script Date: 12/3/2014 10:04:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Applications_Index]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications]
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Membership_index]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership]
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Paths_index]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Paths_index] ON [dbo].[aspnet_Paths]
(
	[ApplicationId] ASC,
	[LoweredPath] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_PersonalizationPerUser_index1]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_PersonalizationPerUser_index1] ON [dbo].[aspnet_PersonalizationPerUser]
(
	[PathId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Roles_index1]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Roles_index1] ON [dbo].[aspnet_Roles]
(
	[ApplicationId] ASC,
	[LoweredRoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Users_Index]    Script Date: 12/3/2014 10:04:06 PM ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'/', N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'1553e74d-5ef8-446a-b14f-9bc3f34abbb3', N'ku123.', 0, N'QZtykka6f7SjEksZpGn+Bg==', NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(0x0000A3F100E9BF74 AS DateTime), CAST(0x0000A3F100E9BF74 AS DateTime), CAST(0x0000A3F100E9BF74 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'd60cc706-cfe4-4a3d-85d4-139b5d4b8a72', N'4nx1rp16vCeVU2Vnd3mB3M7cUao=', 1, N'kq5UGcjdP8ZMPv9URAsnkQ==', NULL, N'hello@danielptalley.com', N'hello@danielptalley.com', N'supervisor', N'fdd0ev6aTHPgDZlSuwKWjqIMUIw=', 1, 0, CAST(0x0000A39D003E9130 AS DateTime), CAST(0x0000A3F60103DEBE AS DateTime), CAST(0x0000A3D80133BAFA AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'Verified')
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'aef839dc-ee11-4048-a20b-9c205be7ef97', N'eDhbZg1nDJWrkqv6EHS1B+oFtQw=', 1, N'ISfk5cLIbXtTR2KfSEji3g==', NULL, N'hello@danielptalley.com', N'hello@danielptalley.com', N'manager', N'7N4ydfWjs+ifpWUUcQBRPPVznMw=', 1, 0, CAST(0x0000A39D003DE99C AS DateTime), CAST(0x0000A3F60104005D AS DateTime), CAST(0x0000A39D003DE99C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'76cf4ce7-0c78-4bf9-b4d5-cff8d19ef1ae', N'tAYPLO36UKu9lqu4qqNb5+N7as8=', 1, N'kHX+hMtuqBX/v8mp95cRSw==', NULL, N'hello@dpt.com', N'hello@dpt.com', N'user', N'l0CzPtsk5yNOKre/xg5sWFSZFeg=', 1, 0, CAST(0x0000A39D003EA9CC AS DateTime), CAST(0x0000A39D0056C816 AS DateTime), CAST(0x0000A39D003EA9CC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'177d9d0b-d5a7-4116-98d9-39bdce07b515', N'addli@', 0, N'64BLnNHAYw0VCBFF/U7sow==', NULL, N'sdywords@word.com', N'sdywords@word.com', NULL, NULL, 0, 0, CAST(0x0000A3F501123224 AS DateTime), CAST(0x0000A3F501123224 AS DateTime), CAST(0x0000A3F501123224 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'ac810f8d-52e8-454b-ac38-58343d5c8530', N'ku123.', 0, N'5z5tfr4CEhp0PF6xNOnLIA==', NULL, N'tan@kutztown.edu', N'tan@kutztown.edu', NULL, NULL, 1, 0, CAST(0x0000A3D5011CF9E8 AS DateTime), CAST(0x0000A3F50032585B AS DateTime), CAST(0x0000A3DB01436267 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'f3ddd035-1ce6-423c-8088-584cdb485a03', N'ku123.', 0, N'ugohSTGl4DY4+vGiTfBmYw==', NULL, N'tan@kutztown.edu', N'tan@kutztown.edu', NULL, NULL, 1, 0, CAST(0x0000A3D5011CE5FC AS DateTime), CAST(0x0000A3F6001E41AC AS DateTime), CAST(0x0000A3D5011CE5FC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'Verified')
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'5dccfcee-7380-4ac3-af19-d9012249f36c', N'ku123.', 0, N'arbd6q+VsbS/8J94kHwr4g==', NULL, N'tan@kutztown.edu', N'tan@kutztown.edu', NULL, NULL, 1, 0, CAST(0x0000A3D5011CB4C4 AS DateTime), CAST(0x0000A3D5011CB4C4 AS DateTime), CAST(0x0000A3F5010F8268 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'6b41f8e9-35cf-4eeb-9d82-b415f7f16787', N'ku123.', 0, N'A0FvPz8W9AAYiyunnfISuQ==', NULL, N'tan@kutztown.edu', N'tan@kutztown.edu', NULL, NULL, 1, 0, CAST(0x0000A3F100F9BCD0 AS DateTime), CAST(0x0000A3F100F9BCD0 AS DateTime), CAST(0x0000A3F100F9BCD0 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'b613f3c7-ca41-4c9c-a9f8-5096a3e2bc09', N'ku123.', 0, N'Ofx2Vmp7UUqglL2PaDCAUg==', NULL, N'tan@kutztown.edu', N'tan@kutztown.edu', NULL, NULL, 0, 0, CAST(0x0000A3D5011CCE8C AS DateTime), CAST(0x0000A3F6002E11E0 AS DateTime), CAST(0x0000A3D5011CCE8C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'Verified')
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'd7103ec7-594f-4e1e-88b9-effdfcaebe5f', N'this12*', 0, N'/MMyp26l3DTJUngeE8KNbw==', NULL, N'that@that.com', N'that@that.com', NULL, NULL, 1, 0, CAST(0x0000A3F50034E2AC AS DateTime), CAST(0x0000A3F6002E921B AS DateTime), CAST(0x0000A3F50034E2AC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'justLoggedOn')
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'b28a74c1-614b-4b12-9dab-85976aba1e26', N'Manager', N'manager', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308', N'Supervisor', N'supervisor', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd', N'User', N'user', NULL)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'5dccfcee-7380-4ac3-af19-d9012249f36c', N'Bryan Robson', N'bryan robson', NULL, 0, CAST(0x0000A3D5011CB4C4 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'b613f3c7-ca41-4c9c-a9f8-5096a3e2bc09', N'Chris Appleton', N'chris appleton', NULL, 0, CAST(0x0000A3F6002E185B AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'f3ddd035-1ce6-423c-8088-584cdb485a03', N'Dave Mackey', N'dave mackey', NULL, 0, CAST(0x0000A3F6001E6D87 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'1553e74d-5ef8-446a-b14f-9bc3f34abbb3', N'Emre', N'emre', NULL, 0, CAST(0x0000A3F100E9BF74 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'177d9d0b-d5a7-4116-98d9-39bdce07b515', N'jobble', N'jobble', NULL, 0, CAST(0x0000A3F501123224 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'aef839dc-ee11-4048-a20b-9c205be7ef97', N'Manager', N'manager', NULL, 0, CAST(0x0000A3F601040240 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'6b41f8e9-35cf-4eeb-9d82-b415f7f16787', N'Rob Shaw', N'rob shaw', NULL, 0, CAST(0x0000A3F100F9BCD0 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'd60cc706-cfe4-4a3d-85d4-139b5d4b8a72', N'Supervisor', N'supervisor', NULL, 0, CAST(0x0000A3F6010408A8 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'd7103ec7-594f-4e1e-88b9-effdfcaebe5f', N'testsup11', N'testsup11', NULL, 0, CAST(0x0000A3F6002F071C AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'ac810f8d-52e8-454b-ac38-58343d5c8530', N'Tina Pelle', N'tina pelle', NULL, 0, CAST(0x0000A3F50032585B AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b5c2266e-9af5-488c-bff0-7028aa1b98f6', N'76cf4ce7-0c78-4bf9-b4d5-cff8d19ef1ae', N'User', N'user', NULL, 0, CAST(0x0000A39D0056C816 AS DateTime))
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'ac810f8d-52e8-454b-ac38-58343d5c8530', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'1553e74d-5ef8-446a-b14f-9bc3f34abbb3', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'6b41f8e9-35cf-4eeb-9d82-b415f7f16787', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'76cf4ce7-0c78-4bf9-b4d5-cff8d19ef1ae', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'5dccfcee-7380-4ac3-af19-d9012249f36c', N'f0d05c09-b992-45a3-8f5e-4ea772c760fd')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'aef839dc-ee11-4048-a20b-9c205be7ef97', N'b28a74c1-614b-4b12-9dab-85976aba1e26')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'd60cc706-cfe4-4a3d-85d4-139b5d4b8a72', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'177d9d0b-d5a7-4116-98d9-39bdce07b515', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'b613f3c7-ca41-4c9c-a9f8-5096a3e2bc09', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'f3ddd035-1ce6-423c-8088-584cdb485a03', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'd7103ec7-594f-4e1e-88b9-effdfcaebe5f', N'61e7abf3-10ca-4f7d-bb31-cc9eddf78308')
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (1, N'How To Brew Coffee', N'Dave Mackey', CAST(0x0000A3D500DB91EF AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (2, N'Make Sandwiches', N'Dave Mackey', CAST(0x0000A3D500DB9B7A AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (3, N'Library Assignments', N'Dave Mackey', CAST(0x0000A3D500DBA82B AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (8, N'Yard Work', N'Dave Mackey', CAST(0x0000A3DA00F96E0D AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (9, N'Prepare Dinner', N'Dave Mackey', CAST(0x0000A3DA00F98D8B AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (10, N'Making Cookies', N'Dave Mackey', CAST(0x0000A3DB00FA5659 AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (11, N'Test Category One', N'supervisor', CAST(0x0000A3E200A9EDB9 AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (22, N'How To Brew Coffee', N'supervisor', CAST(0x0000A3E300BBFB0C AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (24, N'Appleton''s Jamming Category', N'Chris Appleton', CAST(0x0000A3EE007E00F9 AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (25, N'Cat 1', N'presSup', CAST(0x0000A3EE00BA759D AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (26, N'new category', N'presSup', CAST(0x0000A3EE00BC6ADA AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (27, N'How To Brew Coffee', N'presSup', CAST(0x0000A3EE00BD03E4 AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (28, N'Make Sandwiches', N'presSup', CAST(0x0000A3EE00BD063C AS DateTime), 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [IsActive]) VALUES (29, N'Setting a Table', N'Dave Mackey', CAST(0x0000A3F1009800D2 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Tina Pelle', 1)
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Emre', 27)
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Emre', 1)
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Bryan Robson', 25)
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Bryan Robson', 26)
INSERT [dbo].[CategoryAssignments] ([AssignedUser], [CategoryID]) VALUES (N'Emre', 29)
SET IDENTITY_INSERT [dbo].[CompletedMainSteps] ON 

INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (5, 4, N'Slice of Bread', N'User', CAST(0x0000A3ED01327B81 AS DateTime), 5, 7)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (5, 4, N'Slice of Bread', N'Tina Pelle', CAST(0x0000A3ED0133504B AS DateTime), 0.805, 8)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (6, 4, N'Add Jam', N'Tina Pelle', CAST(0x0000A3ED01335606 AS DateTime), 3.683, 9)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 4, N'Peanut Butter', N'Tina Pelle', CAST(0x0000A3ED0133582F AS DateTime), 1.1, 10)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (8, 4, N'Combine Bread', N'Tina Pelle', CAST(0x0000A3ED013359F6 AS DateTime), 0.905, 11)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (22, 24, N'pour ground into filter', N'Tina Pelle', CAST(0x0000A3ED014D83CA AS DateTime), 3.884, 12)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 10, N'Slice of Bread', N'User', CAST(0x0000A3EE00DCB059 AS DateTime), 5, 13)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 11, N'Slice of Bread', N'User', CAST(0x0000A3EE00DD4707 AS DateTime), 5, 14)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (5, 4, N'Slice of Bread', N'Tina Pelle', CAST(0x0000A3F500D76824 AS DateTime), 4.352, 15)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (6, 4, N'Add Jam', N'Tina Pelle', CAST(0x0000A3F500D76A23 AS DateTime), 0.922, 16)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 4, N'Peanut Butter', N'Tina Pelle', CAST(0x0000A3F500D76C80 AS DateTime), 0.731, 17)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (8, 4, N'Combine Bread', N'Tina Pelle', CAST(0x0000A3F500D77101 AS DateTime), 3.308, 18)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (31, 49, N'Set placemats', N'Emre', CAST(0x0000A3F500F73E37 AS DateTime), 46.432, 19)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (34, 49, N'Set plates', N'Emre', CAST(0x0000A3F500F76BCF AS DateTime), 35.858, 20)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (35, 49, N'Set utensils', N'Emre', CAST(0x0000A3F500F78CAD AS DateTime), 25.53, 21)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (36, 49, N'Set cups', N'Emre', CAST(0x0000A3F500F7C534 AS DateTime), 46.086, 22)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (17, 1, N'Place a SMALL coffee filter into the coffee carrier - UPDATED 11/7', N'Emre', CAST(0x0000A3F500F8B6C2 AS DateTime), 45.593, 23)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (5, 4, N'Slice of Bread', N'Tina Pelle', CAST(0x0000A3F500F8D76D AS DateTime), 0.485, 24)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (6, 4, N'Add Jam', N'Tina Pelle', CAST(0x0000A3F500F8D89D AS DateTime), 0.51, 25)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 4, N'Peanut Butter', N'Tina Pelle', CAST(0x0000A3F500F8D9DC AS DateTime), 0.573, 26)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (8, 4, N'Combine Bread', N'Tina Pelle', CAST(0x0000A3F500F8DB6A AS DateTime), 0.795, 27)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (5, 4, N'Slice of Bread', N'Tina Pelle', CAST(0x0000A3F500F8E899 AS DateTime), 0.446, 28)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (6, 4, N'Add Jam', N'Tina Pelle', CAST(0x0000A3F500F8E984 AS DateTime), 0.354, 29)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (7, 4, N'Peanut Butter', N'Tina Pelle', CAST(0x0000A3F500F8EA65 AS DateTime), 0.396, 30)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (8, 4, N'Combine Bread', N'Tina Pelle', CAST(0x0000A3F500F8EB95 AS DateTime), 0.558, 31)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (17, 1, N'Place a SMALL coffee filter into the coffee carrier - UPDATED 11/7', N'Tina Pelle', CAST(0x0000A3F500F8FF8B AS DateTime), 4.593, 32)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (18, 1, N'Add coffee powder to coffee filter', N'Tina Pelle', CAST(0x0000A3F500F9016E AS DateTime), 0.808, 33)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (19, 1, N'Add water to the coffee maker', N'Tina Pelle', CAST(0x0000A3F500F90287 AS DateTime), 0.521, 34)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (17, 1, N'Place a SMALL coffee filter into the coffee carrier - UPDATED 11/7', N'Emre', CAST(0x0000A3F500F9D19B AS DateTime), 38.201, 35)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (18, 1, N'Add coffee powder to coffee filter', N'Emre', CAST(0x0000A3F500F9E3F8 AS DateTime), 13.855, 36)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (19, 1, N'Add water to the coffee maker', N'Emre', CAST(0x0000A3F500FA0026 AS DateTime), 21.948, 37)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (31, 49, N'Set placemats', N'Emre', CAST(0x0000A3F500FC13A3 AS DateTime), 13.003, 38)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (34, 49, N'Set plates', N'Emre', CAST(0x0000A3F500FC18F3 AS DateTime), 3.065, 39)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (35, 49, N'Set utensils', N'Emre', CAST(0x0000A3F500FC2000 AS DateTime), 4.354, 40)
INSERT [dbo].[CompletedMainSteps] ([MainStepID], [TaskID], [MainStepName], [AssignedUser], [DateTimeComplete], [TotalTime], [id]) VALUES (36, 49, N'Set cups', N'Emre', CAST(0x0000A3F500FC2B47 AS DateTime), 7.609, 41)
SET IDENTITY_INSERT [dbo].[CompletedMainSteps] OFF
SET IDENTITY_INSERT [dbo].[CompletedTasks] ON 

INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (1, N'Make Hazelnut Coffee', N'User', CAST(0x0000A3ED013A91AA AS DateTime), 5, 0, 3)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (2, N'How To Brew Coffee', N'User', CAST(0x0000A3ED013AC2FA AS DateTime), 5, 0, 4)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3ED013B53C2 AS DateTime), 11, 0, 5)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3EE00DADA5D AS DateTime), 11, 0, 6)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3EE00DAE949 AS DateTime), 11, 0, 7)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3EE00DB0497 AS DateTime), 11, 0, 8)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3EE00DB3D14 AS DateTime), 11, 0, 9)
INSERT [dbo].[CompletedTasks] ([TaskID], [TaskName], [AssignedUser], [DateTimeCompleted], [TotalTime], [TotalDetailedStepsUsed], [id]) VALUES (3, N'How To Brew Coffee', N'User', CAST(0x0000A3EE00DB4133 AS DateTime), 11, 0, 10)
SET IDENTITY_INSERT [dbo].[CompletedTasks] OFF
SET IDENTITY_INSERT [dbo].[DetailedSteps] ON 

INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (5, 6, N'Add Strawberry Jam', N'Get a knife and add jam to one slice of bread.', NULL, NULL, CAST(0x0000A3D500DEA818 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (6, 7, N'Add PB', N'Use a different knife to add peanut butter to the 2nd slice of bread.', NULL, NULL, CAST(0x0000A3D500DEB37B AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (7, 8, N'Combine the two slices of bread into PB & J', N'There are no detailed step text  in this detail step.', N'Female with Goggles.jpg', N'~/Uploads/Female with Goggles.jpg', CAST(0x0000A3D500DF0F63 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (8, 9, N'Serve on a plate', N'Place the bread on a plate and serve.', N'Music Notes.gif', N'~/Uploads/Music Notes.gif', CAST(0x0000A3D500DF2300 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (12, 17, N'Coffee Filter', N'Look in the cabinet for a small paper filter', N'Arch Yoga.jpg', N'~/Uploads/Arch Yoga.jpg', CAST(0x0000A3DA00F54FEF AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (13, 17, N'Filter in Cup Holder', N'Place the filter into the coffee holder', N'Sunrise Yoga.jpg', N'~/Uploads/Sunrise Yoga.jpg', CAST(0x0000A3DA00F58591 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (14, 18, N'Choose Gevalia Coffee Powder', N'Look for the coffee brand Gevalia', N'Female with Goggles.jpg', N'~/Uploads/Female with Goggles.jpg', CAST(0x0000A3DA00F5E377 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (15, 18, N'Three Teaspoons', N'Get a teaspoon and add 3 spoonful of coffee into the filter', N'Female with Goggles.jpg', N'~/Uploads/Female with Goggles.jpg', CAST(0x0000A3DA00F6261E AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (16, 18, N'Teaspoon on Table', N'Place the teaspoon on the table beside the coffee maker', N'Fish 3.jpg', N'~/Uploads/Fish 3.jpg', CAST(0x0000A3DA00F6648E AS DateTime), 3)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (17, 19, N'Add water', N'Add 12oz of filtered water into the coffee maker', N'Fish 3.jpg', N'~/Uploads/Fish 3.jpg', CAST(0x0000A3DA00F69A0F AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (18, 20, N'On Switch', N'Acitvate the ON switch on the coffee maker', N'Blackboard and Chef.jpg', N'~/Uploads/Blackboard and Chef.jpg', CAST(0x0000A3DA00F6CBD4 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (19, 6, N'Add Blueberry Jam', N'Now add blueberry jam to the slice of bread.', N'Blackboard and Chef.jpg', N'~/Uploads/Blackboard and Chef.jpg', CAST(0x0000A3DA00F8EF49 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (20, 24, N'Really long and detailed detailed step that runs over and must be scrolled', NULL, NULL, NULL, CAST(0x0000A3E50189DAFF AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (21, 25, N'Use wheat... everytime!', NULL, N'fresh-sliced-bread.jpg', N'~/Uploads/fresh-sliced-bread.jpg', CAST(0x0000A3EE00BBD1AF AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (24, 34, N'Get 8 big plates from cabinet', N'Go to the cabinet and take 8 white plates.', N'Female with Goggles.jpg', N'~/Uploads/Female with Goggles.jpg', CAST(0x0000A3F1009BE40C AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (25, 31, N'Get placemats', N'Go to the cabinet and take eight (8) red placemats.', N'Sunrise Yoga.jpg', N'~/Uploads/Sunrise Yoga.jpg', CAST(0x0000A3F1009C1B1C AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (26, 31, N'Position placemats', N'Put one placemat on table in front of each chair position', N'Soil.jpg', N'~/Uploads/Soil.jpg', CAST(0x0000A3F1009C5349 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (27, 34, N'Place plates', N'Place one plate on each placemat.', N'Wind Power.jpg', N'~/Uploads/Wind Power.jpg', CAST(0x0000A3F1009C8999 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (28, 35, N'Set utensils', N'Get 8 forks, spoons and knives each from cabinet', N'Chef.jpg', N'~/Uploads/Chef.jpg', CAST(0x0000A3F1009CB890 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (29, 35, N'Position forks & spoons', N'Put one fork and one spoon on left side of the plate.', NULL, NULL, CAST(0x0000A3F1009CEA50 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (30, 35, N'Position knives', N'Put one knife on right side of each plate.', NULL, NULL, CAST(0x0000A3F1009D0BC0 AS DateTime), 3)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (31, 36, N'Get cups', N'Get 8 cups from the cabinet/', N'Arch Yoga.jpg', N'~/Uploads/Arch Yoga.jpg', CAST(0x0000A3F1009D5A44 AS DateTime), 1)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (32, 36, N'Place cups', N'Put one cup on the right side of each plate.', NULL, NULL, CAST(0x0000A3F1009D9D57 AS DateTime), 2)
INSERT [dbo].[DetailedSteps] ([DetailedStepID], [MainStepID], [DetailedStepName], [DetailedStepText], [ImageFilename], [ImagePath], [CreatedTime], [ListOrder]) VALUES (33, 37, N'Set napkins', N'Place one napkin on each plate.', N'Girl and Fish.jpg', N'~/Uploads/Girl and Fish.jpg', CAST(0x0000A3F1009DBEFE AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[DetailedSteps] OFF
SET IDENTITY_INSERT [dbo].[MainSteps] ON 

INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (5, 4, N'Slice of Bread', N'Take 2 slices of bread and place them on a plate.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3D500DCF1D9 AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (6, 4, N'Add Jam', N'Get the strawberry jam from the fridge.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3D500DCFAA2 AS DateTime), 2)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (7, 4, N'Peanut Butter', N'Get peanut butter from the cabinet.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3D500DD049F AS DateTime), 3)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (8, 4, N'Combine Bread', N'Place one slice of bread on top of the other slice.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3D500DD0E93 AS DateTime), 4)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (9, 4, N'Cut to Half', N'Use a knife to cut the two slices of bread into half - diagonally.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3D500DD16F6 AS DateTime), 5)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (17, 1, N'Place a SMALL coffee filter into the coffee carrier - UPDATED 11/7', N'added on 11/5/14 ...', NULL, N'The Sound Of Silence.mp3', N'~/Uploads/The Sound Of Silence.mp3', N'Lego.mp4', N'~/Uploads/Lego.mp4', CAST(0x0000A3DA00F3047D AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (18, 1, N'Add coffee powder to coffee filter', N'added 11/5/14 - Lego.mp4', NULL, N'audio1.mp3', N'~/Uploads/audio1.mp3', N'Lego.mp4', N'~/Uploads/Lego.mp4', CAST(0x0000A3DA00F34DAD AS DateTime), 2)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (19, 1, N'Add water to the coffee maker', N'added 11/5/14 - audio2.mp3 and movie.mp4', NULL, N'audio1.mp3', N'~/Uploads/audio1.mp3', N'movie.mp4', N'~/Uploads/movie.mp4', CAST(0x0000A3DA00F3FBF1 AS DateTime), 3)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (20, 1, N'Turn coffee maker switch to On position', N'added 11/5/14 - movie.mp4', NULL, N'audio1.mp3', N'~/Uploads/audio1.mp3', N'movie.mp4', N'~/Uploads/movie.mp4', CAST(0x0000A3DA00F4D0A3 AS DateTime), 4)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (22, 24, N'pour ground into filter', N'pour the coffee grounds into the filter and place it in the coffee maker', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3E5011C2291 AS DateTime), 2)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (23, 24, N'add water', N'pour water into coffee maker', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3E5011DFEF5 AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (24, 29, N'Test1', N'This is a test', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3E501896B9C AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (25, 37, N'Slice bread', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3EE00BB26AD AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (26, 37, N'Add peanut butter', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3EE00BB3D2A AS DateTime), 2)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (27, 37, N'Add fluff', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3EE00BB4562 AS DateTime), 3)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (28, 37, N'Combine halves ', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3EE00BB5161 AS DateTime), 4)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (29, 37, N'Slice bread', NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3EE00BB5AB3 AS DateTime), 5)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (30, 37, N'Enjoy', NULL, NULL, N'It''s Peanut Butter Jelly Time!!!.mp3', N'~/Uploads/It''s Peanut Butter Jelly Time!!!.mp3', NULL, NULL, CAST(0x0000A3EE00BBFD7B AS DateTime), 6)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (31, 49, N'Set placemats', N'This step tells how to set placemats on the table', NULL, N'The Sound Of Silence.mp3', N'~/Uploads/The Sound Of Silence.mp3', N'Lego.mp4', N'~/Uploads/Lego.mp4', CAST(0x0000A3F10098D4FD AS DateTime), 1)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (34, 49, N'Set plates', N'This step tells how to put plates on the table.', NULL, NULL, NULL, N'bear hunting.mp4', N'~/Uploads/bear hunting.mp4', CAST(0x0000A3F1009A1556 AS DateTime), 2)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (35, 49, N'Set utensils', N'This step explains where to place utensils beside the plate.', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A3F1009A5B8A AS DateTime), 3)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (36, 49, N'Set cups', N'Tells where to place cups.', NULL, N'Piano Music.wav', N'~/Uploads/Piano Music.wav', N'Lego.mp4', N'~/Uploads/Lego.mp4', CAST(0x0000A3F1009A96CB AS DateTime), 5)
INSERT [dbo].[MainSteps] ([MainStepID], [TaskID], [MainStepName], [MainStepText], [MainStepTime], [AudioFilename], [AudioPath], [VideoFilename], [VideoPath], [CreatedTime], [ListOrder]) VALUES (37, 49, N'Set placemats', N'This step tells how to set placemats on the table', NULL, NULL, NULL, N'movie.mp4', N'~/Uploads/movie.mp4', CAST(0x0000A3F1009ACCF0 AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[MainSteps] OFF
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'Bryan Robson', N'presSup', 0, N'000.000.0.0    ')
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'Emre', N'Dave Mackey', 0, N'000.000.0.0    ')
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'New USer Create', N'Chris Appleton', 0, N'000.000.0.0    ')
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'Rob Shaw', N'jobble', 0, N'000.000.0.0    ')
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'Tina Pelle', N'Dave Mackey', 0, N'000.000.0.0    ')
INSERT [dbo].[MemberAssignments] ([AssignedUser], [AssignedSupervisor], [IsUserLoggedIn], [UsersIp]) VALUES (N'User', N'Chris Appleton', 0, N'000.000.0.0    ')
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Name], [Picture]) VALUES (1, N'Dave Mackey', N'~/Uploads/kutztown-university-spotlight-tmb-original.png')
INSERT [dbo].[Profile] ([Id], [Name], [Picture]) VALUES (2, N'supervisor', N'~/Uploads/backgrounddefault.jpg')
INSERT [dbo].[Profile] ([Id], [Name], [Picture]) VALUES (3, N'testsup11', N'~/Uploads/nope.gif')
SET IDENTITY_INSERT [dbo].[Profile] OFF
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (11, 1, N'dave mackey', N'Supervisor', CAST(0x0000A3E200E3AFEF AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (22, 1, N'dave mackey', N'Supervisor', CAST(0x0000A3ED012708EB AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (25, 0, N'Dave Mackey', N'presSup', CAST(0x0000A3EE00BDAF85 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (25, 0, N'supervisor', N'presSup', CAST(0x0000A3EE00CA12B6 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (26, 0, N'supervisor', N'presSup', CAST(0x0000A3EE00CA1651 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (27, 0, N'supervisor', N'presSup', CAST(0x0000A3EE00CA1888 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (24, 0, N'supervisor', N'Chris Appleton', CAST(0x0000A3F50166A8A4 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (22, 1, N'testsup11', N'Supervisor', CAST(0x0000A3F50167CCC5 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (1, 1, N'supervisor', N'Dave Mackey', CAST(0x0000A3E300BB8F13 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (1, 1, N'presSup', N'Dave Mackey', CAST(0x0000A3EE00BCD8F0 AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (2, 1, N'presSup', N'Dave Mackey', CAST(0x0000A3EE00BCDDEB AS DateTime))
INSERT [dbo].[RequestedCategories] ([CategoryID], [IsApproved], [RequestingUser], [CreatedBy], [Date]) VALUES (11, 1, N'testsup11', N'Supervisor', CAST(0x0000A3F5015893BE AS DateTime))
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (1, 1, N'Make Hazelnut Coffee', 1, CAST(0x0000A3D500DBD25F AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (2, 1, N'Make a Pumpkin Spice Latte', 1, CAST(0x0000A3D500DBE309 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (3, 1, N'Make a Caramel Mocha', 1, CAST(0x0000A3D500DBFE61 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (4, 2, N'PB & J Sandwich', 1, CAST(0x0000A3D500DC1FDF AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (5, 3, N'Sort Books', 1, CAST(0x0000A3D500DC3466 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (6, 3, N'Check Returned Books', 1, CAST(0x0000A3D500DC404A AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (10, 9, N'Make Pasta', 1, CAST(0x0000A3DA00F9A1E8 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (11, 8, N'Add Mutch', 1, CAST(0x0000A3DA00F9C1FD AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (12, 8, N'Cut Grass', 1, CAST(0x0000A3DA00F9CED7 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (13, 9, N'Make Pizza', 1, CAST(0x0000A3DA00F9F13F AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (14, 9, N'Make Dinner Rolls', 1, CAST(0x0000A3DA00FA14CA AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (15, 10, N'Peanut Butter Cookies', 1, CAST(0x0000A3DB00FA79AC AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (16, 10, N'Oatmeal Cookies', 1, CAST(0x0000A3E300B248FE AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (17, 10, N'Chocolate Chip Cookies', 1, CAST(0x0000A3E300B25AC5 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (18, 10, N'Macademia Nut Cookeis', 1, CAST(0x0000A3E300B2B421 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (19, 8, N'Trim Edges', 1, CAST(0x0000A3E300B2DBF4 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (20, 3, N'Restock Books', 1, CAST(0x0000A3E300B30E54 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (21, 2, N'Turkey Sandwich', 1, CAST(0x0000A3E300B34BDE AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (22, 2, N'Ham Sandwich', 1, CAST(0x0000A3E300B370D2 AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (23, 1, N'Make an Expresso', 1, CAST(0x0000A3E300B385AD AS DateTime), N'Dave Mackey')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (24, 22, N'Make Hazelnut Coffee', 1, CAST(0x0000A3D500DBD25F AS DateTime), N'supervisor')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (25, 22, N'Make a Pumpkin Spice Latte', 1, CAST(0x0000A3D500DBE309 AS DateTime), N'supervisor')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (26, 22, N'Make a Caramel Mocha', 1, CAST(0x0000A3D500DBFE61 AS DateTime), N'supervisor')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (27, 22, N'Make an Expresso', 1, CAST(0x0000A3E300B385AD AS DateTime), N'supervisor')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (28, 11, N'Test task 01', 1, CAST(0x0000A3E5011B8936 AS DateTime), N'supervisor ')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (29, 11, N'Test Task 2', 1, CAST(0x0000A3E501895BEB AS DateTime), N'supervisor')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (34, 11, N'test task 3', 0, CAST(0x0000A3EE001C9FCF AS DateTime), N'supervisor ')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (35, 24, N'Strawberry Jamming', 1, CAST(0x0000A3EE007E3EBC AS DateTime), N'Chris Appleton')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (36, 24, N'Blueberry Jamming', 1, CAST(0x0000A3EE007E8EE7 AS DateTime), N'Chris Appleton')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (37, 25, N'Peanut Butter and Fluff', 1, CAST(0x0000A3EE00BABB8B AS DateTime), N'presSup')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (46, 28, N'PB & J Sandwich', 1, CAST(0x0000A3D500DC1FDF AS DateTime), N'presSup')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (47, 28, N'Turkey Sandwich', 1, CAST(0x0000A3E300B34BDE AS DateTime), N'presSup')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (48, 28, N'Ham Sandwich', 1, CAST(0x0000A3E300B370D2 AS DateTime), N'presSup')
INSERT [dbo].[Tasks] ([TaskID], [CategoryID], [TaskName], [IsActive], [CreatedTime], [CreatedBy]) VALUES (49, 29, N'Big Table Setting', 1, CAST(0x0000A3F1009827F4 AS DateTime), N'Dave Mackey')
SET IDENTITY_INSERT [dbo].[Tasks] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'', N'd9835d5b-7c34-4d5a-9cf5-883ae85f6fee', 0, 19)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'chris appleton', N'3f9cfa0b-4c52-44e2-9609-ec46d0396b0f', 0, 16)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'dave mackey', N'5fdeccf2-7570-4fb4-be76-4a3e9062d237', 0, 13)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'manager', N'4482a47b-8129-4b96-a656-795f3871ae85', 0, 14)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'modal test', N'eba9b6de-ac13-4fa2-87cb-fc2205105b8c', 0, 17)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'pressup', N'd3cea231-41b1-43e0-9ba5-1f46ab0da081', 0, 18)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'supervisor', N'12e1d301-6003-41e7-8369-78ef35676684', 0, 12)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'supervisor2', N'4b411744-a831-4426-8bb1-bd4090510fb8', 0, 15)
INSERT [dbo].[User] ([UserName], [ConnectionID], [Connected], [Id]) VALUES (N'testsup11', N'2556389a-cdfe-49c7-9d7d-1b6a9f4a1ba6', 0, 20)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Index [PK__aspnet_A__C93A4C98AE22F64C]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__aspnet_A__17477DE4B05C0744]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__aspnet_A__309103316423161A]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_M__1788CC4DD7BD5970]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Membership] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_P__CD67DC589D4CC071]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Paths] ADD PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_P__3214EC06B24CE3A9]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_PersonalizationPerUser_ncindex2]    Script Date: 12/3/2014 10:04:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [aspnet_PersonalizationPerUser_ncindex2] ON [dbo].[aspnet_PersonalizationPerUser]
(
	[UserId] ASC,
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_R__8AFACE1BA9487AFD]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Roles] ADD PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_U__1788CC4DD1B274B8]    Script Date: 12/3/2014 10:04:07 PM ******/
ALTER TABLE [dbo].[aspnet_Users] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_Users_Index2]    Script Date: 12/3/2014 10:04:07 PM ******/
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_UsersInRoles_index]    Script Date: 12/3/2014 10:04:07 PM ******/
CREATE NONCLUSTERED INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[aspnet_Applications] ADD  DEFAULT (newid()) FOR [ApplicationId]
GO
ALTER TABLE [dbo].[aspnet_Membership] ADD  DEFAULT ((0)) FOR [PasswordFormat]
GO
ALTER TABLE [dbo].[aspnet_Paths] ADD  DEFAULT (newid()) FOR [PathId]
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[aspnet_Roles] ADD  DEFAULT (newid()) FOR [RoleId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (newid()) FOR [UserId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (NULL) FOR [MobileAlias]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT ((0)) FOR [IsAnonymous]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CompletedTasks] ADD  CONSTRAINT [DF_CompletedTasks_TotalDetailedStepsUsed]  DEFAULT ((0)) FOR [TotalDetailedStepsUsed]
GO
ALTER TABLE [dbo].[DetailedSteps] ADD  CONSTRAINT [DF_DetailedSteps_ListOrder]  DEFAULT ((0)) FOR [ListOrder]
GO
ALTER TABLE [dbo].[MainSteps] ADD  CONSTRAINT [DF_MainSteps_ListOrder]  DEFAULT ((0)) FOR [ListOrder]
GO
ALTER TABLE [dbo].[MemberAssignments] ADD  CONSTRAINT [DF_MemberAssignments_IsUserLoggedIn]  DEFAULT ((0)) FOR [IsUserLoggedIn]
GO
ALTER TABLE [dbo].[MemberAssignments] ADD  CONSTRAINT [DF_MemberAssignments_UsersIp]  DEFAULT ('000.000.0.0') FOR [UsersIp]
GO
ALTER TABLE [dbo].[TaskAssignments] ADD  CONSTRAINT [DF_TaskAssignments_TaskTime]  DEFAULT ((0)) FOR [TaskTime]
GO
ALTER TABLE [dbo].[TaskAssignments] ADD  CONSTRAINT [DF_TaskAssignments_DetailedStepsUsed]  DEFAULT ((0)) FOR [DetailedStepsUsed]
GO
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[CategoryAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CategoryAssignments_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryAssignments] CHECK CONSTRAINT [FK_CategoryAssignments_Categories]
GO
ALTER TABLE [dbo].[CategoryAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CategoryAssignments_MemberAssignments] FOREIGN KEY([AssignedUser])
REFERENCES [dbo].[MemberAssignments] ([AssignedUser])
GO
ALTER TABLE [dbo].[CategoryAssignments] CHECK CONSTRAINT [FK_CategoryAssignments_MemberAssignments]
GO
ALTER TABLE [dbo].[CompletedMainSteps]  WITH CHECK ADD  CONSTRAINT [FK_CompletedMainSteps_MainSteps] FOREIGN KEY([MainStepID])
REFERENCES [dbo].[MainSteps] ([MainStepID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompletedMainSteps] CHECK CONSTRAINT [FK_CompletedMainSteps_MainSteps]
GO
ALTER TABLE [dbo].[CompletedMainSteps]  WITH CHECK ADD  CONSTRAINT [FK_CompletedMainSteps_MemberAssignments] FOREIGN KEY([AssignedUser])
REFERENCES [dbo].[MemberAssignments] ([AssignedUser])
GO
ALTER TABLE [dbo].[CompletedMainSteps] CHECK CONSTRAINT [FK_CompletedMainSteps_MemberAssignments]
GO
ALTER TABLE [dbo].[CompletedMainSteps]  WITH CHECK ADD  CONSTRAINT [FK_CompletedMainSteps_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[CompletedMainSteps] CHECK CONSTRAINT [FK_CompletedMainSteps_Tasks]
GO
ALTER TABLE [dbo].[DetailedSteps]  WITH CHECK ADD  CONSTRAINT [FK_DetailedSteps_MainSteps] FOREIGN KEY([MainStepID])
REFERENCES [dbo].[MainSteps] ([MainStepID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetailedSteps] CHECK CONSTRAINT [FK_DetailedSteps_MainSteps]
GO
ALTER TABLE [dbo].[MainSteps]  WITH CHECK ADD  CONSTRAINT [FK_MainSteps_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MainSteps] CHECK CONSTRAINT [FK_MainSteps_Tasks]
GO
ALTER TABLE [dbo].[RequestedCategories]  WITH CHECK ADD  CONSTRAINT [FK_RequestedCategories_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[RequestedCategories] CHECK CONSTRAINT [FK_RequestedCategories_Categories]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Categories]
GO
USE [master]
GO
ALTER DATABASE [ipawsTeamB] SET  READ_WRITE 
GO
