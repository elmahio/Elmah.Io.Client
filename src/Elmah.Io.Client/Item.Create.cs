// Don't use primary constructor here since that will cause properties not to be correctly set on partial class
#pragma warning disable IDE0290 // Use primary constructor
namespace Elmah.Io.Client
{
    /// <summary>
    /// Create a new item with the specified key and value.
    /// </summary>
    public partial class Item
    {
        /// <summary>
        /// Create a new item with the specified key and value.
        /// </summary>
        public Item(string key = default, string value = default)
        {
            Key = key;
            Value = value;
        }
    }
}
#pragma warning restore IDE0290 // Use primary constructor
