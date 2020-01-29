// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs.Host.Converters;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal class StorageQueueMessageToStringConverter : IConverter<CloudQueueMessage, string>
    {
        public string Convert(CloudQueueMessage input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.AsString;
        }
    }
}
