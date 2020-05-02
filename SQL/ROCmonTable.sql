IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Agent') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Agent') AND type='U')
DROP TABLE dbo.Agent
CREATE TABLE Agent ( 
AgentId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
AgentName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Agent PRIMARY KEY CLUSTERED (
AgentId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Borrower') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Borrower') AND type='U')
DROP TABLE dbo.Borrower
CREATE TABLE Borrower ( 
BorrowerId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
BorrowerName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Borrower PRIMARY KEY CLUSTERED (
BorrowerId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Broker') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Broker') AND type='U')
DROP TABLE dbo.Broker
CREATE TABLE Broker ( 
BrokerId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
BrokerName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Broker PRIMARY KEY CLUSTERED (
BrokerId
)
)
END

GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.City') AND type='U')
DROP TABLE dbo.City
CREATE TABLE City ( 
CityId int IDENTITY(1,1) NOT NULL ,
CityName nvarchar (50) NOT NULL ,
CountryId smallint NOT NULL ,
StateId smallint NOT NULL ,
CONSTRAINT PK_City PRIMARY KEY CLUSTERED (
CityId
)
)

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Company') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Company') AND type='U')
DROP TABLE dbo.Company
CREATE TABLE Company ( 
CompanyId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
CompanyDesc nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Company PRIMARY KEY CLUSTERED (
CompanyId
)
)
END

GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Country') AND type='U')
DROP TABLE dbo.Country
CREATE TABLE Country ( 
CountryId smallint IDENTITY(1,1) NOT NULL ,
CountryName nvarchar (50) NOT NULL ,
CountryCd char (3) NOT NULL ,
CONSTRAINT PK_Country PRIMARY KEY CLUSTERED (
CountryId
)
)

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Customer') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Customer') AND type='U')
DROP TABLE dbo.Customer
CREATE TABLE Customer ( 
CustomerId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
CustomerName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (
CustomerId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Firm') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Firm') AND type='U')
DROP TABLE dbo.Firm
CREATE TABLE Firm ( 
FirmId int IDENTITY(1,1) NOT NULL ,
TradeName nvarchar (100) NOT NULL ,
LegalName nvarchar (100) NOT NULL ,
Address1 nvarchar (100) NULL ,
Address2 nvarchar (100) NULL ,
CountryId smallint NULL ,
StateId smallint NULL ,
CityId int NULL ,
PostalZip nvarchar (20) NULL ,
WebUrl nvarchar (100) NULL ,
LinkedIn nvarchar (100) NULL ,
Twitter nvarchar (100) NULL ,
Contact nvarchar (100) NULL ,
TollFree varchar (20) NULL ,
Phone varchar (20) NULL ,
Mobile varchar (20) NULL ,
Fax varchar (20) NULL ,
Active char (1) NOT NULL CONSTRAINT DF_Firm_Active DEFAULT ('Y'),
CONSTRAINT PK_Firm PRIMARY KEY CLUSTERED (
FirmId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.FxRate') and type='U')
BEGIN
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_FxRate')
DROP INDEX FxRate.IX_FxRate 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.FxRate') AND type='U')
DROP TABLE dbo.FxRate
CREATE TABLE FxRate ( 
FxRateId int IDENTITY(1,1) NOT NULL ,
FrCurrency char (3) NOT NULL ,
ToCurrency char (3) NOT NULL ,
ToFxRate decimal (12,4) NOT NULL ,
ValidFr datetime NOT NULL ,
ValidTo datetime NOT NULL ,
CONSTRAINT PK_FxRate PRIMARY KEY CLUSTERED (
FxRateId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Guarantor') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Guarantor') AND type='U')
DROP TABLE dbo.Guarantor
CREATE TABLE Guarantor ( 
GuarantorId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
GuarantorName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Guarantor PRIMARY KEY CLUSTERED (
GuarantorId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Investor') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Investor') AND type='U')
DROP TABLE dbo.Investor
CREATE TABLE Investor ( 
InvestorId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
InvestorName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Investor PRIMARY KEY CLUSTERED (
InvestorId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Lender') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Lender') AND type='U')
DROP TABLE dbo.Lender
CREATE TABLE Lender ( 
LenderId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
LenderName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Lender PRIMARY KEY CLUSTERED (
LenderId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Member') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Member') AND type='U')
DROP TABLE dbo.Member
CREATE TABLE Member ( 
MemberId int IDENTITY(1,1) NOT NULL ,
ParentId int NULL ,
MemberName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Member PRIMARY KEY CLUSTERED (
MemberId
)
)
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Project') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Project') AND type='U')
DROP TABLE dbo.Project
CREATE TABLE Project ( 
ProjectId int IDENTITY(1,1) NOT NULL ,
ParentId int NULL ,
ProjectDesc nvarchar (100) NOT NULL ,
ProjectLink nvarchar (100) NULL ,
ProjectImg varchar (100) NULL ,
CompanyId int NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Project PRIMARY KEY CLUSTERED (
ProjectId
)
)
END

GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.State') AND type='U')
DROP TABLE dbo.State
CREATE TABLE State ( 
StateId smallint IDENTITY(1,1) NOT NULL ,
StateName nvarchar (50) NOT NULL ,
CountryId smallint NOT NULL ,
StateCode varchar (3) NOT NULL ,
CONSTRAINT PK_State PRIMARY KEY CLUSTERED (
StateId
)
)

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendor') and type='U')
BEGIN
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Vendor') AND type='U')
DROP TABLE dbo.Vendor
CREATE TABLE Vendor ( 
VendorId int IDENTITY(1,1) NOT NULL ,
FirmId int NOT NULL ,
ParentId int NULL ,
VendorName nvarchar (100) NOT NULL ,
Active char (1) NOT NULL ,
CONSTRAINT PK_Vendor PRIMARY KEY CLUSTERED (
VendorId
)
)
END

GO