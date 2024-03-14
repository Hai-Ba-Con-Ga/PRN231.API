-- New script in localhost.
-- Author: KhoaBD
-- CreatedDate: Mar 14, 2024
-- UpdatedDate: 2:17:26 PM
-- Description: 

USE PRN231_IOT;
CREATE TABLE Account (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    "Role" varchar(50) NOT NULL,
    Username varchar(100) NOT NULL,
    Password varchar(150) NOT NULL,
    Email varchar(150),
    FirstName nvarchar(100),
    LastName nvarchar(100),
    BirthDate Date NOT NULL,
    ParentAccountID int,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT DEFAULT 0,
    UpdatedBy INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE DeviceType (
    DeviceTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName nvarchar(100) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT DEFAULT 0,
    UpdatedBy INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE Device (
    DeviceID INT IDENTITY(1,1) PRIMARY KEY,
    SerialID varchar(100) NOT NULL,
    DeviceName nvarchar(100) NOT NULL,
    OwnerID int NOT NULL,
    DeviceTypeID int NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT DEFAULT 0,
    UpdatedBy INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (OwnerID) REFERENCES Account(AccountID),
    FOREIGN KEY (DeviceTypeID) REFERENCES DeviceType(DeviceTypeID)
);

CREATE TABLE CollectedDataType(
    CollectedDataTypeID INT IDENTITY(1,1) PRIMARY KEY,
    DataTypeName nvarchar(100) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT DEFAULT 0,
    UpdatedBy INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE CollectedData (
    CollectedDataID INT IDENTITY(1,1) PRIMARY KEY,
    CollectedDataTypeID INT NOT NULL,
    DataValue varchar(100) NOT NULL,
    DataUnit varchar(100) NOT NULL,
    DeviceID int NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT DEFAULT 0,
    UpdatedBy INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CollectedDataTypeID) REFERENCES CollectedDataType(CollectedDataTypeID),
    FOREIGN KEY (DeviceID) REFERENCES Device(DeviceID)
);










