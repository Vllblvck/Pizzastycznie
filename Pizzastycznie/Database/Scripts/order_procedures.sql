CREATE PROCEDURE InsertOrder(UserId BIGINT, OrderComments NVARCHAR(150), StatusDate DATETIME, OrderStatus NVARCHAR(15),
                             CustomerPhone VARCHAR(15), DeliveryAddress NVARCHAR(120), PaymentMethod INT,
                             TotalPrice DECIMAL(4, 2), SelfPickup BOOL)
BEGIN
    INSERT INTO Orders (user_id, order_comments, status_date, order_status, customer_phone, delivery_address,
                        payment_method, total_price, self_pickup)
    VALUES (UserId, OrderComments, StatusDate, OrderStatus, CustomerPhone, DeliveryAddress, PaymentMethod, TotalPrice,
            SelfPickup);
END;

CREATE PROCEDURE InsertOrderFood(OrderId BIGINT, FoodName NVARCHAR(100), Amount INT)
BEGIN
    SET @FoodId = (SELECT id FROM Food WHERE food_name = FoodName);

    INSERT INTO OrderFood(order_id, food_id, amount)
    VALUEs (OrderId, @FoodId, Amount);
END;

CREATE PROCEDURE InsertOrderAdditive(OrderId BIGINT, AdditiveName NVARCHAR(100), Amount INT)
BEGIN
    SET @AdditiveId = (SELECT id FROM FoodAdditives WHERE additive_name = AdditiveName);

    INSERT INTO OrderAdditives(order_id, additive_id, amount)
    VALUES (OrderID, @AdditiveId, Amount);
END;

CREATE PROCEDURE SelectLastInsertId()
BEGIN
    SELECT LAST_INSERT_ID();
END;

CREATE PROCEDURE SelectOrdersForUser(Email VARCHAR(150))
BEGIN
    SET @UserId = (SELECT id FROM Users WHERE email = Email);

    SELECT id,
           order_comments,
           status_date,
           order_status,
           customer_phone,
           delivery_address,
           payment_method,
           total_price,
           self_pickup
    FROM Orders
    WHERE user_id = @UserId;
END;

CREATE PROCEDURE SelectOrderContent(OrderId BIGINT)
BEGIN
    SELECT OrderFood.amount, Food.food_name
    FROM OrderFood
             JOIN Food ON OrderFood.food_id = Food.id
    WHERE OrderFood.order_id = OrderId;

    SELECT OrderAdditives.amount, FoodAdditives.additive_name
    FROM OrderAdditives
             JOIN FoodAdditives ON OrderAdditives.additive_id = FoodAdditives.id
    WHERE OrderAdditives.order_id = OrderId;
END;

CREATE PROCEDURE SelectPendingOrders()
BEGIN
    SELECT id,
           order_comments,
           status_date,
           order_status,
           customer_phone,
           delivery_address,
           payment_method,
           total_price,
           self_pickup
    FROM Orders
    WHERE user_id = @UserId;
END;