--SOLUCIÓN PRUEBA_BBDD

--
select a.Nombres + ' ' + a.Apellidos AS NombreEmpleado, d.Nombre as Departamento
from Empleados a
inner join Departamentos d on a.IdDepartamento = d.Id
order by a.Id

--
select p.Nombre,p.Presupuesto,count(ep.IdEmpleado) as conteo
from Proyectos p
inner join EmpleadoProyecto ep on p.Id = ep.IdEmpleado
group by p.Nombre,p.Presupuesto

--
select top 3 e.Nombres + ' ' + e.Apellidos as NombreEmpleado
from Empleados e
inner join EmpleadoProyecto ep on e.Id = ep.IdEmpleado
group by e.Nombres, e.Apellidos
order by count(ep.IdProyecto) desc

--
select d.Nombre as NombreDepartamento
from Departamentos d
left join Empleados e on d.Id = e.IdDepartamento
where e.IdDepartamento is null

--
select e.Id, e.Nombres + ' ' + e.Apellidos as NombreEmpleado
from Empleados e
inner join EmpleadoProyecto ep on e.Id = ep.IdEmpleado
GROUP BY e.Id,e.Nombres, e.Apellidos
HAVING count(ep.IdProyecto) > 1

----------------------------------------------------------------

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Andrés Castañeda
-- Create date: 2025-12-24
-- Description:	Programa que permite retornar
--				información del empleado
-- =============================================
CREATE OR ALTER PROCEDURE sp_buscar_empleado
	@nombreCompleto VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT e.Nombres + '' + e.Apellidos as NombreEmpleado,
		e.NumDocumento, d.Nombre as Departamento
	FROM Empleados e
	INNER JOIN Departamentos d on e.IdDepartamento = d.Id
	WHERE UPPER(e.Nombres + ' ' + e.Apellidos) = UPPER(@nombreCompleto) 
END
GO

-----------------------------------------------------

-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Andrés Castañeda
-- Create date: 2025-12-24
-- Description:	Función que permite traer el 
--				número de proyectos por empleado
-- =============================================
CREATE OR ALTER FUNCTION fn_total_proyectos 
(
	@idEmpleado INT
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @conteo INT = 0;

	SELECT @conteo = COUNT(ep.IdProyecto) FROM EmpleadoProyecto ep
	WHERE ep.IdEmpleado = @idEmpleado
	GROUP BY ep.IdEmpleado
	-- Return the result of the function
	RETURN @conteo

END
GO

------------------------------------------------------
/*
Para realizar un bacjup de la Bd, por el SSMS, se da clic en la opción Back Up, donde se genera el archivo .bak de la
base de datos. Otra opción es pr el mismo menú, pero la opción Generate Scripts, donde se puede personalizar qué se debe agregar
al script, si solo la estructura, o tabien los datos que contienen las tablas, así como todos o determinados objetos de la BD.
Para hacer el restore, usando el SSMS, se da por la opción Restore Database, donde carga el archivo .bak, o si se generó el script,
en la base de datos aster, se ejecuta el script generado y se crea de nuevo la BD.
*/

----------------------------------------------------------------

EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'Ole Automation Procedures', 1;
GO
RECONFIGURE;
GO

BEGIN
	IF OBJECT_ID('TempDB..#UsuariosAPI','U') IS NULL
	BEGIN
		CREATE TABLE #UsuariosAPI(
		id INT PRIMARY KEY NOT NULL,
		name VARCHAR(50),
		userName VARCHAR(20),
		email varchar(50)
		);
		CREATE TABLE #tmpLlamado (dt NVARCHAR(MAX));
	END
	DECLARE @api_url VARCHAR(50)= 'https://jsonplaceholder.typicode.com/users';
	DECLARE @obj AS INT;
	DECLARE @hr AS INT;
	DECLARE @response AS NVARCHAR(MAX);
	EXEC @hr = sp_OACreate 'MSXML2.ServerXMLHTTP.6.0', @obj OUT;
    IF @hr <> 0 
	BEGIN		
		EXEC sp_OAGetErrorInfo @obj;
	END

	EXEC @hr = sp_OAMethod @obj, 'open', NULL, 'get', @api_url, 'false'; -- 'false' for synchronous call
    IF @hr <> 0 
	BEGIN		
		EXEC sp_OAGetErrorInfo @obj;
	END

	EXEC @hr = sp_OAMethod @obj, 'send';
    IF @hr <> 0 
	BEGIN		
		EXEC sp_OAGetErrorInfo @obj;
	END

	EXEC @hr = sp_OAMethod @obj, 'ResponseText', @response OUTPUT;
    IF @hr <> 0 
	BEGIN		
		EXEC sp_OAGetErrorInfo @obj;
	END

	EXEC sp_OADestroy @obj;
	SELECT @response
	IF @response IS NOT NULL AND @response <> ''
	BEGIN
		INSERT INTO #UsuariosAPI (id,name,userName,email)
		 SELECT [id], [name], [username],[email]
         FROM OPENJSON(@response, N'$.data') 
         WITH (
			 id int '$.id',
             name NVARCHAR(100) '$.name',
             username NVARCHAR(100) '$.username',
             email NVARCHAR(100) '$.email' 
         );
	END

	SELECT e.Nombres + '' + e.Apellidos, e.Email FROM Empleados e 
	inner join #UsuariosAPI u on e.Email = u.email

	DROP TABLE #UsuariosAPI
	drop TABLE #tmpLlamado
END


	