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
