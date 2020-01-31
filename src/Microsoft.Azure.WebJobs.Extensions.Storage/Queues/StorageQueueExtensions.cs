// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Net;
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
        public static async Task<bool> ExistsAsync(this QueueClient queue)
        {
            // Ideally, the v12 SDK would just have an ExistsAsync method like previous versions.  In the meantime we can
            // do what the previous SDK's implementation did, which was to try to fetch the queue's properties and use the
            // status of the operation to decide if the queue exists or not.
            // (ref: https://github.com/Azure/azure-storage-net/blob/v11.0.0/Lib/ClassLibraryCommon/Queue/CloudQueue.cs#L2357-L2380)
            // (ref: https://github.com/Azure/azure-sdk-for-net/issues/9752)
            try
            {
                await queue.GetPropertiesAsync();
                return true;
            }
            catch (RequestFailedException e) when (e.Status == 404 /* NotFound */)
            {
                return false;
            }
            catch (RequestFailedException e) when (e.Status == 412 /* Precondition Failed */)
            {
                return true;
            }
        }

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
                return await queue.SendMessageAsync(message.AsString(), cancellationToken);
            }
            catch (RequestFailedException exception) when (exception.IsNotFoundQueueNotFound())
            {
                isQueueNotFoundException = true;
            }

            Debug.Assert(isQueueNotFoundException);
            await queue.CreateIfNotExistsAsync(cancellationToken);
            return await queue.SendMessageAsync(message.AsString(), cancellationToken);
        }
    }
}
