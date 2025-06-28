USE master;
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'EmployeeManagementSystem')
BEGIN
    CREATE DATABASE EmployeeManagementSystem;
END
GO

USE EmployeeManagementSystem;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departments' AND xtype='U')
BEGIN
    CREATE TABLE Departments (
        DepartmentID INT PRIMARY KEY,
        DepartmentName VARCHAR(100) NOT NULL
    );
END
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT PRIMARY KEY,
        FirstName VARCHAR(50) NOT NULL,
        LastName VARCHAR(50) NOT NULL,
        DepartmentID INT,
        Salary DECIMAL(10,2) NOT NULL,
        JoinDate DATE NOT NULL,
        FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
    );
END
GO
IF NOT EXISTS (SELECT * FROM Departments)
BEGIN
    INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
    (1, 'HR'),
    (2, 'IT'),
    (3, 'Finance');
END
GO
IF NOT EXISTS (SELECT * FROM Employees)
BEGIN
    INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
    (1, 'John', 'Doe', 1, 5000.00, '2020-01-15'),
    (2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'),
    (3, 'Bob', 'Johnson', 3, 5500.00, '2021-07-01');
END
GO
IF OBJECT_ID('dbo.fn_CalculateAnnualSalary', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_CalculateAnnualSalary;
GO

CREATE FUNCTION dbo.fn_CalculateAnnualSalary(@EmployeeID INT)
RETURNS DECIMAL(12,2)
AS
BEGIN
    DECLARE @AnnualSalary DECIMAL(12,2);
    SELECT @AnnualSalary = Salary * 12
    FROM Employees
    WHERE EmployeeID = @EmployeeID;
    IF @AnnualSalary IS NULL
        SET @AnnualSalary = 0;
    
    RETURN @AnnualSalary;
END
GO
-- Method 1: Using SELECT statement
SELECT 
    'John Doe salary via fn_calcAnnualSal' AS ExecutionMethod,
    dbo.fn_CalculateAnnualSalary(1) AS AnnualSalary;

-- Method 2: Using DECLARE and PRINT
DECLARE @Result DECIMAL(12,2);
SET @Result = dbo.fn_CalculateAnnualSalary(1);
SELECT 
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName,
    e.Salary AS MonthlySalary,
    e.Salary * 12 AS ManualCalculation,
    dbo.fn_CalculateAnnualSalary(e.EmployeeID) AS FunctionResult,
    CASE 
        WHEN (e.Salary * 12) = dbo.fn_CalculateAnnualSalary(e.EmployeeID) 
        THEN 'VERIFIED' 
        ELSE 'ERROR' 
    END AS VerificationStatus
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.EmployeeID = 1;
