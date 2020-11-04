CREATE TABLE Customer(
	SSNumber int not null primary key,
	[Name] nvarchar(50) not null,
	PhoneNumber int not null,
	Email nvarchar(50) not null
)
GO

CREATE TABLE SupportCases(	
	CaseNumber int identity(1,1) primary key,
	CustomerNumber int not null references Customer(SSNumber),
	[Status] nvarchar(50) not null,
	[Description] nvarchar(500) not null,	
	Title nvarchar(50) not null,
	Category nvarchar(50) not null
	[Time] DateTime  
)