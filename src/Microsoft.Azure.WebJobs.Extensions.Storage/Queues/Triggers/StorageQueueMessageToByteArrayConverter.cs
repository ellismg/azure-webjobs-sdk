﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Azure.Storage.Queues;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal class StorageQueueMessageToByteArrayConverter : IConverter<CloudQueueMessage, byte[]>
    {
        public byte[] Convert(CloudQueueMessage input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.AsBytes;
        }
    }
}
