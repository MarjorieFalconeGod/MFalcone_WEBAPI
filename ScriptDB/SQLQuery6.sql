CREATE TABLE [M-Falcone_Usuarios] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombres] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Username] NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    [Rol] NVARCHAR(50) NOT NULL,
    [Estado] NVARCHAR(20) NOT NULL
);

-- Create M-Falcone_Solicitudes table
CREATE TABLE [M-Falcone_Solicitudes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Tipo] NVARCHAR(50) NOT NULL,
    [DescripcionSolicitud] NVARCHAR(255) NOT NULL,
    [Justificativo] NVARCHAR(MAX),
    [Estado] NVARCHAR(50) NOT NULL,
    [DetalleGestion] NVARCHAR(255),
    [FechaIngreso] DATETIME NOT NULL DEFAULT (GETDATE()),
    [FechaActualizacion] DATETIME NOT NULL DEFAULT (GETDATE()),
    [FechaGestion] DATETIME,
    [UsuarioCreadorId] INT NOT NULL,
    [UsuarioGestorId] INT,
    CONSTRAINT [FK_Solicitud_UsuarioCreador] FOREIGN KEY ([UsuarioCreadorId]) REFERENCES [M-Falcone_Usuarios]([Id]),
    CONSTRAINT [FK_Solicitud_UsuarioGestor] FOREIGN KEY ([UsuarioGestorId]) REFERENCES [M-Falcone_Usuarios]([Id])
);

INSERT INTO [M-Falcone_Usuarios] ([Nombres], [Email], [Username], [Password], [Rol], [Estado])
VALUES
('Juan Pérez', 'juan.perez@example.com', 'juanperez', 'Password123', 'Admin', 'Activo'),
('Ana Gómez', 'ana.gomez@example.com', 'anagomez', 'Password123', 'Usuario', 'Activo'),
('Carlos Ramírez', 'carlos.ramirez@example.com', 'carlosram', 'Password123', 'Usuario', 'Activo'),
('Lucía Torres', 'lucia.torres@example.com', 'luciatorres', 'Password123', 'Usuario', 'Inactivo'),
('Pedro Martínez', 'pedro.martinez@example.com', 'pedrom', 'Password123', 'Gestor', 'Activo'),
('María López', 'maria.lopez@example.com', 'mlopez', 'Password123', 'Usuario', 'Activo'),
('Jorge Fernández', 'jorge.fernandez@example.com', 'jorgef', 'Password123', 'Gestor', 'Inactivo'),
('Sofía Sánchez', 'sofia.sanchez@example.com', 'sofias', 'Password123', 'Admin', 'Activo'),
('David Morales', 'david.morales@example.com', 'dmorales', 'Password123', 'Usuario', 'Activo'),
('Elena Rojas', 'elena.rojas@example.com', 'elenar', 'Password123', 'Usuario', 'Inactivo');


INSERT INTO [M-Falcone_Solicitudes] ([Tipo], [DescripcionSolicitud], [Justificativo], [Estado], [DetalleGestion], [FechaIngreso], [FechaActualizacion], [FechaGestion], [UsuarioCreadorId], [UsuarioGestorId])
VALUES
('Soporte', 'Problema con el acceso a la plataforma', 'Necesito acceso para realizar mis tareas diarias', 'Pendiente', NULL, GETDATE(), GETDATE(), NULL, 1, 5),
('Mejora', 'Actualización de software', 'La versión actual tiene varios errores', 'En Proceso', 'Revisión en curso', GETDATE(), GETDATE(), NULL, 2, 7),
('Solicitud', 'Cambio de contraseña', 'La contraseña actual no es segura', 'Resuelto', 'Contraseña actualizada', GETDATE(), GETDATE(), GETDATE(), 3, 5),
('Reclamo', 'Mal funcionamiento del sistema', 'El sistema se cae frecuentemente', 'Pendiente', NULL, GETDATE(), GETDATE(), NULL, 4, 8),
('Soporte', 'Problemas con la carga de datos', 'No puedo cargar datos en la plataforma', 'En Proceso', 'Revisando problema de carga', GETDATE(), GETDATE(), NULL, 5, 6),
('Solicitud', 'Crear nuevo usuario', 'Necesitamos agregar a un nuevo colaborador', 'Resuelto', 'Usuario creado', GETDATE(), GETDATE(), GETDATE(), 6, 1),
('Mejora', 'Añadir funcionalidad de informes', 'Es necesario agregar informes mensuales', 'Pendiente', NULL, GETDATE(), GETDATE(), NULL, 7, 2),
('Reclamo', 'Fallas en la autenticación', 'No puedo autenticarme en la plataforma', 'Resuelto', 'Problema de autenticación solucionado', GETDATE(), GETDATE(), GETDATE(), 8, 9),
('Soporte', 'Errores en la base de datos', 'Fallas al ejecutar consultas en la base', 'En Proceso', 'Consultando con el equipo técnico', GETDATE(), GETDATE(), NULL, 9, 4),
('Solicitud', 'Cambio de rol de usuario', 'Necesito permisos de administrador', 'Pendiente', NULL, GETDATE(), GETDATE(), NULL, 10, 3);


CREATE PROCEDURE "M-Falcone_ActualizarSolicitud"
@Id INT,
@Tipo NVARCHAR(50),
@DescripcionSolicitud NVARCHAR(255),
@Justificativo NVARCHAR(MAX),
@Estado NVARCHAR(50),
@DetalleGestion NVARCHAR(255),
@FechaIngreso DATETIME,
@UsuarioCreadorId INT,
@UsuarioGestorId INT
AS
BEGIN
    UPDATE [M-Falcone_Solicitudes]
    SET Tipo = @Tipo,
        DescripcionSolicitud = @DescripcionSolicitud,
        Justificativo = @Justificativo,
        Estado = @Estado,
        DetalleGestion = @DetalleGestion,
        FechaIngreso = @FechaIngreso,
        FechaActualizacion = GETDATE(),
        FechaGestion = CASE WHEN @Estado = 'ATENDIDA' OR @Estado = 'NO RESUELTA' THEN GETDATE() ELSE NULL END,
        UsuarioCreadorId = @UsuarioCreadorId,
        UsuarioGestorId = @UsuarioGestorId
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0
    BEGIN
        RETURN -1; -- Solicitud no encontrada
    END

    RETURN 0; -- Actualización exitosa
END;


CREATE PROCEDURE "M-Falcone_ConsultarSolicitud"
@Id INT
AS
BEGIN
    SELECT * 
    FROM [M-Falcone_Solicitudes]
    WHERE Id = @Id;
END;

CREATE PROCEDURE "M-Falcone_ConsultarSolicitudes"
@IdUsuario INT = NULL,
@FechaIngreso DATETIME = NULL,
@Estado NVARCHAR(50) = NULL
AS
BEGIN
    SELECT * 
    FROM [M-Falcone_Solicitudes]
    WHERE (@IdUsuario IS NULL OR UsuarioCreadorId = @IdUsuario)
      AND (@FechaIngreso IS NULL OR CAST(FechaIngreso AS DATE) = CAST(@FechaIngreso AS DATE))
      AND (@Estado IS NULL OR Estado = @Estado);
END;

CREATE PROCEDURE "M-Falcone_CrearSolicitud"
@Tipo NVARCHAR(50),
@DescripcionSolicitud NVARCHAR(255),
@Justificativo NVARCHAR(MAX),
@UsuarioCreadorId INT
AS
BEGIN
    INSERT INTO [M-Falcone_Solicitudes]
    (Tipo, DescripcionSolicitud, Justificativo, Estado, DetalleGestion, FechaIngreso, FechaActualizacion, UsuarioCreadorId)
    VALUES (@Tipo, @DescripcionSolicitud, @Justificativo, 'INGRESADA', NULL, GETDATE(), GETDATE(), @UsuarioCreadorId);

    RETURN SCOPE_IDENTITY(); -- Devolver el ID de la nueva solicitud
END;

CREATE PROCEDURE "M-Falcone_ActualizarEstadoSolicitud"
@Id INT,
@Estado NVARCHAR(50)
AS
BEGIN
    UPDATE [M-Falcone_Solicitudes]
    SET Estado = @Estado,
        FechaGestion = CASE WHEN @Estado = 'ATENDIDA' OR @Estado = 'NO RESUELTA' THEN GETDATE() ELSE NULL END,
        FechaActualizacion = GETDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0
    BEGIN
        RETURN -1; -- Solicitud no encontrada
    END

    RETURN 0; -- Estado actualizado correctamente
END;

CREATE PROCEDURE "M-Falcone_CrearUsuario"
    @Username NVARCHAR(50),
    @Nombres NVARCHAR(100),
    @Email NVARCHAR(100),
    @Estado NVARCHAR(20),
    @Password NVARCHAR(100),
    @Rol NVARCHAR(50)
AS
BEGIN
    INSERT INTO [M-Falcone_Usuarios] (Username, Nombres, Email, Estado, Password, Rol)
    VALUES (@Username, @Nombres, @Email, @Estado, @Password, @Rol);

    SELECT * FROM [M-Falcone_Usuarios] WHERE Id = SCOPE_IDENTITY();
END;

CREATE PROCEDURE "M-Falcone_Login"
    @Username NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT * 
    FROM [M-Falcone_Usuarios]
    WHERE Username = @Username AND Password = @Password;
END;
