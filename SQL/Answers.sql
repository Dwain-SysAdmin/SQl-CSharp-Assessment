BEGIN
-----------------------------------------------------------------------------------

-- Question 1
SELECT *
FROM Products
WHERE Active = 1
ORDER BY Price DESC

-----------------------------------------------------------------------------------

-- Question 2
SELECT *
FROM Orders
WHERE OrderDate >= DATEADD(DAY,-30,GETDATE())

-----------------------------------------------------------------------------------

-- Question 3
SELECT 
	o.OrderId,
	c.FirstName,
	c.LastName,
	o.OrderDate,
	o.TotalAmount
FROM Orders o
INNER JOIN Customers c ON o.CustomerId = c.CustomerId

-----------------------------------------------------------------------------------

-- Question 4
SELECT 
	YEAR(OrderDate) [Year],
	MONTH(OrderDate) [Month],
	SUM(TotalAmount) [Total Reveneu]
FROM Orders
GROUP BY YEAR(OrderDate), MONTH(OrderDate)
ORDER BY YEAR, MONTH

-----------------------------------------------------------------------------------

-- Question 5
SELECT TOP 5
	c.CustomerId,
	c.FirstName + ' ' + c.LastName [Customer Name],
	SUM(o.TotalAmount) [Total Spend]
FROM Customers c
INNER JOIN Orders o ON o.CustomerId = c.CustomerId
GROUP BY c.CustomerId, c.FirstName, c.LastName
ORDER BY [Total Spend] DESC

-----------------------------------------------------------------------------------

-- Question 6
SELECT 
	o.OrderId,
	o.TotalAmount,
	SUM(oi.Quantity * oi.UnitPrice) [Total]
FROM Orders o
INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId
GROUP BY o.OrderId, o.TotalAmount
HAVING o.TotalAmount != SUM(oi.Quantity * oi.UnitPrice)

-- This Check whether the TotalAmount is equal to the sum of the order items.

-----------------------------------------------------------------------------------

-- Question 7
SELECT 
	CustomerId,
	OrderId,
	OrderDate,
	ROW_NUMBER() OVER (
						PARTITION BY CustomerId
						ORDER BY OrderDate
					) [Row Number]
FROM Orders

-----------------------------------------------------------------------------------

-- Question 8

-- BAD QUERY
SELECT *
FROM Orders
WHERE YEAR(OrderDate) = 2025

-- BETTER QUERY
SELECT *
FROM Orders
WHERE OrderDate >= '2025-01-01'
AND OrderDate < '2026-01-01'


-- Using specific dates narrows dows the search

-----------------------------------------------------------------------------------

-- Question 9
CREATE INDEX Customer_Report
ON Orders (CustomerId, OrderDate)

-----------------------------------------------------------------------------------

-- Qustion 10
UPDATE Products
SET Active = 0
WHERE ProductId NOT IN ( SELECT DISTINCT 
							ProductId
						 FROM OrderItems
						)

SELECT *
FROM Products
WHere Active = 0

-----------------------------------------------------------------------------------

-- Question 11
/*
	Problems:
	- Date Duplication
	- Update Anomolies
	- Inconsistent Product Type
	- Customer Information repeats

	Normalized:
	Customers
	Products
	Orders
	OrderItems

	Relationships:
	Customers 1 > many Order
	Orders 1 > many OrderItems
	Products 1 > many OrderItems
*/

-----------------------------------------------------------------------------------

-- Question 12
/*
	CTE:
	- Temp names Result set
	- Inproves Reliability
	- used for recursive queries or procs

	Temp Table:
	- Stored in tempDB
	- reusable
	- uses space in the tempDB
	- Used for large queries

	Subquery:
	- nested queries inside select and where
	- used for simple quick queries
*/

END