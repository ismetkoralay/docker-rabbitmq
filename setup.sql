CREATE SCHEMA IF NOT EXISTS CustomerDB;
USE CustomerDB;

CREATE TABLE IF NOT EXISTS Customer
(
    Id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) not null,
    Password VARCHAR(255) not null,
    IsActive BIT not null,
    CreatedBy INT not null,
    CreatedAt DATETIME not null,
    ModifiedBy INT null,
    ModifiedAt DATETIME null
);

CREATE TABLE IF NOT EXISTS Contact
(
    Id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    CustomerId INT not null,
    Address VARCHAR(255) not null,
    Phone VARCHAR(11) not null,
    IsActive BIT not null,
    CreatedBy INT not null,
    CreatedAt DATETIME not null,
    ModifiedBy INT null,
    ModifiedAt DATETIME null
    );