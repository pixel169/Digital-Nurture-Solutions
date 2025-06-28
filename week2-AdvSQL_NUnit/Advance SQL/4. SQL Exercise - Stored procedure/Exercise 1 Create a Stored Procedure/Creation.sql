DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Departments;
DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
DROP PROCEDURE IF EXISTS sp_InsertEmployee;
DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
DROP PROCEDURE IF EXISTS sp_InsertEmployee;
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Departments;

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    Salary DECIMAL(10,2),
    JoinDate DATE
);

INSERT INTO Departments (DepartmentID, DepartmentName) VALUES 
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Marketing');

INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES 
('John', 'Doe', 1, 5000.00, '2020-01-15'),
('Jane', 'Smith', 2, 6000.00, '2019-03-22'),
('Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
('Emily', 'Davis', 4, 5500.00, '2021-11-05');

GO

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SET NOCOUNT ON;    
    SELECT 
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        d.DepartmentName,
        e.Salary,
        e.JoinDate
    FROM Employees e
    INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE e.DepartmentID = @DepartmentID
    ORDER BY e.LastName, e.FirstName;
END;

GO

CREATE PROCEDURE sp_InsertEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE
AS
BEGIN
    SET NOCOUNT ON;
        IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @DepartmentID)
    BEGIN
        RAISERROR('Department ID %d does not exist.', 16, 1, @DepartmentID);
        RETURN;
    END    
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
    
    -- Return the new employee ID
    SELECT SCOPE_IDENTITY() AS NewEmployeeID;
END;
-- Run 
DELETE FROM Employees WHERE EmployeeID > 4;

-- Properly insert new employee
EXEC sp_InsertEmployee 
    @FirstName = 'Priyanshu',
    @LastName = 'Ranjan',
    @DepartmentID = 3,
    @Salary = 42000.00,
    @JoinDate = '2025-06-28';

-- Query valid departments only (1-4)
EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;  -- HR
EXEC sp_GetEmployeesByDepartment @DepartmentID = 2;  -- Finance
EXEC sp_GetEmployeesByDepartment @DepartmentID = 3;  -- IT
EXEC sp_GetEmployeesByDepartment @DepartmentID = 4;  
