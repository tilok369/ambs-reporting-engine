create schema config
Go
Create table [config].[Dashboard]
(
	Id bigint not null primary key identity(1, 1),
	[Name] nvarchar(50) not null,
	[Status] bit not null,
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null
)

Go

Create table [config].[Widget]
(
	Id bigint not null primary key identity(1, 1),
	DashboardId bigint not null,
	[Name] nvarchar(50) not null,
	[Status] bit not null,
	[Type] int not null,
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (DashboardId) REFERENCES [config].[Dashboard](Id)
)
GO

Create table [config].[Report]
(
	Id bigint not null primary key identity(1, 1),
	DashboardId bigint not null,
	[Name] nvarchar(50) not null,
	[Status] bit not null,
	[Type] int not null,
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (DashboardId) REFERENCES [config].[Dashboard](Id)
)
GO

Create table [config].[ReportFeature]
(
	Id bigint not null primary key identity(1, 1),
	ReportId bigint not null,
	Script nvarchar(max) not null,

	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (ReportId) REFERENCES [config].[Report](Id)
)