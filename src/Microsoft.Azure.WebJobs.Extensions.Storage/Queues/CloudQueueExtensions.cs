// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Microsoft.Azure.WebJobs.Host.Queues
{
    internal static class CloudQueueExtensions
    {
        public static Task<bool> CreateIfNotExistsAsync(this QueueClient queue, CancellationToken cancellationToken)
        {
            return queue.CreateIfNotExistsAsync(null, null, cancellationToken);
        }
    }
}
