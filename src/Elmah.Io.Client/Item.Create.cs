namespace Elmah.Io.Client
{
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
