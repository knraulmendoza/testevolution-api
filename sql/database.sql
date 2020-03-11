CREATE TABLE [users] (
[id] int NOT NULL IDENTITY(1,1),
[username] varchar(20) NOT NULL,
[password] varchar(20) NOT NULL,
[state] smallint NOT NULL,
PRIMARY KEY ([id]) ,
CONSTRAINT [un_user_username] UNIQUE ([username] ASC)
)
GO
CREATE TABLE [roles] (
[id] int NOT NULL IDENTITY(1,1),
[name] varchar(30) NOT NULL,
[description] varchar(300) NULL,
[state] smallint NOT NULL,
PRIMARY KEY ([id]) ,
CONSTRAINT [un_role_name] UNIQUE ([name] ASC)
)
GO
CREATE TABLE [users_roles] (
[user_id] int NOT NULL,
[role_id] int NOT NULL,
PRIMARY KEY ([user_id], [role_id]) 
)
GO
CREATE TABLE [persons] (
[id] int NOT NULL IDENTITY(1,1),
[first_name] varchar(40) NOT NULL,
[second_name] varchar(40) NULL,
[first_last_name] varchar(40) NOT NULL,
[second_last_name] varchar(40) NOT NULL,
[state] smallint NOT NULL,
[user_id] int NOT NULL,
PRIMARY KEY ([id]) 
)
GO

ALTER TABLE [users_roles] ADD CONSTRAINT [fk_users_roles_users_1] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [users_roles] ADD CONSTRAINT [fk_users_roles_roles_1] FOREIGN KEY ([role_id]) REFERENCES [roles] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [persons] ADD CONSTRAINT [fk_persons_users_1] FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

