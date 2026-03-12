CREATE PROCEDURE ObtenerProducto
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.Id,P.IdSubCategoria,P.Nombre,P.Descripcion, P.Precio,P.Stock,P.CodigoBarras,SC.Nombre AS SubCategoria,C.Nombre AS Categoria
	FROM Producto P 
	INNER JOIN SubCategorias SC  ON P.IdSubCategoria = SC.Id
	INNER JOIN Categorias C ON SC.IdCategoria = C.Id
	WHERE (P.Id = @Id)
END