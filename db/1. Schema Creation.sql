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
	[Status] bit not null default(1),
	[Type] int not null,
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (DashboardId) REFERENCES [config].[Dashboard](Id)
)
GO

Create table [config].[TabularFeature]
(
	Id bigint not null primary key identity(1, 1),
	ReportId bigint not null,
	Script nvarchar(max) not null,
	Title nvarchar(100) null,
	SubTitle nvarchar(100) null,
	ShowFilterInfo bit not null default(1),
	Template nvarchar(200) null,
	AsOnDate bit not null default(0),
	Exportable bit not null default(1),
	HasTotalColumn bit not null default(0),
	HasTotalRow bit not null default(0),
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (ReportId) REFERENCES [config].[Report](Id)
)
Go


Create table [config].[GraphicalFeature]
(
	Id bigint not null primary key identity(1, 1),
	ReportId bigint not null,
	GraphType int not null,
	Script nvarchar(max) not null,
	Title nvarchar(100) null,
	SubTitle nvarchar(100) null,
	ShowFilterInfo bit not null default(1),
	ShowLegend bit not null default(1),
	XAxisTitle nvarchar(100) null,
	YAxisTitle nvarchar(100) null,
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
	FOREIGN KEY (ReportId) REFERENCES [config].[Report](Id)
)
Go

Create table [config].[Filter]
(
	Id bigint not null primary key identity(1, 1),
	[Name] nvarchar(50) not null,
	[Label] nvarchar(50) not null,
	Script nvarchar(max) not null,
	[Parameter] nvarchar(50) not null,
	DependentParameters nvarchar(500) not null,
	[Status] int not null default(1),
	CreatedOn datetime not null,
	CreatedBy nvarchar(20) not null,
	UpdatedOn datetime null,
	UpdatedBy nvarchar(20) null,
)
Go

Create table [config].[ReportFilter]
(
	Id bigint not null primary key identity(1, 1),
	ReportId bigint not null,
	FilterId bigint not null,
	SortOrder int not null,
	FOREIGN KEY (ReportId) REFERENCES [config].[Report](Id),
	FOREIGN KEY (FilterId) REFERENCES [config].[Filter](Id)
)

Go

Create table [config].[GraphType]
(
	Id int not null primary key identity(1, 1),
	[Name] nvarchar(50) not null,
	[Status] bit not null default(1),
	SortOrder int not null
)
Go

insert into [config].[GraphType] values ('Pie', 1, 1)
insert into [config].[GraphType] values ('Column', 1, 2)
insert into [config].[GraphType] values ('Bar', 1, 3)
insert into [config].[GraphType] values ('Stacked Column', 1, 4)
insert into [config].[GraphType] values ('Stacked Bar', 1, 5)