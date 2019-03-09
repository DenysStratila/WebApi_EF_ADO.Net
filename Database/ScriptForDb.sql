--Task_005
--Каталог содержит описание товаров, отсортированных по категориям, от различных поставщиков.
--Создать базу данных с таблицами Категории_Товаров, Поставщики и Товары.
CREATE DATABASE SupermarketDb
GO

USE SupermarketDb
GO

-- Creating Tables.
CREATE TABLE CategoriesProducts
(
Id int IDENTITY(1,1),
CategoryName nvarchar(50),
CONSTRAINT PK_CategoriesProducts_Id PRIMARY KEY (Id)
);
GO

CREATE TABLE Suppliers
(
Id int IDENTITY(1,1),
CompanyName nvarchar(50),
CONSTRAINT PK_Suppliers_Id PRIMARY KEY (Id)
);
GO

CREATE TABLE Products
(
Id int IDENTITY(1,1),
ProductName nvarchar(50),
CategoryId int NOT NULL,
SupplierId int NOT NULL,
CONSTRAINT PK_Products_Id PRIMARY KEY (Id),
CONSTRAINT FK_Categories_Products_Id FOREIGN KEY (CategoryId) REFERENCES CategoriesProducts(Id),
CONSTRAINT FK_Suppliers_Id FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);
GO

-- Inserting data.
INSERT INTO CategoriesProducts
(CategoryName)
VALUES
('Computers'),
('Smartphones'),
('Fridges')
GO

INSERT INTO Suppliers
(CompanyName)
VALUES
('Apple'),
('LG'),
('Dell'),
('Bosch'),
('Asus'),
('Indesit')
GO

INSERT INTO Products
(ProductName, CategoryId, SupplierId)
VALUES
('DS3181S',							 3,		6),
('Inspiron 3573 ',					 1,		3),
('X570UD-E4037 ',					 1,		5),
('LI7 S1 W',						 3,		6),
('VivoBook 15 X510UA-BQ438',		 1,		5),
('A1466 MacBook Air 13" ',			 1,		1),
('iPhone 7 Plus 32GB Jet Black ',	 2,		1),
('RAA 24 N',						 3,		6),
('iPhone 6s 32GB Rose Gold (MN122)', 2,		1),
('G7 ThinQ 4/64GB Moroccan Blue',	 2,		2),
('Inspiron 3567 ',					 1,		3),
('KGN39VI35',						 3,		4),
('Inspiron 15 3567 ',				 1,		3),
('GA-B429SMQZ',						 3,		2),
('KGN39UW306',						 3,		4)
GO