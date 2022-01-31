CREATE OR ALTER PROC GetZoneByState 
@stateId BIGINT
AS
BEGIN
	SELECT Z.Id AS [Value],Z.Name FROM Zone Z WHERE (@stateId=-1 OR Z.StateId=@stateId)
END
GO
CREATE OR ALTER PROC GetDistrictByZone  
@zoneId BIGINT
AS
BEGIN
	SELECT Id AS [Value],Name FROM District  WHERE (@zoneId=-1 OR ZoneId=@zoneId)
END
GO
CREATE OR ALTER PROC GetRegionByDistrict   
@districtId BIGINT
AS
BEGIN
	SELECT Id AS [Value],Name FROM Region   WHERE (@districtId=-1 OR DistrictId=@districtId)
END
GO
CREATE OR ALTER PROC GetBranchByRegion    
@regionId BIGINT
AS
BEGIN
	SELECT Id AS [Value],Name FROM Branch  WHERE (@regionId=-1 OR RegionId=@regionId)
END
GO