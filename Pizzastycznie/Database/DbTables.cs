namespace Pizzastycznie.Database
{
    public static class DbTables
    {
        public const string LastInsertId = "LAST_INSERT_ID()";

        public static class Food
        {
            public const string FoodName = "food_name";
            public const string FoodType = "food_type";
            public const string Price = "price";
        }

        public static class FoodAdditives
        {
            public const string AdditiveName = "additive_name";
            public const string Price = "price";
        }

        public static class Orders
        {
            public const string OrderId = "id";
            public const string StatusDate = "status_date";
            public const string OrderStatus = "order_status";
            public const string CustomerPhone = "customer_phone";
            public const string DeliveryAddress = "delivery_address";
            public const string PaymentMethod = "payment_method";
            public const string TotalPrice = "total_price";
            public const string SelfPickup = "self_pickup";
            public const string OrderComments = "order_comments";
        }

        public static class OrderFood
        {
            public const string FoodName = "food_name";
            public const string Amount = "amount";
        }

        public static class OrderAdditives
        {
            public const string AdditiveName = "additive_name";
            public const string Amount = "amount";
        }

        public static class Users
        {
            public const string Id = "id";
            public const string Email = "email";
            public const string Name = "name";
            public const string Admin = "admin";
            public const string Address = "address";
            public const string PhoneNumber = "phone_number";
            public const string PasswordHash = "password_hash";
            public const string Salt = "salt";
        }
    }
}