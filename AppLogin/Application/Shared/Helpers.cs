namespace AppLogin.Application.Shared
{
    public static class Helpers
    {
        /**
         * Helper method to get the model type by name.
         * Let's assume the models are in Models namespace.
         * Examples: User, Product, Order; system dinamic.
         */
        public static Type GetModelType(string model)
        {
            var type = Type.GetType($"AppLogin.Models.{model}", throwOnError: false, ignoreCase: true)
                ?? throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage($"Model '{model}' not found.")
                    .SetCode("MODEL_NOT_FOUND")
                    .Build());
            return type;
        }
    }
}