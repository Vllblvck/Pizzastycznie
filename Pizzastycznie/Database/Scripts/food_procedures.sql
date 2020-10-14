CREATE PROCEDURE InsertFood(Name NVARCHAR(100), Type INT, Price DECIMAL(4, 2))
BEGIN
    INSERT INTO Food (food_name, food_type, price)
    VALUES (Name, Type, Price);
END;

CREATE PROCEDURE InsertFoodAdditive(FoodName NVARCHAR(100), AdditiveName NVARCHAR(100), Price DECIMAL(4, 2))
BEGIN
    SET @food_id = (SELECT id FROM Food WHERE food_name = FoodName);

    INSERT INTO FoodAdditives(additive_name, price, food_id)
    VALUES (AdditiveName, Price, @food_id);
END;

CREATE PROCEDURE SelectFood(FoodName NVARCHAR(100))
BEGIN
    SELECT * FROM Food WHERE food_name = FoodName;
END;

CREATE PROCEDURE SelectAllFood()
BEGIN
    SELECT * FROM Food;
END;

CREATE PROCEDURE SelectAdditiveForFood(FoodName NVARCHAR(100))
BEGIN
    SET @FoodId = (SELECT id FROM Food WHERE food_name = FoodName);
    SELECT additive_name, price FROM FoodAdditives WHERE food_id = @FoodId;
END;

CREATE PROCEDURE DeleteFood(FoodName NVARCHAR(100))
BEGIN
    DELETE FROM Food WHERE food_name = FoodName;
END;