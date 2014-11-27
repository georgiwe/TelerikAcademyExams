USE CompanyDb
GO


-- This query is incomplete - it does not select the count.
-- If I have the time, I'll come back to it later.
SELECT e.FirstName + ' ' + e.LastName [Employee Full Name],
	   p.Name [Project Name],
	   ep.StartDate [Start Date],
	   ep.EndDate [End Date]
FROM Employees e
INNER JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
INNER JOIN EmployeesProjects ep
	ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects p
	ON ep.ProjectID = p.ProjectID
INNER JOIN Reports r
	ON r.Time >= ep.StartDate and
	   r.Time <= ep.EndDate




--SELECT COUNT(*)
--FROM (SELECT e.FirstName + ' ' + e.LastName [Employee Full Name],
--	   p.Name [Project Name],
--	   ep.StartDate [Start Date],
--	   ep.EndDate [End Date]
--FROM Employees e
--INNER JOIN Departments d
--	ON e.DepartmentID = d.DepartmentID
--INNER JOIN EmployeesProjects ep
--	ON e.EmployeeID = ep.EmployeeID
--INNER JOIN Projects p
--	ON ep.ProjectID = p.ProjectID
--INNER JOIN Reports r
--	ON r.) d