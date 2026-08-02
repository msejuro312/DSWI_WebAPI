DROP DATABASE IF EXISTS DB_BIBLIOTECA
GO

CREATE DATABASE DB_BIBLIOTECA
GO

use DB_BIBLIOTECA

CREATE TABLE Autor
(
    AutorId INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(50)
);

CREATE TABLE Libro
(
    LibroId INT IDENTITY PRIMARY KEY,
    AutorId INT references Autor(AutorId),
    Titulo NVARCHAR(150) NOT NULL,
    ISBN VARCHAR(20) UNIQUE,
    AnioPublicacion INT
);


INSERT INTO Autor (Nombre, Nacionalidad)
VALUES
('Gabriel García Márquez', 'Colombia'),
('Mario Vargas Llosa', 'Perú');


INSERT INTO Libro (AutorId, Titulo, ISBN, AnioPublicacion)
VALUES
(1, 'Cien años de soledad', '9780307474728', 1967),
(2, 'La ciudad y los perros', '9788420471839', 1963);

SELECT * FROM Autor

create procedure sp_list_autores
as
begin
    select
    AutorId,
    Nombre
    from Autor
end

exec sp_list_autores

create procedure sp_list_libros
as
begin
    select
    l.LibroId,
    l.Titulo, 
    l.ISBN, 
    l.AnioPublicacion,
    a.Nombre 'Autor'
    from Libro l
    join Autor a on l.AutorId = a.AutorId
end

create procedure sp_find_libro_by_id
@LibroId int
as
begin
    select
    l.LibroId,
    l.Titulo, 
    l.ISBN, 
    l.AnioPublicacion,
    a.Nombre 'Autor'
    from Libro l
    join Autor a on l.AutorId = a.AutorId
    where l.LibroId = @LibroId
end


create procedure sp_insert_libro
@AutorId INT,
@Titulo NVARCHAR(150),
@ISBN VARCHAR(20),
@AnioPublicacion INT
as
begin
    insert Libro(AutorId, Titulo, ISBN, AnioPublicacion)
    values(@AutorId, @Titulo, @ISBN, @AnioPublicacion)
end

create or alter procedure sp_update_libro
@LibroId INT,
@AutorId INT,
@Titulo NVARCHAR(150),
@ISBN VARCHAR(20),
@AnioPublicacion INT
as
begin
	update Libro set 
	AutorId = @AutorId,
	Titulo = @Titulo, 
	ISBN = @ISBN,
	AnioPublicacion = @AnioPublicacion
    where LibroId = @LibroId
end

create or alter procedure sp_delete_libro
@LibroId INT
as
begin
	delete from Libro	
    where LibroId = @LibroId
end