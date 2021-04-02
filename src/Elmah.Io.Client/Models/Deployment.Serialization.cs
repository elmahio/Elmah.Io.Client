// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Elmah.Io.Client.Models
{
    public partial class Deployment
    {
        internal static Deployment DeserializeDeployment(JsonElement element)
        {
            Optional<string> id = default;
            Optional<string> version = default;
            Optional<DateTimeOffset> created = default;
            Optional<string> createdBy = default;
            Optional<string> description = default;
            Optional<string> userName = default;
            Optional<string> userEmail = default;
            Optional<string> logId = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("version"))
                {
                    version = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("created"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    created = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("createdBy"))
                {
                    createdBy = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("description"))
                {
                    description = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("userName"))
                {
                    userName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("userEmail"))
                {
                    userEmail = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("logId"))
                {
                    logId = property.Value.GetString();
                    continue;
                }
            }
            return new Deployment(id.Value, version.Value, Optional.ToNullable(created), createdBy.Value, description.Value, userName.Value, userEmail.Value, logId.Value);
        }
    }
}
