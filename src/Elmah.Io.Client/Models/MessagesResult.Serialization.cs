// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Elmah.Io.Client.Models
{
    public partial class MessagesResult
    {
        internal static MessagesResult DeserializeMessagesResult(JsonElement element)
        {
            Optional<IReadOnlyList<MessageOverview>> messages = default;
            Optional<int> total = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("messages"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<MessageOverview> array = new List<MessageOverview>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(MessageOverview.DeserializeMessageOverview(item));
                    }
                    messages = array;
                    continue;
                }
                if (property.NameEquals("total"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    total = property.Value.GetInt32();
                    continue;
                }
            }
            return new MessagesResult(Optional.ToList(messages), Optional.ToNullable(total));
        }
    }
}
