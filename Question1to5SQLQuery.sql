USE DEVTEST;
GO

-- 1. Sales people with an order for Orange and Samory
SELECT DISTINCT sp.Name
FROM dbo.Salespersons sp
INNER JOIN dbo.Orders o ON o.SalesPersonId = sp.Id
INNER JOIN dbo.Customers c ON c.Id = o.CustId
WHERE c.Name IN ('Orange', 'Samory');

-- 2. Customers with orders amount > 700 and < 2000
SELECT DISTINCT c.Name
FROM dbo.Customers c
INNER JOIN dbo.Orders o ON o.CustId = c.Id
WHERE o.Amount > 700 AND o.Amount < 2000;

-- 3. Customers with no orders
SELECT c.Name
FROM dbo.Customers c
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Orders o WHERE o.CustId = c.Id
);

-- 4. Salespeople with 2 or more orders
SELECT sp.Name
FROM dbo.Salespersons sp
INNER JOIN dbo.Orders o ON o.SalesPersonId = sp.Id
GROUP BY sp.Name
HAVING COUNT(o.SalesOrder) >= 2;

-- 5. Insert high achievers (salary >= 100,000)
INSERT INTO dbo.HighAchievers (Name, Age)
SELECT Name, Age
FROM dbo.Salespersons
WHERE Salary >= 100000;