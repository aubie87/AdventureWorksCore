/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [BusinessEntityID]
      ,[rowguid]
      ,[ModifiedDate]
  FROM [AdventureWorks2014].[Person].[BusinessEntity]
  where BusinessEntityID > 20600
  Order by BusinessEntityID desc

  insert into Person.BusinessEntity
	(rowguid)
	values
	(default)

select * from Person.Person
where BusinessEntityID = 21780