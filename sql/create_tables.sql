use GCAV
CREATE TABLE [dbo].[driver](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	name varchar(255) not null,
	nationality varchar(255) not null,
	age int not null,
	active bit not null
)