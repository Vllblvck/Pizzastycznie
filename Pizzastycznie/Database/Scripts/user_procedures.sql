CREATE PROCEDURE InsertUser(Email VARCHAR(150), Name NVARCHAR(32), PasswordHash VARCHAR(150), Salt VARCHAR(150),
                            Address NVARCHAR(120), PhoneNumber VARCHAR(15), Admin BOOL)
BEGIN
    INSERT INTO Users (email, name, password_hash, salt, address, phone_number, admin)
    VALUES (Email, Name, PasswordHash, Salt, Address, PhoneNumber, Admin);
END;

CREATE PROCEDURE SelectUser (Email VARCHAR(150))
BEGIN
    SELECT * FROM Users WHERE email = Email;
END;

CREATE PROCEDURE SelectHashAndSalt (Email VARCHAR(150))
BEGIN
    SELECT password_hash, salt FROM Users WHERE email = Email;
END;