USE [master]
GO

/****** Object:  Database [CheckQLGiaoVu]    Script Date: 7/4/2023 2:22:58 PM ******/
DROP DATABASE if EXISTS [CheckQLGiaoVu]
CREATE DATABASE [CheckQLGiaoVu]
 GO 
 Use [CheckQLGiaoVu]
 GO 
 DROP TABLE IF EXISTS [dbo].[Account]
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[IdRole] int  NOT NULL,
	[IdDepartment] int not null , 
	[Fullname] [nvarchar](50) NULL,--họ tên đầy đủ
	[Emailaddress] [nvarchar](50) NULL,--địa chỉ email
	[Phonenumber] [nvarchar](50) NULL,--SoDienThoai
	[Address] [nvarchar](50) NULL,--địa chỉ
	[Citizenidentification] [nvarchar](150) NULL,--CCCD
	[Dateofbirth] [date] NULL,--ngày sinh
	[Sex] [bit] NULL,--giới tính 
	[Status] [bit] NULL,--trạng thái
	[Role] NVARCHAR(50) ,--CHỨC VỤ HỌC SINH , HAY SINH VIÊN 
	--bên học sinh 
	[Class] [nvarchar](50) NULL, --khóa,lớp 
	[Schoolyear] [nvarchar](50) NULL,--năm học
	--bên giáo viên 
	[Degree] [nvarchar](50) NULL,--học vị 
	[Academicrank] [nvarchar](50) NULL,--học hàm
	
)
go

DROP TABLE IF EXISTS [dbo].[Role]
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) ,
	[Describe] NVARCHAR(50) --miêu tả 
)
go


/****** Object:  Table [dbo].[Teacher]    Script Date: 7/4/2023 2:22:59 PM ******/

/****** Object:  Table [dbo].[Requets]    Script Date: 7/4/2023 2:22:59 PM ******/
DROP TABLE IF EXISTS [dbo].[Requets]--mail
CREATE TABLE [dbo].[Requets](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[IdComplain] [int] NOT NULL, --là người gửi khiếu nại 
	[IdDepartment] [int] NOT NULL,	
	[IdHandle] [int]  NULL,--là người giải quyết 
	[Title] NVARCHAR(50) NULL, --tiêu đề 
	[Status] [smallint] NULL,  --trạng thái -- từ chối -1 , đang xem xets 0 ,1 đã nhận đc yêu cầu ,2 đã hoàn thành 	
	
	[Level] SMALLINT NOT NULL ,--mỨC ĐỘ NGHIÊM TRỌNG CỦA VỤ VIỆC 
	[Sentdate] date ,--ngày gửi
	[Enddate] date ,
	[Priority]	smallint ,-- độ ưu tiên	--1 là ưa tiên nhất ,2 ko ưa tiên lắm  
)
GO
DROP TABLE IF EXISTS [dbo].[Requetsdetailed]--lời yêu cầu chi tiết 
CREATE TABLE [dbo].[Requetsdetailed](
	Id int primary key identity ,
	[Sentdate] [date] NULL,--ngày gửi
	[Payday] [date] NULL,--ngày trả
	[Reason] [text] NULL,--lí do 
	[Status] [smallint] NULL, --trạng thái-- từ chối -1 , đang xem xets 0 ,1 đã nhận đc yêu cầu ,2 đã hoàn thành 
	[Reply] [text] NULL,--trả lời
	[IdRequest] int not null ,
)
GO 
DROP TABLE IF EXISTS  [dbo].[RequestFile]
CREATE TABLE  [dbo].[RequestFile]
(
	Id int primary key identity ,
	[Name] NVARCHAR(100) ,
	[IdRequest] int not null ,
)
GO 
/****** Object:  Table [dbo].[Department]    Script Date: 7/4/2023 2:22:59 PM ******/
DROP TABLE IF EXISTS [dbo].[Department]
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key ,
	[TenDepartment] [nvarchar](50) NULL,--phòng ban
	[Describe] [nvarchar](50) NULL,--mô tả
	[Address] [nvarchar](50) NULL,--địa chỉ
	[Status] [bit] NULL,--trạng thái
)
GO

--ĐỪng 
ALTER TABLE [dbo].[Requets]  
 ADD  CONSTRAINT [FK_Mail_Account_] FOREIGN KEY([IdComplain])
REFERENCES [dbo].[Account] ([Id])
GO
--nối người giải quyết 
ALTER TABLE [dbo].[Requets]  
 ADD  CONSTRAINT [FK_Mail_Account_1] FOREIGN KEY([IdHandle])
REFERENCES [dbo].[Account] ([Id])
GO


ALTER TABLE [dbo].[Requets] --lời yêu cầu --email 
 ADD  CONSTRAINT [FK_Mail_Department_] FOREIGN KEY([IdDepartment])
REFERENCES [dbo].[Department] ([Id])
GO





ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [FK_account_role_] 
 FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [FK_account_Department_] 
 FOREIGN KEY([IdDepartment])
REFERENCES [dbo].[Department]([Id])
GO


ALTER TABLE [dbo].[Requetsdetailed] --yêu cầu chi tiết
ADD  CONSTRAINT [FK_Detailedemail_Mail_] 
FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Requets] ([Id])
GO

ALTER TABLE [dbo].[RequestFile]
ADD  CONSTRAINT [FK_RequestFile_Mail_] 
FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Requets] ([Id])
GO



--
DROP TABLE IF EXISTS [dbo].[ManageRoleClaim]
CREATE TABLE [dbo].[ManageRoleClaim](
	[IdRoleClaim] [int] ,
	[IdAccount] [int] ,
	Primary key ([IdRoleClaim],[IdAccount])
) 
GO
DROP TABLE IF EXISTS [dbo].[RoleClaim]
CREATE TABLE [dbo].[RoleClaim](
	[Id] INT PRIMARY KEY IDENTITY ,
	[Name] NVARCHAR(50) ,
	[Describe] NVARCHAR(200),
	[Claim] int   , 

) 
GO
ALTER TABLE [dbo].[ManageRoleClaim]  --quanlyroleclaim
ADD  CONSTRAINT [FK_ManageRoleClaim_Account_] 
FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[ManageRoleClaim]  
ADD  CONSTRAINT [FK_ManageRoleClaim_RoleClaim_] 
FOREIGN KEY([IdRoleClaim])
REFERENCES [dbo].[RoleClaim] ([Id])
GO


SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Describe]) VALUES (1, N'Admin1', N'met moi')
INSERT [dbo].[Role] ([Id], [Name], [Describe]) VALUES (2, N'aDMIN2', N'MET MOI')
INSERT [dbo].[Role] ([Id], [Name], [Describe]) VALUES (3, N'ADMIN3 ', N'MET MOI ')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [TenDepartment], [Describe], [Address], [Status]) VALUES (1, N'Clb', N'Met', N'Chan', 1)
INSERT [dbo].[Department] ([Id], [TenDepartment], [Describe], [Address], [Status]) VALUES (2, N'NhaAn', N'CHan ', N'Met Moi', 1)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (1, N'Fadasdad1234@gmail.com', N'$2b$10$83Yu5e2fjrGPH95Pxf6/1uJ/xBSlabrySt6UDsXz13ci72GcQ.y.S', 1, 2, N'qwqeqweq', N'Fadasdad1234@gmail.com', N'0974752587', N'1231231313213', N'', CAST(N'2023-07-04' AS Date), 1, 1, N'student', N'fdsfsdfsf', N'2023-07-11', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (2, N'adasdad@gmail.com', N'$2b$10$bZdBGN1wRSSKycjZ0qnTG.ajmCKqxtfGfM0xZNfaUA.PQdnwiifAS', 2, 1, N'adb', N'adasdad@gmail.com', N'0975752587', N'1231231313213', N'account\NnoEuDZNQT.jpg', CAST(N'2023-07-12' AS Date), 0, 1, N'student', N'dfsfsfsfs', N'2023-07-26', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (3, N'adfasdad12@gmail.com', N'$2b$10$yvgMrtSOvh5iAX4KNNHf5OJpv.ylli8IFkE4e7z2lYgfTBv67cG16', 3, 1, N'312313', N'adfasdad12@gmail.com', N'0974752587', N'1231231313213', N'account\H1H9E1qUFR.png', CAST(N'2023-07-13' AS Date), 1, 1, N'teacher', NULL, NULL, N'dffd', N'fsfsf')
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (4, N'Fadasdad123456@gmail.com', N'$2b$10$lINsaUSa2ggY/6MLGYO6n.xXVuP6B2aXKm6XspgkCbirOw4fj0hg.', 3, 1, N'qwqeqweq', N'Fadasdad123456@gmail.com', N'0974752587', N'1231231313213', N'account\1arNOYnKNY.png', CAST(N'2023-07-12' AS Date), 0, 0, N'student', N'ewrqrwr', N'2023-07-04', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (5, N'Fafffdasdad1234@gmail.com', N'$2b$10$Rbv0EEVUWKoXTNRA.DrPWuwjOGjqc8xoANTn386OZBociBEKrdoSC', 2, 2, N'abcd', N'Fafffdasdad1234@gmail.com', N'0975752587', N'575 hung ', N'account\Ps736ucd7q.png', CAST(N'2023-06-29' AS Date), 1, 0, N'student', N'eeeeeeeeee', N'2023-07-18', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (1002, N'ggggdasdad@gmail.com', N'$2b$10$VsYd99UEdlHYmPT273LLpOD3ZyvbvAwl1c.2h2AiCNGvXd.oqFsj6', 2, 1, N'qwqeqweq', N'ggggdasdad@gmail.com', N'0974752587', N'1231231313213', N'account\iwmgK1CsHu.jpg', CAST(N'2023-07-06' AS Date), 1, 1, N'student', N'42342424', N'2023-07-06', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (1003, N'ttttadfasdad12@gmail.com', N'$2b$10$G698.IFlLe.c64m0HwWq1OOljYV0L/oYZhDUAmmzgu8jjRg9ZgvB6', 2, 1, N'qwqeqweq', N'ttttadfasdad12@gmail.com', N'0974752587', N'1231231313213', N'account\6ZtdBQBMsf.jpg', CAST(N'2023-07-13' AS Date), 0, 1, N'student', N'131231', N'2023-07-06', NULL, NULL)
INSERT [dbo].[Account] ([Id], [Username], [Password], [IdRole], [IdDepartment], [Fullname], [Emailaddress], [Phonenumber], [Address], [Citizenidentification], [Dateofbirth], [Sex], [Status], [Role], [Class], [Schoolyear], [Degree], [Academicrank]) VALUES (2002, N'adasdad1111@gmail.com', N'$2b$10$1i0HYQZdUk6/sOpxRCwAzOI4s0QOhDWXpxd2SFAdDNLBc0Bx492SC', 3, 1, N'qwqeqweq', N'adasdad1111@gmail.com', N'0975752587', N'1231231313213', NULL, CAST(N'2023-07-11' AS Date), 0, 1, N'student', N'ffffff', N'2023-07-16', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Requets] ON 

INSERT [dbo].[Requets] ([Id], [IdComplain], [IdDepartment], [IdHandle], [Title], [Status], [Level], [Sentdate], [Enddate], [Priority]) VALUES (1, 2, 1, 1, N'fdsfsdfs', 0, 0, CAST(N'2020-11-11' AS Date), CAST(N'2020-11-12' AS Date), 1)
INSERT [dbo].[Requets] ([Id], [IdComplain], [IdDepartment], [IdHandle], [Title], [Status], [Level], [Sentdate], [Enddate], [Priority]) VALUES (2, 2, 2, 1, N'ffffff', 0, 0, CAST(N'2020-11-11' AS Date), CAST(N'2020-11-12' AS Date), 3)
INSERT [dbo].[Requets] ([Id], [IdComplain], [IdDepartment], [IdHandle], [Title], [Status], [Level], [Sentdate], [Enddate], [Priority]) VALUES (6, 1, 2, 2, N'fffffaaaa', 0, 0, CAST(N'2023-11-12' AS Date), CAST(N'2021-11-11' AS Date), 4)
SET IDENTITY_INSERT [dbo].[Requets] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleClaim] ON 

INSERT [dbo].[RoleClaim] ([Id], [Name], [Describe], [Claim]) VALUES (1, N'sdfsdf', N'fdsfs', 1)
INSERT [dbo].[RoleClaim] ([Id], [Name], [Describe], [Claim]) VALUES (2, N'dddd2', N'dddds', 2)
INSERT [dbo].[RoleClaim] ([Id], [Name], [Describe], [Claim]) VALUES (3, N'dddd3', N'fffff', 3)
SET IDENTITY_INSERT [dbo].[RoleClaim] OFF
GO
INSERT [dbo].[ManageRoleClaim] ([IdRoleClaim], [IdAccount]) VALUES (1, 2)
INSERT [dbo].[ManageRoleClaim] ([IdRoleClaim], [IdAccount]) VALUES (1, 5)
INSERT [dbo].[ManageRoleClaim] ([IdRoleClaim], [IdAccount]) VALUES (1, 1003)
INSERT [dbo].[ManageRoleClaim] ([IdRoleClaim], [IdAccount]) VALUES (2, 5)
INSERT [dbo].[ManageRoleClaim] ([IdRoleClaim], [IdAccount]) VALUES (2, 1003)
GO


--Bãi rác 















--DROP TABLE IF EXISTS [dbo].[Alumnus]
--CREATE TABLE [dbo].[Alumnus](
--	[Id] [int] IDENTITY(1,1) NOT NULL primary key ,
--	[Fullname] [nvarchar](50) NULL,--họ tên đầy đủ
--	[Emailaddress] [nvarchar](50) NULL,--địa chỉ email
--	[Phonenumber] [nvarchar](50) NULL,--SoDienThoai
--	[Address] [nvarchar](50) NULL,--địa chỉ
--	[Citizenidentification] [nvarchar](50) NULL,--CCCD
--	[Dateofbirth] [date] NULL,--ngày sinh
--	[Sex] [bit] NULL,--giới tính 
--	[Class] [nvarchar](50) NULL, --khóa,lớp 
--	[Schoolyear] [nvarchar](50) NULL,--năm học
--	[Status] [bit] NULL,--trạng thái
--	[Parentphonenumber] [nvarchar](50) NULL,--sdt phụ huynh
--	[Emergencycontactphonenumber] [nvarchar](50) NULL,--sđt liên hệ khẩn cấp 
--	[Datecreated] [date] NULL,--ngày tạo
--	[Username] [nvarchar](50) NULL,
--)
--GO
--DROP TABLE IF EXISTS [dbo].[Teacher] 
--CREATE TABLE [dbo].[Teacher](
--	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
--	[Fullname] [nvarchar](50) NULL, 
--	[Degree] [nvarchar](50) NULL,--học vị 
--	[Academicrank] [nvarchar](50) NULL,--học hàm
--	[Sex] [nvarchar](50) NULL,--giới tính
--	[Dateofbirth] [smalldatetime] NULL,--ngày sinh
--	--[NgayVL] [smalldatetime] NULL,
--	[Coefficient] [numeric](4, 2) NULL,--hệ số 
--	[Wage] [money] NULL,--mức lương
--	[Username] [nvarchar](50) NULL,
--)
--GO

-- DROP TABLE IF EXISTS [dbo].[Person]
--CREATE TABLE [dbo].[Person](
--	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	[Username] [nvarchar](50) NOT NULL,
--	--[Password] [nvarchar](50) NOT NULL,
--	--[Role] [nvarchar](50) NOT NULL,
--	--[ChucVu] smallint not null , --chức vụ  
--)
--go
/****** Object:  Table [dbo].[Claim]    Script Date: 7/4/2023 2:22:59 PM ******/

/****** Object:  Table [dbo].[infrastructure]    Script Date: 7/4/2023 2:22:59 PM ******/
--DROP TABLE IF EXISTS [infrastructure] 
--CREATE TABLE [dbo].[infrastructure](
--	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
--	[IdCSVc] [nvarchar](50) NOT NULL,
--	[NameCSVc] [nvarchar](50) NULL,
--	[StatusCSVc] [nvarchar](50) NULL,--trạng thái
--	[ReceiveddateCSVc] [date] NULL,--ngày nhận
--	[TypeCSVc] [nvarchar](50) NULL,--loại
--	[Amountreceived] [int] NULL,--số lượng nhận
--	[QuantitygPresent] [int] NULL,--số lượng 
--	[Unit] [nvarchar](50) NULL,--đơn vị
--	[Value] [money] NULL,--giá trị
--	[ExpiryCSVc] [date] NULL,--hạn sử dụng 
--	[Payments] [nvarchar](50) NULL,--hình thức thanh toán
--	[storageroom] [nvarchar](50) NULL,--phòng lưu trữ
--	[Funds] [nvarchar](50) NULL,--NguonTien
--	[Traders] [nvarchar](50) NULL,--NguoiGiaoDich
--	[IdBan] [int] NULL,
--	[Describe] [nvarchar](100) NULL,--mô tả
--)
--GO


/****** Object:  Table [dbo].[Facilitiesmanagement]    Script Date: 7/4/2023 2:22:59 PM ******/
--DROP TABLE IF EXISTS Facilitiesmanagement--quản lí cơ sở vật chất 
--CREATE TABLE [dbo].[Facilitiesmanagement](
--	[IdCsvc] [int] NULL,
--	[Id] [int] NULL
--) ON [PRIMARY]
--GO
/****** Object:  Table [dbo].[ManageClaim]    Script Date: 7/4/2023 2:22:59 PM ******/


/****** Object:  Table [dbo].[ManageLoaiPhieu]    Script Date: 7/4/2023 2:22:59 PM ******/
--DROP TABLE IF EXISTS [dbo].[ManageLoaiPhieu]
--CREATE TABLE [dbo].[ManageLoaiPhieu](
--	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY  ,
--	[Name] [nvarchar](50) NULL,
--	[MoTa] [nvarchar](200) NULL,
--)
--GO
/****** Object:  Table [dbo].[ManageMail]    Script Date: 7/4/2023 2:22:59 PM ******/
--DROP TABLE IF EXISTS [dbo].[ManageMail]
--CREATE TABLE [dbo].[ManageMail](
--	[MaNV] [int] NOT NULL,
--	[IdRequest] [int] NOT NULL,
--		PRIMARY KEY ([MaNV],[IdRequest]) 
--		)
--GO
/****** Object:  Table [dbo].[Alumnus]    Script Date: 7/4/2023 2:22:59 PM ******/

--ALTER TABLE [dbo].[Requets]  
--ADD  CONSTRAINT [FK_Mail_ManageLoaiPhieu_] FOREIGN KEY([MaLoaiPhieu])
--REFERENCES [dbo].[ManageLoaiPhieu] ([Id])
--GO

--ALTER TABLE [dbo].[Facilitiesmanagement] --quản lí cơ sở vật chất 
-- ADD  CONSTRAINT [FK_Facilitiesmanagement_infrastructure_] 
-- FOREIGN KEY([IdCsvc])
--REFERENCES [dbo].[infrastructure] ([Id])
--GO


--

--ALTER TABLE [dbo].[ManageMail] 
-- ADD  CONSTRAINT [FK_ManageMail_Account_] 
-- FOREIGN KEY([MaNV])
--REFERENCES [dbo].[Account] ([Id])
--GO

--ALTER TABLE [dbo].[ManageMail]  
-- ADD  CONSTRAINT [FK_ManageMail_Mail_] 
-- FOREIGN KEY([IdRequest])
--REFERENCES [dbo].[Requets] ([Id])
--GO
--- mỡi 


--cái này mới nhất 


--Tạo kết nối 1-1 account với person 
--ALTER TABLE [dbo].[Account]
--ADD  CONSTRAINT UQ_account_Username UNIQUE (Username)
--GO 

--ALTER TABLE [dbo].[Person]
--ADD  CONSTRAINT UQ_Person_Username UNIQUE(Username)
--GO
--ALTER TABLE [dbo].[Person]
--ADD CONSTRAINT FK_HOIMET_DDD
--FOREIGN KEY ([Username])
--REFERENCES [dbo].[Account]([Username])
--GO 
--MỚI NHẤT 


--ALTER TABLE [dbo].[Account]
--ADD  CONSTRAINT UQ_PersonID UNIQUE (Username)
--GO 

--ALTER TABLE [dbo].[Alumnus]
--ADD  CONSTRAINT UQ_Person_Person UNIQUE(Username)
--go 

--ALTER TABLE [dbo].[Teacher] 
--ADD  CONSTRAINT UQ_Person_Person123  UNIQUE(Username)
--go 


--ALTER TABLE [dbo].[Alumnus]
--ADD CONSTRAINT FK_HOIMET_DDD
--FOREIGN KEY ([Username])
--REFERENCES [dbo].[Account]([Username])
--		GO 
