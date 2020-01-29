// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace Microsoft.Azure.WebJobs.Host.Queues.Listeners
{
    internal class QueueTriggerExecutor : ITriggerExecutor<QueueMessage>
    {
        private readonly ITriggeredFunctionExecutor _innerExecutor;

        public QueueTriggerExecutor(ITriggeredFunctionExecutor innerExecutor)
        {
            _innerExecutor = innerExecutor;
        }

        public async Task<FunctionResult> ExecuteAsync(QueueMessage value, CancellationToken cancellationToken)
        {
            Guid? parentId = QueueCausalityManager.GetOwner(value);
            TriggeredFunctionData input = new TriggeredFunctionData
            {
                ParentId = parentId,
                TriggerValue = value,
                TriggerDetails = PopulateTriggerDetails(value)
            };
            return await _innerExecutor.TryExecuteAsync(input, cancellationToken);
        }

        internal static Dictionary<string, string> PopulateTriggerDetails(QueueMessage value)
        {
            return new Dictionary<string, string>()
            {
                { "MessageId", value.Id.ToString() },
                { nameof(QueueMessage.DequeueCount), value.DequeueCount.ToString() },
                { nameof(QueueMessage.InsertionTime), value.InsertionTime?.ToString(Constants.DateTimeFormatString) }
            };
        }
    }
}
