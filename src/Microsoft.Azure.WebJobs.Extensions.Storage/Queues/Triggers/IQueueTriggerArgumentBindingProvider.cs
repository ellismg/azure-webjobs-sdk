// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal interface IQueueTriggerArgumentBindingProvider
    {
        ITriggerDataArgumentBinding<QueueMessage> TryCreate(ParameterInfo parameter);
    }
}
