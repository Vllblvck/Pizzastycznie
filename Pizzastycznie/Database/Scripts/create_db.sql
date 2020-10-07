CREATE DATABASE IF NOT EXISTS PizzastycznieDb;
USE PizzastycznieDb;

CREATE TABLE IF NOT EXISTS Users
(
    id            BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    email         VARCHAR(150) UNIQUE NOT NULL,
    name          NVARCHAR(32)        NOT NULL,
    password_hash VARCHAR(150)        NOT NULL,
    salt VARCHAR(150) NOT NULL,
    address       NVARCHAR(120),
    phone_number  VARCHAR(15),
    admin         BOOL                NOT NULL
);

CREATE TABLE IF NOT EXISTS Food
(
    id        BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    food_type INT                  NOT NULL,
    food_name NVARCHAR(100) UNIQUE NOT NULL,
    price     DECIMAL(4, 2)        NOT NULL
);

CREATE TABLE IF NOT EXISTS FoodAdditives
(
    id            BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    food_id       BIGINT UNSIGNED      NOT NULL,
    additive_name NVARCHAR(100) UNIQUE NOT NULL,
    price         DECIMAL(4, 2)        NOT NULL,
    CONSTRAINT fk_food_id FOREIGN KEY (food_id) REFERENCES Food (id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Orders
(
    id               BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id          BIGINT UNSIGNED NOT NULL,
    order_comments   NVARCHAR(150),
    order_date       DATETIME        NOT NULL,
    order_status     NVARCHAR(15)    NOT NULL,
    customer_phone   VARCHAR(15)     NOT NULL,
    delivery_address NVARCHAR(120)   NOT NULL,
    payment_method   INT             NOT NULL,
    total_price      DECIMAL(4, 2)   NOT NULL,
    self_pickup      BOOL            NOT NULL,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES Users (id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS OrderFood
(
    id       BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    order_id BIGINT UNSIGNED NOT NULL,
    food_id  BIGINT UNSIGNED NOT NULL,
    amount   INT             NOT NULL,
    CONSTRAINT fk_food_order FOREIGN KEY (order_id) REFERENCES Orders (id) ON DELETE CASCADE,
    CONSTRAINT fk_food_details FOREIGN KEY (food_id) REFERENCES Food (id)
);

CREATE TABLE IF NOT EXISTS OrderAdditives
(
    id          BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    order_id    BIGINT UNSIGNED NOT NULL,
    additive_id BIGINT UNSIGNED NOT NULL,
    amount      INT             NOT NULL,
    CONSTRAINT fk_additive_order FOREIGN KEY (order_id) REFERENCES Orders (id) ON DELETE CASCADE,
    CONSTRAINT fk_additive_details FOREIGN KEY (additive_id) REFERENCES FoodAdditives (id)
);