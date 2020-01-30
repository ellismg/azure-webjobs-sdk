// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Microsoft.Azure.WebJobs.Host.Queues
{
    internal static class CloudQueueExtensions
    {
        // Adding CreateIfNotExists back to the Queue SDK is tracked with: https://github.com/Azure/azure-sdk-for-net/issues/7879
        public static async Task CreateIfNotExistsAsync(this QueueClient queue, CancellationToken cancellationToken)
        {
            try
            {
                Response r = await queue.CreateAsync(null, cancellationToken).ConfigureAwait(false);
            }
            catch (RequestFailedException e) when (e.ErrorCode == "QueueAlreadyExists")
            {
                // Queue already exists, safe to ignore.
            }
        }
    }
}
