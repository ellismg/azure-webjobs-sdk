// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs.Host.Converters;

namespace Microsoft.Azure.WebJobs.Host.Queues.Triggers
{
    internal class OutputConverter<TInput> : IObjectToTypeConverter<QueueMessage>
        where TInput : class
    {
        private readonly IConverter<TInput, QueueMessage> _innerConverter;

        public OutputConverter(IConverter<TInput, QueueMessage> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public bool TryConvert(object input, out QueueMessage output)
        {
            TInput typedInput = input as TInput;

            if (typedInput == null)
            {
                output = null;
                return false;
            }

            output = _innerConverter.Convert(typedInput);
            return true;
        }
    }
}
