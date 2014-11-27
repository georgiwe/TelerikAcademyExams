USE CompanyDb
GO

SELECT FirstName + ' ' + LastName AS [Employee Full Name], YearlySalary AS [Yearly Salary]
FROM Employees
WHERE YearlySalary >= 100000 and
	  YearlySalary <= 150000
ORDER BY YearlySalary ASC