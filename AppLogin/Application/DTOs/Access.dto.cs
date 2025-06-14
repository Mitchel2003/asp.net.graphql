namespace AppLogin.Application.DTOs
{
    // Inputs
    public record LoginInput(string Email, string Password);
    public record RegisterInput(string Email, string Username, string Password, string ConfirmPassword);

    /** to queries */
    public class IGetAll
    {
        public string Model { get; set; } = string.Empty;
    }
    public class IGetById
    {
        public string Model { get; set; } = string.Empty;
        public object Id { get; set; } = null!;
    }
    public class IGetByFilter
    {
        public string Model { get; set; } = string.Empty;
        public Dictionary<string, object>? Filter { get; set; }
    }

    /** to mutations */
    public class ICreate
    {
        public Dictionary<string, object> Data { get; set; } = new(); // Fields
        public string Model { get; set; } = string.Empty; //"User", "Product", etc.
    }
    public class IUpdate
    {
        public object Id { get; set; } = null!;
        public Dictionary<string, object> Data { get; set; } = new(); // Fields to update
        public string Model { get; set; } = string.Empty; //"User", "Product", etc.
    }
    public class IDelete
    {
        public object Id { get; set; } = null!; //ID element to delete, e.g., User ID etc.
        public string Model { get; set; } = string.Empty; //"User", "Product", etc.
    }
}
