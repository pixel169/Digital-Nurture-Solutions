-- =====================================================
-- Exercise 5: Return Data from a Stored Procedure
-- Goal: Create a stored procedure that returns the total number of employees in a department
-- SQL Server Management Studio 21
-- =====================================================

-- First, create the database schema and insert sample data
-- (Run this section first if tables don't exist)

-- Create Departments Table
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

-- Create Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT,
    Salary DECIMAL(10,2),
    JoinDate DATE,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- Insert Sample Data
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES 
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Marketing');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES 
(1, 'John', 'Doe', 1, 5000.00, '2020-01-15'),
(2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'),
(3, 'Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
(4, 'Emily', 'Davis', 4, 5500.00, '2021-11-05');

GO

CREATE PROCEDURE GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        d.DepartmentID,
        d.DepartmentName,
        COUNT(e.EmployeeID) AS TotalEmployees
    FROM Departments d
    LEFT JOIN Employees e ON d.DepartmentID = e.DepartmentID
    WHERE d.DepartmentID = @DepartmentID
    GROUP BY d.DepartmentID, d.DepartmentName;
END

GO
EXEC GetEmployeeCountByDepartment @DepartmentID = 1;
EXEC GetEmployeeCountByDepartment @DepartmentID = 2;
EXEC GetEmployeeCountByDepartment @DepartmentID = 3;
EXEC GetEmployeeCountByDepartment @DepartmentID = 4;
EXEC GetEmployeeCountByDepartment @DepartmentID = 5;
GO
CREATE PROCEDURE GetEmployeeCountByDepartmentOutput
    @DepartmentID INT,
    @TotalEmployees INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @TotalEmployees = COUNT(*)
    FROM Employees 
    WHERE DepartmentID = @DepartmentID;
END

GO
DECLARE @EmployeeCount INT;
EXEC GetEmployeeCountByDepartmentOutput @DepartmentID = 1, @TotalEmployees = @EmployeeCount OUTPUT;
SELECT @EmployeeCount AS 'HR Department Employee Count';
EXEC GetEmployeeCountByDepartmentOutput @DepartmentID = 2, @TotalEmployees = @EmployeeCount OUTPUT;
SELECT @EmployeeCount AS 'Finance Department Employee Count';
EXEC GetEmployeeCountByDepartmentOutput @DepartmentID = 3, @TotalEmployees = @EmployeeCount OUTPUT;
SELECT @EmployeeCount AS 'IT Department Employee Count';
EXEC GetEmployeeCountByDepartmentOutput @DepartmentID = 4, @TotalEmployees = @EmployeeCount OUTPUT;
SELECT @EmployeeCount AS 'Marketing Department Employee Count';
GO
SELECT 
    name AS ProcedureName,
    create_date AS CreatedDate,
    modify_date AS ModifiedDate
FROM sys.procedures 
WHERE name LIKE '%GetEmployeeCount%'
ORDER BY name;
SELECT * FROM Departments ORDER BY DepartmentID;
SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName,
    e.Salary,
    e.JoinDate
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY d.DepartmentName, e.LastName;
SELECT 
    d.DepartmentID,
    d.DepartmentName,
    COUNT(e.EmployeeID) AS TotalEmployees
FROM Departments d
LEFT JOIN Employees e ON d.DepartmentID = e.DepartmentID
GROUP BY d.DepartmentID, d.DepartmentName
ORDER BY d.DepartmentID;
GO
