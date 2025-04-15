namespace Beyond.Web.Server.Request
{
    /// <summary>
    /// AdItem Request
    /// </summary>
    public class AddItemRequest
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }
}
