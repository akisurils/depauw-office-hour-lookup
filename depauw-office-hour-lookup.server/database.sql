CREATE DATABASE test_db;

CREATE TABLE AspNetUsers (
    Id TEXT PRIMARY KEY,
    FullName TEXT,
    UserName VARCHAR(256),
    NormalizedUserName VARCHAR(256),
    Email VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed BOOLEAN,
    PasswordHash TEXT,
    SecurityStamp TEXT,
    ConcurrencyStamp TEXT,
    PhoneNumber TEXT,
    PhoneNumberConfirmed BOOLEAN,
    TwoFactorEnabled BOOLEAN,
    LockoutEnd TIMESTAMP WITH TIME ZONE,
    LockoutEnabled BOOLEAN,
    AccessFailedCount INTEGER
);

GRANT ALL PRIVILEGES ON DATABASE test_db TO postgres;
-- GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO postgres;

