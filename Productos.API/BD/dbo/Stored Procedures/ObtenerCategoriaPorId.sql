CREATE PROCEDURE ObtenerCategoriaPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Nombre
    FROM dbo.Categorias
    WHERE Id = @Id;
END