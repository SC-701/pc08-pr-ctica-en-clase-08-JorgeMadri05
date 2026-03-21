
CREATE PROCEDURE ObtenerSubCategoriaPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT SC.Id, SC.Nombre, SC.IdCategoria, C.Nombre AS NombreCategoria
    FROM dbo.SubCategorias SC
    INNER JOIN dbo.Categorias C ON SC.IdCategoria = C.Id
    WHERE SC.IdCategoria = @Id
END