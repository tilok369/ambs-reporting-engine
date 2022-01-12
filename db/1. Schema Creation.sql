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