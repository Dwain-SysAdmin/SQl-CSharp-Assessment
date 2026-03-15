Question 1

Date Source:
- It is the connection to the Database

DataSet:
- It is the query set to collect data from the Database
- It can retreive data in Text, Table or Stored Procedure

Data Region:
- it is visual elements to display data

-----------------------------------------------------------------------------------------

Question 2

BEGIN

DECLARE 
@StartDate DateTime,
@EndDate DateTime

SET @StartDate = '2025-01-01'
SET @EndDate = '2025-12-31'


	SELECT
		c.FirstName + ' ' + c.LastName [Customer Name],
		o.OrderId,
		o.TotalAmount
	FROM Orders o
	INNER JOIN Customers c ON o.CustomerId = c.CustomerId
	WHERE o.OrderDate >= @StartDate
	AND o.OrderDate < DATEADD(DAY, 1, @EndDate)

END

-----------------------------------------------------------------------------------------

Question 3

I would use Row Groups on Customer > Orders > Order Items

Customer
  >  Orders
     >   Order Items

-----------------------------------------------------------------------------------------

Question 4

IIF(IsNothing(Fields!PaymentMethod.Value), "No Payment", Fields!PaymentMethod.Value)

-----------------------------------------------------------------------------------------

Question 5

- Filter all data in SQL instead of SSRS
- use Indexed queries
- avoid large datasets

-----------------------------------------------------------------------------------------

Question 6

- Splitting Overview and Details resuls
- Modular reports

