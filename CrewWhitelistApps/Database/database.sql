
CREATE DATABASE db_crew_whitelist
USE db_crew_whitelist

CREATE TABLE administrator (
		
		id_admin INT IDENTITY(1,1) NOT NULL,
		username CHAR (20) NULL,
		password CHAR(20) NULL,
		role VARCHAR(20)  CHECK (role IN('Admin', 'Admin Whitelist'))
		
		PRIMARY KEY (id_admin),
	)

CREATE TABLE crew (
		
		id_crew CHAR (12) NOT NULL,
		name VARCHAR (50) NULL,
		date_list DATE NULL,
		status VARCHAR(10)  CHECK (status IN('Pramugari', 'Pilot')),
		airport VARCHAR(30) NULL,
		company_airways VARCHAR(30) NOT NULL,

		
		PRIMARY KEY (id_crew),
	)

CREATE TABLE crew_schedule (
		
		id_schedule INT IDENTITY(1,1) NOT NULL,
		id_crew CHAR(12) NOT NULL,
		start_date DATE NULL,
		end_date DATE NULL,
		
		PRIMARY KEY (id_schedule),
		FOREIGN KEY (id_crew) REFERENCES crew (id_crew)
	)

--------------------------------------- INSERT ADMINISTRATOR -------------------------------------

INSERT Administrator VALUES ('sa', 'admin', 'admin')
INSERT Administrator VALUES ('sa whitelist', 'whitelist', 'admin whitelist')

--------------------------------------- FUNCTION ------------------------------------------------

-- MD5
CREATE FUNCTION MD5 (
	
		@value VARCHAR(255)
	)
RETURNS VARCHAR(32)
AS
BEGIN
	RETURN SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5', @value)),3,32);
END
GO

-- AUTONUMBER
CREATE FUNCTION AutoGenerate()
RETURNS CHAR(12)
AS 
	BEGIN
	
	DECLARE @id CHAR(12),  @sort INT
	
	SELECT @sort = ISNULL(MAX (right (c.id_crew, 3)), 0) FROM crew c
	SET @id = FORMAT(GETDATE(),'ddMMyyyy') + '-' + RIGHT('00' + CAST( @sort + 1 AS varchar(3)),3)
	 
RETURN @id
END

--------------------------------------- PROCEDURE -----------------------------------------------

-- LOGIN
CREATE PROCEDURE ProcLogin (
		@user CHAR(20),
		@pass CHAR(20),
		@role VARCHAR(20)
	)
AS 
BEGIN 

	SET NOCOUNT ON;
 
SELECT a.username , dbo.MD5(a.password) AS password, a.role FROM administrator a
WHERE a.username COLLATE Latin1_General_CS_AS = @user AND a.password COLLATE Latin1_General_CS_AS = @pass
AND a.role COLLATE Latin1_General_CS_AS = @role

END
GO

--================================================ CREW ================================================================

------------------------------- INSERT 
CREATE PROCEDURE ProcInsertCrew ( 

		@name VARCHAR(50),
		@status VARCHAR(10),
		@airport VARCHAR(30),
		@company VARCHAR(30)
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

	DECLARE @autoid CHAR(12), @date VARCHAR(10)
	SELECT @autoid = dbo.AutoGenerate()
 
INSERT INTO crew VALUES (@autoid, @name, GETDATE(), @status, @airport, @company)

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

---------------------------------- EDIT 
CREATE PROCEDURE ProcUpdateCrew (

		@id CHAR(12),
		@name VARCHAR(50),
		@status VARCHAR(10),
		@airport VARCHAR(30),
		@company VARCHAR(30)
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

UPDATE crew SET name = @name, status = @status, airport = @airport, company_airways = @company WHERE id_crew = @id

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

------------------------------------ DELETE 
CREATE PROCEDURE ProcDeleteCrew (

		@id CHAR(12)
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

DELETE crew WHERE id_crew = @id

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

----------------------------------------- SELECT 
CREATE PROCEDURE ProcGetAllCrew
AS 
BEGIN 

	SET NOCOUNT ON;
 
SELECT c.id_crew, c.name, FORMAT(c.date_list, 'dd-MM-yyyy') AS date_list, c.status, c.airport, c.company_airways FROM crew c

END
GO

------------------------------------------------ SELECT BY NAME

CREATE PROCEDURE ProcGetCrewByName
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.id_crew, c.name, c.status FROM crew c
	WHERE c.id_crew NOT IN (SELECT id_crew FROM crew_schedule)

END
GO

--=========================================================== CREW SCHEDULE =============================================================

-------------------------------------------- INSERT 
CREATE PROCEDURE ProcInsertCrewSchedule (

		@idcrew CHAR(12),
		@start_date DATE,
		@end_date DATE
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

	

	INSERT INTO crew_schedule VALUES (@idcrew, @start_date, @end_date)

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

-------------------------------------------------- EDIT
CREATE PROCEDURE ProcUpdateCrewSchedule (

		@id INT,
		@start_date DATE,
		@end_date DATE
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

UPDATE crew_schedule SET start_date = @start_date, end_date = @end_date  WHERE id_schedule = @id

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

----------------------------------------------------------------- DELETE
CREATE PROCEDURE ProcDeleteCrewSchedule (

		@id CHAR(12)
	)
AS 
BEGIN TRANSACTION

	SET NOCOUNT ON;

DELETE crew_schedule WHERE id_schedule = @id

IF @@error = 0 
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO

------------------------------------------------ SELECT
CREATE PROCEDURE ProcGetAllCrewSchedule
AS 
BEGIN 

	SET NOCOUNT ON;
 
	SELECT cs.id_schedule, cs.id_crew, c.name, FORMAT(cs.start_date, 'dd-MM-yyyy') AS start_date, FORMAT(cs.end_date, 'dd-MM-yyyy') AS end_date
	FROM crew_schedule cs JOIN crew c
	ON cs.id_crew = c.id_crew

END
GO

------------------------------------------------ SELECT BY DAY
CREATE PROCEDURE ProcGetByDayCrewSchedule
AS 
BEGIN 

	SET NOCOUNT ON;

	DECLARE @datenow DATE
	SELECT @datenow = FORMAT(GETDATE(), 'yyyy-MM-dd') 

	SELECT cs.id_schedule, cs.id_crew, c.name, FORMAT(cs.start_date, 'dd-MM-yyyy') AS start_date, FORMAT(cs.end_date, 'dd-MM-yyyy') AS end_date, c.status
	FROM crew_schedule cs JOIN crew c
	ON cs.id_crew = c.id_crew
	WHERE GETDATE() >= cs.start_date AND @datenow <= cs.end_date

END
GO