CREATE PROCEDURE ObtenerSubCategorias
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT SC.Id, SC.Nombre, SC.IdCategoria, C.Nombre AS NombreCategoria
    FROM dbo.SubCategorias SC
    INNER JOIN dbo.Categorias C ON SC.IdCategoria = C.Id
END