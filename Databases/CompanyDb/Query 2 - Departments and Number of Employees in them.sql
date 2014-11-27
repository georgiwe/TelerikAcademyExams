USE CompanyDb
GO

SELECT Name, COUNT(*) [Number of Employees]
FROM Departments d
INNER JOIN Employees e
	ON d.DepartmentID = e.DepartmentID
GROUP BY d.Name