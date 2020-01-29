// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Microsoft.Azure.WebJobs.Host.Queues
{
    internal static class StorageQueueMessageExtensions
    {
        public static string TryGetAsString(this QueueMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            string value;

            try
            {
                value = message.AsString;
            }
            catch (Exception ex)
            {
                if (ex is DecoderFallbackException || ex is FormatException)
                {
                    value = null;
                }
                else
                {
                    throw;
                }
            }

            return value;
        }
    }
}
