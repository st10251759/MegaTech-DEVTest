-- ============================================
-- DEVTEST Database - Complete Schema
-- Matches EF Core Code First conventions
-- (identity primary keys, pluralized table names)
-- ============================================

CREATE DATABASE DEVTEST;
GO

USE DEVTEST;
GO

-- ============================================
-- TABLES
-- ============================================

CREATE TABLE dbo.Salespersons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Age INT NULL,
    Salary DECIMAL(18,2) NULL
);
GO

CREATE TABLE dbo.Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NULL,
    IndustryType NVARCHAR(5) NULL
);
GO

CREATE TABLE dbo.Orders (
    SalesOrder INT IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    CustId INT NOT NULL,
    SalesPersonId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustId) REFERENCES dbo.Customers(Id),
    CONSTRAINT FK_Orders_Salespersons FOREIGN KEY (SalesPersonId) REFERENCES dbo.Salespersons(Id)
);
GO

CREATE TABLE dbo.HighAchievers (
    Name NVARCHAR(50) NULL,
    Age INT NULL
);
GO

-- ============================================
-- INSERT DATA
-- (IDENTITY_INSERT used to keep the exact IDs
--  from the test paper, since Orders references
--  specific Customer/Salesperson IDs)
-- ============================================

SET IDENTITY_INSERT dbo.Salespersons ON;

INSERT INTO dbo.Salespersons (Id, Name, Age, Salary) VALUES
(1, 'Abe', 61, 140000.00),
(2, 'Bob', 34, 40000.00),
(5, 'Chris', 34, 40000.00),
(7, 'Dan', 41, 52000.00),
(8, 'Ken', 57, 115000.00),
(11, 'Joe', 38, 38000.00);

SET IDENTITY_INSERT dbo.Salespersons OFF;
GO

SET IDENTITY_INSERT dbo.Customers ON;

INSERT INTO dbo.Customers (Id, Name, City, IndustryType) VALUES
(4, 'Samsonic', 'Pleasant', 'J'),
(5, 'Bayside', 'AutumnWood', 'B'),
(6, 'Panasung', 'Oaktown', 'J'),
(7, 'Samory', 'Jackson', 'B'),
(9, 'Orange', 'Jackson', 'B');

SET IDENTITY_INSERT dbo.Customers OFF;
GO

SET IDENTITY_INSERT dbo.Orders ON;

INSERT INTO dbo.Orders (SalesOrder, OrderDate, CustId, SalesPersonId, Amount) VALUES
(10, '2018-08-22', 4, 2, 540.00),
(11, '2018-08-25', 4, 8, 1800.00),
(12, '2018-09-10', 9, 1, 460.00),
(13, '2019-09-15', 7, 2, 2400.00),
(14, '2019-09-23', 6, 7, 600.00),
(15, '2019-09-23', 6, 7, 720.00),
(16, '2019-09-29', 9, 7, 150.00);

SET IDENTITY_INSERT dbo.Orders OFF;
GO

-- HighAchievers is intentionally left empty here.
-- It gets populated later by the Q5 query:
--
-- INSERT INTO dbo.HighAchievers (Name, Age)
-- SELECT Name, Age FROM dbo.Salespersons WHERE Salary >= 100000;