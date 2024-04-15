USE TMA_DB;

BEGIN TRANSACTION

CREATE TABLE MeasurementUnits (
    UnitId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    UnitName VARCHAR(20) NOT NULL,
);
INSERT INTO MeasurementUnits (UnitName) VALUES
('piece(s)'),
('unit(s)'),
('box(es)'),
('pallet(s)'),
('meter(s)'),
('square meter(s)'),
('liter(s)'),
('kilogram(s)'),
('pair(s)');

CREATE TABLE ItemGroups (
    GroupID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    GroupName VARCHAR(30) NOT NULL,
);
INSERT INTO ItemGroups (GroupName) VALUES
('Electronics'),
('Office Supplies'),
('Tools and Hardware'),
('Furniture'),
('Clothing and Apparel'),
('Food and Beverages'),
('Medical Supplies'),
('Automotive Parts'),
('Cleaning Supplies'),
('Construction Materials');

CREATE TABLE ItemStatuses (
    StatusId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(50) NOT NULL,
);
INSERT INTO ItemStatuses (StatusName) VALUES
('new'),
('used');

CREATE TABLE Photos (
    PhotoId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    PhotoBinary VARBINARY(max),
);

CREATE TABLE Items(
	ItemId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ItemName Varchar(255) NOT NULL,
	ItemGroupId INT NOT NULL FOREIGN KEY REFERENCES ItemGroups(GroupId),
    MeasurementUnitId INT NOT NULL FOREIGN KEY REFERENCES MeasurementUnits(UnitId),
    Quantity INT NOT NULL,
    PriceWithoutVat DECIMAL(10,2) NOT NULL,
    ItemStatusId INT NOT NULL FOREIGN KEY REFERENCES ItemStatuses(StatusId),
    StorageLocation VARCHAR(255),
    ContactPerson TEXT,
    PhotoId INT FOREIGN KEY REFERENCES Photos(PhotoId),
);



CREATE TABLE RequestStatuses (
    StatusId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(50) NOT NULL,
);
INSERT INTO RequestStatuses (StatusName) VALUES
('New'),
('Approved'),
('Rejected');

CREATE TABLE Requests (
    RequestId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    EmployeeName VARCHAR(255) NOT NULL,
    ItemId INT NOT NULL FOREIGN KEY 
		REFERENCES Items(ItemId),
    MeasurementUnitId INT NOT NULL FOREIGN KEY 
		REFERENCES MeasurementUnits(UnitId),
    Quantity INT NOT NULL,
    PriceWithoutVat DECIMAL(10,2) NOT NULL,
    Comment TEXT,
    RequestStatusId INT NOT NULL FOREIGN KEY REFERENCES RequestStatuses(StatusId),
);

COMMIT TRANSACTION