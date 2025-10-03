
CREATE DATABASE IF NOT EXISTS F1;
USE F1;


CREATE TABLE Circuits (
    CircuitId VARCHAR(50) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Location VARCHAR(100),
    Country VARCHAR(100),
    Lat DECIMAL(10,6),
    Lng DECIMAL(10,6)
);

CREATE TABLE Constructors (
    ConstructorId VARCHAR(50) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Nationality VARCHAR(50)
);


CREATE TABLE Drivers (
    DriverId VARCHAR(50) PRIMARY KEY,
    Number INT,
    Code VARCHAR(10),
    Forename VARCHAR(50),
    Surname VARCHAR(50),
    Dob DATE,
    Nationality VARCHAR(50),
    ConstructorId VARCHAR(50),           
    FOREIGN KEY (ConstructorId) REFERENCES Constructors(ConstructorId)
);

