using Elmah.Io.Client.Models;
using System.Linq;

namespace Elmah.Io.Client
{
    public static class CreateMessageExtensions
    {
        public static CreateMessage ObfuscatePasswords(this CreateMessage message, bool obfuscatePasswords = true)
        {
            if (message == null || message.Form == null) return message;
            foreach (var f in message.Form.Where(f => f.Key.ToLower().Contains("password") && !string.IsNullOrWhiteSpace(f.Value)))
            {
                f.Value = new string('*', f.Value.Length);
            }
            return message;
        }
    }
}
