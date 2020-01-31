// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal class StorageQueueMessageToByteArrayConverter : IConverter<QueueMessage, byte[]>
    {
        public byte[] Convert(QueueMessage input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.AsBytes();
        }
    }
}
