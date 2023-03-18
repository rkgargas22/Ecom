
Create Database [tmfEcom]

USE [tmfEcom]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 2023-03-18 11:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
	[RequestHttpVerb] [nvarchar](20) NOT NULL,
	[RequestBody] [nvarchar](max) NULL,
	[ResponseBody] [nvarchar](max) NULL,
	[ResponseStatusCode] [int] NULL,
	[IsSuccess] [bit] NULL,
	[BpNo] [nvarchar](20) NOT NULL,
	[UserType] [nvarchar](20) NOT NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  StoredProcedure [dbo].[usp_add_logs]    Script Date: 2023-03-18 11:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_add_logs]	
	@Url [nvarchar](200),
	@RequestHttpVerb [nvarchar](20),
	@RequestBody [nvarchar](max) NULL,
	@BpNo [nvarchar](20),
	@UserType [nvarchar](20),
	@CreatedBy [nvarchar](20),
	@CreatedDate datetime
	
AS
BEGIN
	DECLARE @GuidTable TABLE (
		Id uniqueidentifier
	)

	INSERT INTO [dbo].[Logs]
           ([Url]
           ,[RequestHttpVerb]
           ,[RequestBody]
           ,[BpNo]
           ,[UserType]
           ,[CreatedBy]
		   ,[CreatedDate])
		   OUTPUT inserted.Id INTO @GuidTable
     VALUES
           (@Url
           ,@RequestHttpVerb
           ,@RequestBody
           ,@BpNo
           ,@UserType
           ,@CreatedBy
		   ,@CreatedDate)

SELECT Id FROM @GuidTable

END
GO
/****** Object:  StoredProcedure [dbo].[usp_update_logs]    Script Date: 2023-03-18 11:19:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_update_logs]		
	@Id uniqueidentifier,
	@ResponseBody [nvarchar](max) NULL,
	@ResponseStatusCode [int] NULL,
	@IsSuccess [bit] NULL,
	@UpdatedDate datetime
	
	
AS
BEGIN
	UPDATE [dbo].[Logs]
   SET 
	   ResponseBody = @ResponseBody
      ,ResponseStatusCode = @ResponseStatusCode
      ,IsSuccess = @IsSuccess     
      ,[UpdatedDate] = @UpdatedDate
 WHERE Id= @Id

END
GO
