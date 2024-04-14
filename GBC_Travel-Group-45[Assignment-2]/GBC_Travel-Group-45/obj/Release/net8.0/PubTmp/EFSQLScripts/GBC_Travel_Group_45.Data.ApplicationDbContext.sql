IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [Flights] (
        [Id] int NOT NULL IDENTITY,
        [FlightNumber] nvarchar(max) NOT NULL,
        [Origin] nvarchar(max) NOT NULL,
        [Destination] nvarchar(max) NOT NULL,
        [DepartureTime] datetime2 NOT NULL,
        [ArrivalTime] datetime2 NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Flights] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [Guests] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [GovernmentId] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Guests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [Hotels] (
        [Id] int NOT NULL IDENTITY,
        [HotelName] nvarchar(max) NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        [SingleRoomsCount] int NOT NULL,
        [DoubleRoomsCount] int NOT NULL,
        [SingleRoomPrice] decimal(18,2) NOT NULL,
        [DoubleRoomPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Hotels] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [Passengers] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Passengers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [HotelBookings] (
        [Id] int NOT NULL IDENTITY,
        [HotelId] int NOT NULL,
        [GuestId] int NOT NULL,
        [BookingStart] datetime2 NOT NULL,
        [BookingEnd] datetime2 NOT NULL,
        CONSTRAINT [PK_HotelBookings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HotelBookings_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_HotelBookings_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [HotelImages] (
        [Id] int NOT NULL IDENTITY,
        [HotelId] int NOT NULL,
        [ImagePath] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_HotelImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HotelImages_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE TABLE [Bookings] (
        [Id] int NOT NULL IDENTITY,
        [FlightId] int NOT NULL,
        [PassengerId] int NOT NULL,
        [BookingDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Bookings_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Flights] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Bookings_Passengers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [Passengers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Bookings_FlightId] ON [Bookings] ([FlightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Bookings_PassengerId] ON [Bookings] ([PassengerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_HotelBookings_GuestId] ON [HotelBookings] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_HotelBookings_HotelId] ON [HotelBookings] ([HotelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_HotelImages_HotelId] ON [HotelImages] ([HotelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240218202943_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240218202943_InitialCreate', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219172456_HotelUpadate'
)
BEGIN
    ALTER TABLE [HotelBookings] ADD [NumberOfRooms] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219172456_HotelUpadate'
)
BEGIN
    ALTER TABLE [HotelBookings] ADD [RoomType] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219172456_HotelUpadate'
)
BEGIN
    ALTER TABLE [HotelBookings] ADD [TotalPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219172456_HotelUpadate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240219172456_HotelUpadate', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219173015_AddBookingDetails'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240219173015_AddBookingDetails', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219195256_FlightName'
)
BEGIN
    ALTER TABLE [Flights] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240219195256_FlightName'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240219195256_FlightName', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE TABLE [Cars] (
        [Id] int NOT NULL IDENTITY,
        [Make] nvarchar(max) NOT NULL,
        [Model] nvarchar(max) NOT NULL,
        [Year] int NOT NULL,
        [Color] nvarchar(max) NOT NULL,
        [SeatingCapacity] int NOT NULL,
        [PricePerDay] decimal(18,2) NOT NULL,
        [CarType] nvarchar(max) NOT NULL,
        [CarImage] varbinary(max) NOT NULL,
        CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [GovernmentId] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE TABLE [CarImages] (
        [Id] int NOT NULL IDENTITY,
        [CarId] int NOT NULL,
        [ImagePath] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_CarImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CarImages_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Cars] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE TABLE [CarBookings] (
        [Id] int NOT NULL IDENTITY,
        [CarId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [BookingStart] datetime2 NOT NULL,
        [BookingEnd] datetime2 NOT NULL,
        [PickupLocation] nvarchar(max) NOT NULL,
        [DropLocation] nvarchar(max) NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_CarBookings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CarBookings_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Cars] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CarBookings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE INDEX [IX_CarBookings_CarId] ON [CarBookings] ([CarId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE INDEX [IX_CarBookings_CustomerId] ON [CarBookings] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    CREATE INDEX [IX_CarImages_CarId] ON [CarImages] ([CarId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220195218_CarRentals'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240220195218_CarRentals', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220202923_CarRentals1'
)
BEGIN
    EXEC sp_rename N'[Cars].[SeatingCapacity]', N'NumOfSeats', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220202923_CarRentals1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240220202923_CarRentals1', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [CarBookings] DROP CONSTRAINT [FK_CarBookings_Cars_CarId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [CarImages] DROP CONSTRAINT [FK_CarImages_Cars_CarId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [Cars] DROP CONSTRAINT [PK_Cars];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cars]') AND [c].[name] = N'CarImage');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Cars] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Cars] DROP COLUMN [CarImage];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    EXEC sp_rename N'[Cars]', N'Car';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [Car] ADD CONSTRAINT [PK_Car] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [CarBookings] ADD CONSTRAINT [FK_CarBookings_Car_CarId] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    ALTER TABLE [CarImages] ADD CONSTRAINT [FK_CarImages_Car_CarId] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240220215518_Image'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240220215518_Image', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarImages] DROP CONSTRAINT [FK_CarImages_Car_CarId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarImages] DROP CONSTRAINT [PK_CarImages];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    EXEC sp_rename N'[CarImages]', N'CarImage';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    EXEC sp_rename N'[CarImage].[IX_CarImages_CarId]', N'IX_CarImage_CarId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarBookings] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarBookings] ADD [PhoneNumber] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarImage] ADD CONSTRAINT [PK_CarImage] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    ALTER TABLE [CarImage] ADD CONSTRAINT [FK_CarImage_Car_CarId] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221013007_BookCar'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240221013007_BookCar', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    ALTER TABLE [CarBookings] DROP CONSTRAINT [FK_CarBookings_Customers_CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    ALTER TABLE [Customers] DROP CONSTRAINT [PK_Customers];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    EXEC sp_rename N'[Customers]', N'Customer';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    ALTER TABLE [Customer] ADD CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    ALTER TABLE [CarBookings] ADD CONSTRAINT [FK_CarBookings_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221031034_CarBooking'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240221031034_CarBooking', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBookings] DROP CONSTRAINT [FK_CarBookings_Car_CarId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBookings] DROP CONSTRAINT [FK_CarBookings_Customer_CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBookings] DROP CONSTRAINT [PK_CarBookings];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer]') AND [c].[name] = N'Email');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Customer] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Customer] DROP COLUMN [Email];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer]') AND [c].[name] = N'PhoneNumber');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Customer] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Customer] DROP COLUMN [PhoneNumber];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    EXEC sp_rename N'[CarBookings]', N'CarBooking';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    EXEC sp_rename N'[CarBooking].[IX_CarBookings_CustomerId]', N'IX_CarBooking_CustomerId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    EXEC sp_rename N'[CarBooking].[IX_CarBookings_CarId]', N'IX_CarBooking_CarId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBooking] ADD CONSTRAINT [PK_CarBooking] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBooking] ADD CONSTRAINT [FK_CarBooking_Car_CarId] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    ALTER TABLE [CarBooking] ADD CONSTRAINT [FK_CarBooking_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240221052047_CarBooking5'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240221052047_CarBooking5', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320165718_Travel_Management'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240320165718_Travel_Management', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240320171156_IdentityTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240320171156_IdentityTables', N'8.0.3');
END;
GO

COMMIT;
GO

