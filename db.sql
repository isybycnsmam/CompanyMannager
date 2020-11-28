create database company_mannager;
use company_mannager;

/*imported*/

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";

CREATE TABLE `companies` (
  `Id` bigint(20) NOT NULL,
  `Name` longtext NOT NULL,
  `EstablishmentYear` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `employees` (
  `Id` bigint(20) NOT NULL,
  `FirstName` longtext NOT NULL,
  `LastName` longtext NOT NULL,
  `DateOfBirth` datetime(6) NOT NULL,
  `JobTitle` int(11) NOT NULL,
  `CompanyId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20201128143050_InitialMigration', '3.1.10');

ALTER TABLE `companies`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `employees`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Employees_CompanyId` (`CompanyId`);

ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

ALTER TABLE `companies`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

ALTER TABLE `employees`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

ALTER TABLE `employees`
  ADD CONSTRAINT `FK_Employees_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `companies` (`Id`) ON DELETE CASCADE;
COMMIT;