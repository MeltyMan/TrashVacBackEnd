/****** Object:  StoredProcedure [dbo].[sDoor_Persist]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sDoor_Persist]
	@DoorId varchar (50), 
	@Description nvarchar (100), 
	@Deleted bit
AS
BEGIN

	UPDATE 
		dbo.Doors
	SET
		Description = @Description, 
		Deleted = @Deleted, 
		DateLastModified = GETDATE()
	WHERE	
		DoorId = @DoorId;


	IF (@@ROWCOUNT = 0) BEGIN
		INSERT INTO 
			dbo.Doors
		VALUES
			(
				@DoorId, 
				@Description, 
				@Deleted, 
				GETDATE(), 
				GETDATE()
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sRfId_ValidateAccess]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sRfId_ValidateAccess]
	@RfId varchar (50), 
	@DoorId varchar (50),
	@Result bit output, 
	@UserId uniqueidentifier output
AS
BEGIN
	
	SET @Result = CAST(0 as bit);

	SELECT 
		@Result = CAST(1 as bit), 
		@UserId = r.UserId
	FROM 
		dbo.UserRfIdRelations  AS r
	INNER JOIN 
		dbo.RfIdTags AS t ON t.RfId = r.RfId
	INNER JOIN 
		dbo.RfIdDoorAccess AS da ON da.RfId = t.RfId
	WHERE 
		r.RfId = @RfId
		AND r.Deleted = CAST(0 as bit)
		AND t.Deleted = CAST(0 as bit)
		AND da.DoorId = @DoorId
		AND da.Deleted = CAST(0 as bit)
END
GO
/****** Object:  StoredProcedure [dbo].[sRfIdDoorAccess_Persist]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sRfIdDoorAccess_Persist]
	@RfId varchar (50), 
	@DoorId varchar (50), 
	@Deleted bit
AS
BEGIN

	UPDATE 
		dbo.RfIdDoorAccess
	SET 
		Deleted = @Deleted, 
		DateLastModified = GETDATE()
	WHERE	
		RfId = @RfId
		AND DoorId = @DoorId;

	IF (@@ROWCOUNT = 0) BEGIN
		INSERT INTO 
			dbo.RfIdDoorAccess
		VALUES
			(
				@RfId, 
				@DoorId, 
				@Deleted, 
				GETDATE(), 
				GETDATE()
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sRfIdTag_Persist]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sRfIdTag_Persist]
	@RfId varchar (50), 
	@Description nvarchar (100), 
	@Deleted bit
AS
BEGIN



	UPDATE 
		dbo.RfIdTags
	SET
		Description = @Description, 
		Deleted = @Deleted, 
		DateLastModified = GETDATE()
	WHERE
		RfId = @RfId;

	IF (@@ROWCOUNT = 0) BEGIN
		INSERT INTO 
			dbo.RfIdTags
		VALUES
			(
				@RfId, 
				@Description, 
				@Deleted, 
				GETDATE(), 
				GETDATE()
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_Create]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_Create]
	@UserName varchar (255), 
	@PwdHash nvarchar (255), 
	@FirstName nvarchar (50), 
	@LastName nvarchar (100), 
	@UserLevel int, 
	@UserId uniqueidentifier output
AS
BEGIN
	
	SET @UserId = NEWID();

	INSERT INTO 
		dbo.Users
	VALUES
		(
			@UserId, 
			@UserName, 
			@PwdHash, 
			@FirstName, 
			@LastName, 
			@UserLevel, 
			NULL, 
			NULL, 
			GETDATE(), 
			GETDATE()
		);

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_GetById]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_GetById]
	@UserId uniqueidentifier, 
	@Result bit output,
	@UserName varchar (255) output, 
	@FirstName nvarchar (50) output, 
	@LastName nvarchar (100) output, 
	@UserLevel int output
AS
BEGIN

	SET @Result = CAST(0 as bit);

	SELECT
		@Result = CAST(1 as bit), 
		@UserName = u.UserName, 
		@FirstName = u.FirstName, 
		@LastName = u.LastName, 
		@UserLevel = u.UserLevel
	FROM 
		dbo.Users AS u
	WHERE
		u.UserId = @UserId;

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_GetByUserName]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_GetByUserName]
	@UserName varchar (255), 
	@Result bit output, 
	@UserId uniqueidentifier output, 
	@PwdHash nvarchar (255) output, 
	@FirstName nvarchar (255) output, 
	@LastName nvarchar (255) output, 
	@UserLevel int output
AS
BEGIN

	SET @Result = CAST(0 as bit);

	SELECT 
		@Result = CAST(1 as bit), 
		@UserId = u.UserId, 
		@PwdHash = u.PwdHash, 
		@FirstName = u.FirstName, 
		@LastName = u.LastName, 
		@UserLevel = u.UserLevel
	FROM 
		dbo.Users AS u
	WHERE
		u.UserName = @UserName;

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_IsTokenUnique]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_IsTokenUnique]
	@Token nvarchar (255), 
	@Result bit output
AS
BEGIN

	SET @Result = CAST(1 as bit);

	SELECT 
		@Result = CAST(0 as bit)
	FROM 
		dbo.Users AS u
	WHERE
		u.AccessToken = @Token;

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_PersistToken]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_PersistToken]
	@UserId uniqueidentifier, 
	@Token nvarchar (255), 
	@TTL int
AS
BEGIN

	UPDATE 
		dbo.Users
	SET
		AccessToken = @Token, 
		AccessTokenExpireDate = DATEADD(MI, @TTL, GETDATE())
	WHERE	
		UserId = @UserId;

END
GO
/****** Object:  StoredProcedure [dbo].[sUser_ValidateToken]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUser_ValidateToken]
	@Token nvarchar (255),
	@TTL int,
	@Result bit output,
	@UserId uniqueidentifier output
AS
BEGIN
	

	SET @Result = CAST(0 as bit);

	SELECT
		@Result = CAST(1 as bit), 
		@UserId = u.UserId
	FROM 
		dbo.Users AS u
	WHERE
		u.AccessToken = @Token
		AND u.AccessTokenExpireDate > GETDATE();

	IF (@Result = CAST(1 as bit)) BEGIN
		UPDATE
			dbo.Users
		SET 
			AccessTokenExpireDate = DATEADD(MI, @TTL, GETDATE())
		WHERE
			UserId = @UserId;
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sUserRdIdRealation_Persist]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUserRdIdRealation_Persist]
	@UserId uniqueidentifier, 
	@RfId varchar (50), 
	@Deleted bit
AS
BEGIN

	UPDATE 
		dbo.UserRfIdRelations
	SET 
		Deleted = @Deleted, 
		DateLastModified = GETDATE()
	WHERE
		UserId = @UserId
		AND RfId = @RfId;

	IF (@@ROWCOUNT = 0) BEGIN
		INSERT INTO 
			dbo.UserRfIdRelations
		VALUES
			(
				@UserId, 
				@RfId, 
				@Deleted, 
				GETDATE(), 
				GETDATE()
			);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[sUsers_List]    Script Date: 2021-11-17 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sUsers_List]
	
AS
BEGIN

	SELECT 
		u.UserId, 
		u.FirstName, 
		u.LastName, 
		u.UserLevel, 
		u.DateLastModified
	FROM 
		dbo.Users AS u
	ORDER BY 
		u.LastName, 
		u.FirstName

END
GO
