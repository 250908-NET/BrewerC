-- Parking Lot*******
-- *                *
-- *                *
--- *****************

-- SETUP:
    -- Create a database server (docker)
        -- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    -- Connect to the server (Azure Data Studio / Database extension)
    -- Test your connection with a simple query (like a select)
    -- Execute the Chinook database (to create Chinook resources in your db)



-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
-- Shouldnt 'Name' be "Name"? Double qoute for column names? Weird :)
-- apparently it works with single quotes and i have no idea
-- different DBs have slightly different syntax/rules. Apparently '' or "" works.
-- In postgres, to have a capital letter in column name, you have to use "". -- Name, 'Name', "Name" all work here on SQL Serve
select concat(LastName, ', ', FirstName) as "Name", CustomerId, Country 
    from Customer
    where Country <> 'USA';

-- =, >, <, !, !=

use Chinook;
select "FirstName", "LastName", "CustomerId", "Country" from "Customer" where "Country" <> 'USA';
select concat(LastName, ', ', FirstName) as Name, CustomerId, Country 
    from Customer
    where Country <> 'USA';

-- List all customers from Brazil
select * from customer where country='Brazil';

use Chinook;
select * from "Customer" where "Country" = 'Brazil';

-- List all sales agents
select * from employee where title = 'Sales Support Agent';

use Chinook;
select * from "Employee" where "Title" = 'Sales Support Agent';

-- Retrieve a list of all countries in billing addresses on invoices
select distinct country from customer;

use Chinook;
select "BillingCountry" from "Invoice";

-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?
select year(InvoiceDate) as "Year", count(*) as "Invoices", sum(Total) as "Sales" 
from Invoice
where year(InvoiceDate) = 2009;

use Chinook;
select count(*) from "Invoice" where YEAR("InvoiceDate") = 2009;
select sum("Total") from "Invoice" where YEAR("InvoiceDate") = 2009;
select sum("Total") from "Invoice" group by YEAR("InvoiceDate");

    -- (challenge: find the invoice count sales total for every year using one query)

-- how many line items were there for invoice #37
-- SELECT COUNT(*) AS InvoiceCount, SUM(Total) AS SalesTotal FROM Invoice WHERE YEAR(Invo)i
select count(*) 
from InvoiceLine
where InvoiceId = 37;

use Chinook;
select count(*) from "InvoiceLine" where "InvoiceId" = 37;

-- how many invoices per country? BillingCountry  # of invoices -
SELECT BillingCountry, COUNT(InvoiceId) as InvoiceCount
FROM dbo.Invoice
GROUP BY BillingCountry;

use Chinook;
select "BillingCountry", count(*) from "Invoice" group by "BillingCountry";

-- Retrieve the total sales per country, ordered by the highest total sales first.
use Chinook;
select "BillingCountry", sum("Total") as sum_total from "Invoice" group by "BillingCountry" order by sum_total desc;


-- JOINS CHALLENGES
-- Every Album by Artist
SELECT Artist.Name, Album.Title FROM Artist JOIN Album ON Artist.ArtistId = Album.ArtistId;

use Chinook;
select t1.Name, t2.Title from
	Artist t1
join
	Album t2
on t1.ArtistId = t2.ArtistId;
	
-- All songs of the rock genre
select Track.Name as "Track", Genre.Name as "Genre"
from Track left join Genre on Track.GenreId = Genre.GenreId
where Genre.Name = 'Rock';

select * from
	Track t1
join
	Genre t2
on t1.GenreId = t2.GenreId where t2.Name = 'Rock';

-- Show all invoices of customers from brazil (mailing address not billing)
SELECT I.*, C.Firstname, C.LastName
From Invoice I
Inner Join Customer C 
        On I.CustomerId = C.CustomerId
Where C."Country" = 'Brazil'
ORDER BY I.InvoiceDate;

---- Show all invoices together with the name of the sales agent for each one
SELECT (Employee.FirstName + ' ' + Employee.LastName) AS SalesAgent, Invoice.*
FROM Invoice
JOIN Customer
ON Invoice.CustomerId = Customer.CustomerId
JOIN Employee
ON Employee.EmployeeId = Customer.SupportRepId;

select * from
	Invoice t1
join
	Customer t2
on t1.CustomerId = t2.CustomerId where t2.Country = 'Brazil';

-- Show all invoices together with the name of the sales agent for each one

select * from
	Invoice t1
join
	Customer t2
on t1.CustomerId = t2.CustomerId where t2.Country = 'Brazil';

-- Which sales agent made the most sales in 2009?
Select TOP 1 Employee.FirstName as 'Sales Agent', SUM(Invoice.Total) 
FROM Invoice 
INNER JOIN Customer 
    ON Invoice.CustomerId = Customer.CustomerId 
INNER JOIN Employee 
    ON Customer.SupportRepId = Employee.EmployeeId 
WHERE Employee.Title LIKE '%Sales%' AND Year(Invoice.InvoiceDate) = 2009 
GROUP BY Employee.FirstName ORDER BY SUM(Invoice.Total) DESC;

WITH MostSales AS (
    SELECT e.FirstName, e.LastName, "Total Sales"
    FROM Employee AS e JOIN (
        SELECT Employee.EmployeeId, SUM(Invoice.Total) AS "Total Sales"
        FROM Invoice
        JOIN Customer ON Invoice.CustomerId = Customer.CustomerId
        JOIN Employee ON Customer.SupportRepId = Employee.EmployeeId
        WHERE YEAR(Invoice.InvoiceDate) = 2009
        GROUP BY Employee.EmployeeId
    ) AS sales
    ON e."EmployeeId" = sales."EmployeeId"
)
SELECT *
FROM MostSales
WHERE "Total Sales" = (SELECT Max("Total Sales") FROM MostSales);

-- UDF - user defined function
-- Stored Procedure


select * from
	Employee t1
join
	Customer t2
on t1.EmployeeId = t2.SupportRepId where t1.Title = 'Sales Support Agent'
join
	Invoice t3
on t2.

-- How many customers are assigned to each sales agent?
SELECT Employee.EmployeeId, Employee.LastName, Employee.FirstName, COUNT(Customer.CustomerId) as 'Assigned Customers'
FROM Employee
JOIN Customer ON Employee.EmployeeId = Customer.SupportRepID
WHERE Employee.Title = 'Sales Support Agent'
GROUP BY Employee.EmployeeId, Employee.LastName, Employee.FirstName;

-- Which track was purchased the most in 2010?
select Track.Name as MostPopularTrack, count(Invoice.InvoiceLineId) as Purchases
    from Track
    inner join InvoiceLine
        on Track.TrackId = InvoiceLine.TrackId
    inner join Invoice
        on InvoiceLine.InvoiceId = Invoice.InvoiceId
    where year(Invoice.InvoiceDate) = 2010
    group by Track.Name order by Purchases desc
    
-- Show the top three best selling artists.
-- TODO: fix 
TOP 3
ar.Name AS ArtistName,
SUM(ilUnitPrice * il.Quantity) AS TotalSales
FROM Artist ar
JOIN Album al ON ar.ArtistId = al.ArtistId
GROUP BY ar.ArtistId, ar.Name
ORDER BY TotalSales DESC;

-- Which customers have the same initials as at least one other customer?
GO
CREATE VIEW CustomerInitials AS 
SELECT CustomerId, FirstName, LastName, LEFT(FirstName,1) + LEFT(LastName,1) AS Initials FROM Customer;
GO



-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?
SELECT Artist."Name" FROM "Artist" 
EXCEPT
SELECT ARTIST."Name" FROM "Artist"
INNER JOIN "Album" ON "Artist"."ArtistId" = "Album"."ArtistId";

SELECT Artist.Name
FROM dbo.Artist
WHERE Artist.ArtistId NOT IN (SELECT Album.ArtistId FROM dbo.Album);

-- 2. which artists did not record any tracks of the Latin genre?
SELECT Artist.Name FROM Artist
EXCEPT 
SELECT Artist.Name FROM Artist 
JOIN Album ON Artist.ArtistId = Album.ArtistId
JOIN Track ON Track.AlbumId = Album.AlbumId 
JOIN Genre ON Genre.GenreId = Tr ck.TrackId 
WHERE Genre.Name = 'Latin';

-- 3. which video track has the longest length? (use media type table)
select top 1 t1.* from
    Track t1
join
    MediaType t2
on t1.MediaTypeId = t2.MediaTypeId
where t2.Name = 'Protected MPEG-4 video file'
order by Milliseconds desc
--  4.  boss employee (the one who reports to nobody)
WITH BossEmployee as (
    SELECT E.EmployeeId,E.FirstName,E.LastName,E.City FROM Employee AS E
    EXCEPT
    SEELECT E.EmployeeId,E.FirstName,E.LastName,E.City FROM Employee AS E JOIN
    Customer ON E.EmployeeId = Customer.SupportRepId
)
SELECT "Employee"."FirstName","Employee"."LastName" FROM "Employee" JOIN "BossEmployee" ON "Employee"."EmployeeId" <> "BossEmployee"."EmployeeId" AND "Employee"."City" = BossEmployee."City";

select top 1 t1.* from
    Track t1
join
    MediaType t2
on t1.MediaTypeId = t2.MediaTypeId
where t2.Name = 'Protected MPEG-4 video file'
order by Milliseconds desc

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?
SELECT SUM(InvoiceLine.Quantity)
FROM Customer
JOIN Invoice ON (Customer.CustomerId = Invoice.CustomerId)
JOIN InvoiceLine ON (Invoice.InvoiceId = InvoiceLine.InvoiceId)
JOIN Track ON (InvoiceLine.TrackId = Track.TrackId)
JOIN MediaType ON (Track.MediaTypeId = MediaType.MediaTypeId)
WHERE Customer.Country = 'Germany'
    AND MediaType.Name LIKE '%audio%'
GROUP BY Customer.Country

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.
-- select FirstName, LastName from customer inner join select * from employee where birthdate >=;
SELECT
  c.FirstName,
  c.LastName,
  c.Country
FROM Customer AS c
JOIN Employee AS e
  ON e.EmployeeId = c.SupportRepId
WHERE
  -- age at hire < 35 (exact, not off by one)
  DATEDIFF(year, e.BirthDate, e.HireDate)
    - CASE
        WHEN DATEADD(year, DATEDIFF(year, e.BirthDate, e.HireDate), e.BirthDate) > e.HireDate
          THEN 1 ELSE 0
      END < 35
ORDER BY c.Country, c.LastName, c.FirstName;



-- DML exercises

-- 1. insert two new records into the employee table.
INSERT INTO Employee (EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate, HireDate, Address, City, State, Country, PostalCode, Phone, Fax, Email)
VALUES
(10, 'Smith', 'John', 'Sales Support Agent', 1, '1985-04-15', '2025-01-10', '123 Main St', 'Chicago', 'IL', 'USA', '60601', '555-1234', NULL, 'john.smith@example.com'),
(11, 'Brown', 'Emily', 'Sales Support Agent', 1, '1990-08-22', '2025-01-15', '456 Oak Ave', 'New York', 'NY', 'USA', '10001', '555-5678', NULL, 'emily.brown@example.com');

-- 2. insert two new records into the tracks table.
INSERT INTO Track (TrackId, Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice)
VALUES
(5000, 'New Song One', 1, 1, 1, 'John Composer', 250000, 5000000, 0.99),
(5001, 'New Song Two', 1, 1, 1, 'Emily Composer', 300000, 6000000, 1.29);

-- 3. update customer Aaron Mitchell's name to Robert Walter
UPDATE Customer SET FirstName = 'Robert', LastName = 'Walter'
WHERE FirstName = 'Aaron' AND LastName = 'Mitchell';

-- 4. delete one of the employees you inserted.
DELETE FROM Employee WHERE EmployeeId = 11;

-- 5. delete customer Robert Walter.
