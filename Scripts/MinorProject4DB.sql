SQL

USE MinorProject4DB;
GO

-- Clear any partial data safely
DELETE FROM Drivers;
DELETE FROM Principals;
DELETE FROM Teams;
GO

-- 11 2026 Teams (Including Audi & Cadillac)
INSERT INTO Teams (TeamName, BaseCountry, EngineSupplier) VALUES
('Red Bull Racing', 'Austria', 'Red Bull Powertrains-Ford'),
('Ferrari', 'Italy', 'Ferrari'),
('McLaren', 'United Kingdom', 'Mercedes'),
('Mercedes', 'United Kingdom', 'Mercedes'),
('Aston Martin', 'United Kingdom', 'Honda'),
('Alpine', 'France', 'Renault'),
('Williams', 'United Kingdom', 'Mercedes'),
('Racing Bulls', 'Italy', 'Red Bull Powertrains-Ford'),
('Audi', 'Switzerland', 'Audi'),
('Haas', 'United States', 'Ferrari'),
('Cadillac', 'United States', 'Ferrari');

-- Drivers
INSERT INTO Drivers (DriverName, Nationality, CarNumber, TeamID) VALUES
('Max Verstappen', 'Dutch', 1, 1),
('Charles Leclerc', 'Monegasque', 16, 2),
('Lando Norris', 'British', 4, 3),
('George Russell', 'British', 63, 4),
('Fernando Alonso', 'Spanish', 14, 5),
('Pierre Gasly', 'French', 10, 6),
('Alex Albon', 'Thai', 23, 7),
('Liam Lawson', 'New Zealander', 30, 8),
('Nico Hulkenberg', 'German', 27, 9),
('Esteban Ocon', 'French', 31, 10),
('Sergio Perez', 'Mexican', 11, 11),
('Valtteri Bottas', 'Finnish', 77, 11);

-- Principals
INSERT INTO Principals (PrincipalName, RoleTitle, YearsExperience, TeamID) VALUES
('Christian Horner', 'Team Principal', 20, 1),
('Frederic Vasseur', 'Team Principal', 15, 2),
('Andrea Stella', 'Team Principal', 12, 3),
('Toto Wolff', 'Team Principal', 13, 4),
('Mike Krack', 'Team Principal', 10, 5),
('Oliver Oakes', 'Team Principal', 5, 6),
('James Vowles', 'Team Principal', 15, 7),
('Laurent Mekies', 'Team Principal', 14, 8),
('Jonathan Wheatley', 'Team Principal', 18, 9),
('Ayao Komatsu', 'Team Principal', 11, 10);
GO