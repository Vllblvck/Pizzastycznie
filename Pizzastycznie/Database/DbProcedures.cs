namespace Pizzastycznie.Database
{
    public static class DbProcedures
    {
        public static class SelectLastInsertId
        {
            public const string ProcedureName = "SelectLastInsertId";
        }

        public static class InsertFood
        {
            public const string ProcedureName = "InsertFood";

            public static class Parameters
            {
                public const string FoodName = "Name";
                public const string FoodType = "Type";
                public const string FoodPrice = "Price";
            }
        }

        public static class InsertFoodAdditive
        {
            public const string ProcedureName = "InsertFoodAdditive";

            public static class Parameters
            {
                public const string FoodName = "FoodName";
                public const string AdditiveName = "AdditiveName";
                public const string AdditivePrice = "Price";
            }
        }

        public static class SelectFood
        {
            public const string ProcedureName = "SelectFood";

            public static class Parameters
            {
                public const string FoodName = "FoodName";
            }
        }

        public static class SelectAdditiveForFood
        {
            public const string ProcedureName = "SelectAdditiveForFood";

            public static class Parameters
            {
                public const string FoodName = "FoodName";
            }
        }

        public static class SelectAllFood
        {
            public const string ProcedureName = "SelectAllFood";
        }

        public static class DeleteFood
        {
            public const string ProcedureName = "DeleteFood";

            public static class Parameters
            {
                public const string FoodName = "FoodName";
            }
        }

        public static class InsertOrder
        {
            public const string ProcedureName = "InsertOrder";

            public static class Parameters
            {
                public const string UserId = "UserId";
                public const string OrderComments = "OrderComments";
                public const string StatusDate = "StatusDate";
                public const string OrderStatus = "OrderStatus";
                public const string CustomerPhone = "CustomerPhone";
                public const string DeliveryAddress = "DeliveryAddress";
                public const string PaymentMethod = "PaymentMethod";
                public const string TotalPrice = "TotalPrice";
                public const string SelfPickup = "SelfPickup";
            }
        }

        public static class InsertOrderFood
        {
            public const string ProcedureName = "InsertOrderFood";

            public static class Parameters
            {
                public const string OrderId = "OrderId";
                public const string FoodName = "FoodName";
                public const string Amount = "Amount";
            }
        }

        public static class InsertOrderAdditive
        {
            public const string ProcedureName = "InsertOrderAdditive";

            public static class Parameters
            {
                public const string OrderId = "OrderId";
                public const string AdditiveName = "AdditiveName";
                public const string Amount = "Amount";
            }
        }

        public static class SelectOrdersForUser
        {
            public const string ProcedureName = "SelectOrdersForUser";

            public static class Parameters
            {
                public const string Email = "Email";
            }
        }

        public static class SelectOrderContent
        {
            public const string ProcedureName = "SelectOrderContent";

            public static class Parameters
            {
                public const string OrderId = "OrderId";
            }
        }

        public static class SelectPendingOrders
        {
            public const string ProcedureName = "SelectPendingOrders";
        }

        public static class UpdateOrderStatus
        {
            public const string ProcedureName = "UpdateOrderStatus";

            public static class Parameters
            {
                public const string OrderId = "OrderId";
                public const string OrderStatus = "OrderStatus";
            }
        }

        public static class InsertUser
        {
            public const string ProcedureName = "InsertUser";

            public static class Parameters
            {
                public const string Email = "Email";
                public const string Name = "Name";
                public const string PasswordHash = "PasswordHash";
                public const string Salt = "Salt";
                public const string Address = "Address";
                public const string PhoneNumber = "PhoneNumber";
                public const string Admin = "Admin";
            }
        }

        public static class SelectUser
        {
            public const string ProcedureName = "SelectUser";

            public static class Parameters
            {
                public const string UserEmail = "UserEmail";
            }
        }

        public static class SelectHashAndSalt
        {
            public const string ProcedureName = "SelectHashAndSalt";

            public static class Parameters
            {
                public const string Email = "Email";
            }
        }
    }
}