// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs.Extensions.Storage.Queues;

namespace Microsoft.Azure.WebJobs.Host.Queues
{
    internal static class StorageQueueExtensions
    {
        public static async Task<Response<SendReceipt>> AddMessageAndCreateIfNotExistsAsync(this QueueClient queue,
            QueueMessage message, CancellationToken cancellationToken)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }

            bool isQueueNotFoundException = false;

            try
            {
                return await queue.SendMessageAsync(message.AsString, cancellationToken);
            }
            catch (RequestFailedException exception) when (exception.IsNotFoundQueueNotFound())
            {
                isQueueNotFoundException = true;
            }

            Debug.Assert(isQueueNotFoundException);
            await queue.CreateIfNotExistsAsync(cancellationToken);
            return await queue.SendMessageAsync(message.AsString, cancellationToken);
        }
    }
}
