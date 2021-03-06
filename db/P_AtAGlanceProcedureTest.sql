USE [AMBSTZ]
GO
/****** Object:  StoredProcedure [dbo].[P_AtAGlanceProcedure]    Script Date: 1/31/2022 1:08:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[P_AtAGlanceProcedureTest]
			@EndDate AS DateTime,
			@StateId bigint,
			@ZoneId bigint,
			@DistrictId bigint,
			@RegionId bigint,
			@BranchId bigint
			AS
			BEGIN

			SET TRANSACTION ISOLATION LEVEL SNAPSHOT;

			DECLARE @PARRefDate DATETIME
			SET @PARRefDate = '2020-01-01' -- new PAR calculation method imposed from this date

			DECLARE @_EndDate AS DateTime,
			@_StateId bigint,
			@_ZoneId bigint,
			@_DistrictId bigint,
			@_RegionId bigint,
			@_BranchId bigint

			SET @_EndDate = @EndDate
			SET @_StateId = @StateId
			SET @_ZoneId = @ZoneId
			SET @_DistrictId = @DistrictId
			SET @_RegionId = @RegionId
			SET @_BranchId = @BranchId

			DECLARE @AllBranch Bit

			IF (@_StateId IS NULL OR @_StateId  = -1) AND (@_ZoneId IS NULL OR @_ZoneId  = -1) AND (@_DistrictId IS NULL OR @_DistrictId  = -1) AND (@_RegionId IS NULL OR @_RegionId  = -1) AND (@_BranchId IS NULL OR @_BranchId  = -1)
			BEGIN
			SET @AllBranch = 1
			END
			ELSE
			BEGIN
			SET @AllBranch = 0
			END

			DECLARE @YearStartDate DateTime
			SET @YearStartDate = DATENAME(YEAR, @EndDate) + '-01-01'

			DECLARE @MonthStartDate DateTime
			SET @MonthStartDate = DATENAME(YEAR, @EndDate) + '-' + CONVERT(VARCHAR, DATEPART(MONTH, @EndDate)) + '-01'

			SELECT
			Branch.*
			INTO #Temp0
			FROM Branch
			INNER JOIN Region ON Branch.RegionId = Region.Id AND (@_BranchId IS NULL OR Branch.Id = @_BranchId) AND (@_RegionId IS NULL OR Region.Id = @_RegionId)
			INNER JOIN District ON Region.DistrictId = District.Id AND (@_DistrictId IS NULL OR District.Id = @_DistrictId)
			INNER JOIN Zone ON District.ZoneId = Zone.Id AND (@_ZoneId IS NULL OR Zone.Id = @_ZoneId)
			INNER JOIN State ON Zone.StateId = State.Id AND (@_StateId IS NULL OR State.Id = @_StateId)

			SELECT
			COUNT(*) AS NumberOfLO
			INTO #Temp1
			FROM P_LoanOfficer
			INNER JOIN P_LoanOfficerMovement ON P_LoanOfficerId = P_LoanOfficer.Id AND @_EndDate BETWEEN P_LoanOfficerMovement.StartingDate AND P_LoanOfficerMovement.EndingDate
			INNER JOIN #Temp0 AS Branch ON Branch.Id = P_LoanOfficerMovement.BranchId
			WHERE (P_LoanOfficer.IsActive = 1 OR P_LoanOfficer.EndingDate > @_EndDate)
			AND P_LoanOfficer.StartingDate <= @_EndDate
			AND Designation NOT IN ('Branch Manager', 'Assistant Branch Manager')

			PRINT 'LO: ' + FORMAT(GETDATE(), 'yyyy-MM-dd HH:mm:ss')

			SELECT
			CONVERT(INT, NULL) AS DefaultP_ProgramId,
			CONVERT(INT, NULL) AS NumberOfGroup
			INTO #Temp2

			DELETE #Temp2

			SELECT
			CONVERT(INT, NULL) AS DefaultP_ProgramId,
			CONVERT(INT, NULL) AS NumberOfMember
			INTO #Temp3

			DELETE #Temp3

			

			INSERT INTO #Temp2
			SELECT
			DefaultP_ProgramId,
			COUNT(*) AS NumberOfGroup
			FROM P_Group
			INNER JOIN P_GroupMovement ON P_GroupId = P_Group.Id AND @_EndDate BETWEEN P_GroupMovement.StartingDate AND P_GroupMovement.EndingDate
			INNER JOIN P_LoanOfficerMovement ON P_LoanOfficerMovement.P_LoanOfficerId = P_GroupMovement.P_LoanOfficerId AND @_EndDate BETWEEN P_LoanOfficerMovement.StartingDate AND P_LoanOfficerMovement.EndingDate
			INNER JOIN P_Program ON P_Program.Id = DefaultP_ProgramId
			INNER JOIN #Temp0 AS Branch ON Branch.Id = P_LoanOfficerMovement.BranchId
			WHERE P_ProgramTypeId <> 8
			AND FormationDate <= @_EndDate
			AND (P_Group.IsActive = 1 OR P_Group.ClosingDate > @_EndDate)
			GROUP BY DefaultP_ProgramId

			PRINT 'GROUP: ' + FORMAT(GETDATE(), 'yyyy-MM-dd HH:mm:ss')

			IF @AllBranch = 1
			BEGIN
			INSERT INTO #Temp3
			SELECT
			DefaultP_ProgramId,
			COUNT(*) AS NumberOfMember
			FROM P_Member
			INNER JOIN P_Program ON P_Program.Id = DefaultP_ProgramId
			WHERE P_ProgramTypeId <> 8
			AND AdmissionDate <= @_EndDate
			AND (P_Member.IsActive = 1 OR P_Member.ClosingDate > @_EndDate)
			GROUP BY DefaultP_ProgramId
			END
			ELSE
			BEGIN
			INSERT INTO #Temp3
			SELECT
			DefaultP_ProgramId,
			COUNT(*) AS NumberOfMember
			FROM P_Member
			INNER JOIN P_MemberMovement ON P_MemberId = P_Member.Id AND @_EndDate BETWEEN P_MemberMovement.StartingDate AND P_MemberMovement.EndingDate
			INNER JOIN P_GroupMovement ON P_GroupMovement.P_GroupId = P_MemberMovement.P_GroupId AND @_EndDate BETWEEN P_GroupMovement.StartingDate AND P_GroupMovement.EndingDate
			INNER JOIN P_LoanOfficerMovement ON P_LoanOfficerMovement.P_LoanOfficerId = P_GroupMovement.P_LoanOfficerId AND @_EndDate BETWEEN P_LoanOfficerMovement.StartingDate AND P_LoanOfficerMovement.EndingDate
			INNER JOIN P_Program ON P_Program.Id = DefaultP_ProgramId
			INNER JOIN #Temp0 AS Branch ON Branch.Id = P_LoanOfficerMovement.BranchId
			WHERE P_ProgramTypeId <> 8
			AND AdmissionDate <= @_EndDate
			AND (P_Member.IsActive = 1 OR P_Member.ClosingDate > @_EndDate)
			GROUP BY DefaultP_ProgramId
			END


			DECLARE @Table AS TABLE(
			[X] Varchar(200),
			[Y] Decimal(18, 10)
			)

			INSERT INTO @Table SELECT 'Total number of branches', COUNT(*) FROM #Temp0 AS Branch WHERE (Branch.IsActive = 1 OR (Branch.IsActive = 0 AND Branch.ClosingDate >= @EndDate)) AND Branch.OpeningDate <= @EndDate AND Branch.Id >= 0
			INSERT INTO @Table SELECT 'Total number of loan officers', SUM(NumberOfLO) FROM #Temp1
			INSERT INTO @Table SELECT 'Total number of active groups', SUM(NumberOfGroup) FROM #Temp2
			INSERT INTO @Table SELECT 'Total number of active members', SUM(NumberOfMember) FROM #Temp3

			SELECT * FROM @Table

			DROP TABLE #Temp0
			DROP TABLE #Temp1
			DROP TABLE #Temp2
			DROP TABLE #Temp3

			END

			--exec [dbo].[P_AtAGlanceProcedureTest] '2022-01-31', -1, 4, 4, 28, 40