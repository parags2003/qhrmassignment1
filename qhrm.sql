create database qhrm

use qhrm

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

select * from Products