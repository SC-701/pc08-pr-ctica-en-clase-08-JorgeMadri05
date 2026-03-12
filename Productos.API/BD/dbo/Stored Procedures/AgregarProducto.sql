CREATE PROCEDURE AgregarProducto
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@IdSubCategoria uniqueidentifier,
	@Nombre varchar(max),
	@Descripcion varchar(max),
	@Precio decimal(18,2),
	@Stock int,
	@CodigoBarras varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	INSERT INTO [dbo].[Producto]
           ([Id]
           ,[IdSubCategoria]
           ,[Nombre]
           ,[Descripcion]
           ,[Precio]
           ,[Stock]
           ,[CodigoBarras])
     VALUES
           (@Id,
			@IdSubCategoria,
			@Nombre,
			@Descripcion,
			@Precio,
			@Stock,
			@CodigoBarras)
	SELECT @Id
	COMMIT TRANSACTION
END