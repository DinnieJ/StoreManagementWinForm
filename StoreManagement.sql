USE [master]
GO

CREATE DATABASE StoreManagement
GO

USE StoreManagement;
GO
--StoreManagement.User table-----------------
CREATE TABLE [User] (
	ID INT IDENTITY(1000000, 3) PRIMARY KEY,
	[Username] NVARCHAR(255) NOT NULL UNIQUE,
	[Password] NVARCHAR(255) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	PhoneNumber VARCHAR(50) NOT NULL,
	DateOfBirth DATETIME NOT NULL,
	[Role] VARCHAR(10) NOT NULL DEFAULT 'STAFF',
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

--StoreManagement.Product table------------
CREATE TABLE Product(
	ID INT IDENTITY(10000000,1) PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	Price BIGINT NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);
GO

--StoreManagement.Order table------------
CREATE TABLE [Order] (
	ID INT IDENTITY(100000000,4) PRIMARY KEY,
	StaffID INT NOT NULL FOREIGN KEY REFERENCES [User](ID),
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)
GO

--StoreManagement.OrderProduct table------------
CREATE TABLE OrderProduct (
	ID  INT IDENTITY(100000000, 2) PRIMARY KEY,
	OrderID INT NOT NULL FOREIGN KEY REFERENCES [Order](ID),
	ProductID INT NOT NULL FOREIGN KEY REFERENCES Product(ID),
	Sale INT NOT NULL DEFAULT 0,
	Quantity INT NOT NULL DEFAULT 1,
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
)
GO
--Default admin account
INSERT INTO [User]([Name],[Username],[Password],PhoneNumber,DateOfBirth,[Role]) VALUES
('admin4','admin4', '123456', '0099993299', '1993-12-01', 'ADMIN')
GO

--Trigger on modified item

CREATE TRIGGER trg_Product_UpdateModifiedDate
ON Product
AFTER UPDATE
AS
UPDATE Product
SET UpdatedAt = CURRENT_TIMESTAMP
WHERE Product.ID IN (SELECT DISTINCT ID FROM inserted);
GO


CREATE TRIGGER trg_Order_UpdateModifiedDate
ON [Order]
AFTER UPDATE
AS
UPDATE [Order]
SET UpdatedAt = CURRENT_TIMESTAMP
WHERE [Order].ID IN (SELECT DISTINCT ID FROM inserted);
GO


CREATE TRIGGER trg_OrderProduct_UpdateModifiedDate
ON OrderProduct
AFTER UPDATE
AS
UPDATE OrderProduct
SET UpdatedAt = CURRENT_TIMESTAMP
WHERE OrderProduct.ID IN (SELECT DISTINCT ID FROM inserted);
GO


CREATE TRIGGER trg_User_UpdateModifiedDate
ON [User]
AFTER UPDATE
AS
UPDATE [User]
SET UpdatedAt = CURRENT_TIMESTAMP
WHERE [User].ID IN (SELECT DISTINCT ID FROM inserted);
GO