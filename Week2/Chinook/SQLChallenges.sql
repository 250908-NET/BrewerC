-- SETUP:
    -- Create a database server (docker)
        -- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    -- Connect to the server (Azure Data Studio / Database extension)
    -- Test your connection with a simple query (like a select)
    -- Execute the Chinook database (to create Chinook resources in your db)



-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA

use Chinook;
select "FirstName", "LastName", "CustomerId", "Country" from "Customer" where "Country" <> 'USA';
select concat(LastName, ', ', FirstName) as Name, CustomerId, Country 
    from Customer
    where Country <> 'USA';

-- List all customers from Brazil

use Chinook;
select * from "Customer" where "Country" = 'Brazil';

-- List all sales agents

use Chinook;
select * from "Employee" where "Title" = 'Sales Support Agent';

-- Retrieve a list of all countries in billing addresses on invoices

use Chinook;
select "BillingCountry" from "Invoice";

-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?

use Chinook;
select count(*) from "Invoice" where YEAR("InvoiceDate") = 2009;
select sum("Total") from "Invoice" where YEAR("InvoiceDate") = 2009;
select sum("Total") from "Invoice" group by YEAR("InvoiceDate");

    -- (challenge: find the invoice count sales total for every year using one query)

-- how many line items were there for invoice #37

use Chinook;
select count(*) from "InvoiceLine" where "InvoiceId" = 37;

-- how many invoices per country? BillingCountry  # of invoices -

use Chinook;
select "BillingCountry", count(*) from "Invoice" group by "BillingCountry";

-- Retrieve the total sales per country, ordered by the highest total sales first.
use Chinook;
select "BillingCountry", sum("Total") as sum_total from "Invoice" group by "BillingCountry" order by sum_total desc;


-- JOINS CHALLENGES
-- Every Album by Artist

use Chinook;
select t1.Name, t2.Title from
	Artist t1
join
	Album t2
on t1.ArtistId = t2.ArtistId;
	
-- All songs of the rock genre

select * from
	Track t1
join
	Genre t2
on t1.GenreId = t2.GenreId where t2.Name = 'Rock';

-- Show all invoices of customers from brazil (mailing address not billing)

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

select * from
	Employee t1
join
	Customer t2
on t1.EmployeeId = t2.SupportRepId where t1.Title = 'Sales Support Agent'
join
	Invoice t3
on t2.

-- How many customers are assigned to each sales agent?

-- Which track was purchased the most ing 20010?

-- Show the top three best selling artists.

-- Which customers have the same initials as at least one other customer?



-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

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

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.


-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.
