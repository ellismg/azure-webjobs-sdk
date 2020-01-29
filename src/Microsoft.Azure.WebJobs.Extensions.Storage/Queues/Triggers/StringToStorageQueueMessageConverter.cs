// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal class StringToStorageQueueMessageConverter : IConverter<string, QueueMessage>
    {
        private readonly CloudQueue _queue;

        public StringToStorageQueueMessageConverter(CloudQueue queue)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }

            _queue = queue;
        }

        public QueueMessage Convert(string input)
        {
            return new QueueMessage(input);
        }
    }
}
