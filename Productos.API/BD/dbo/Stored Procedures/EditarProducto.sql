CREATE PROCEDURE EditarProducto
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
	BEGIN TRANSACTION
    -- Insert statements for procedure here
	UPDATE [dbo].[Producto]
   SET [Id] = @Id
      ,[IdSubCategoria] = @IdSubCategoria
      ,[Nombre] = @Nombre
      ,[Descripcion] = @Descripcion
      ,[Precio] = @Precio
      ,[Stock] = @Stock
      ,[CodigoBarras] = @CodigoBarras
 WHERE Id=@Id
 SELECT @Id
 COMMIT TRANSACTION
END