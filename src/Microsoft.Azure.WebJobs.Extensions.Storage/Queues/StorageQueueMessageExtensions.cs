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
        // TODO(matell): We need to figure out the correct semantics here.  The v11 SDK had some translation logic that would allow you
        // to Base64 encode strings on send and recieve, which the newer v12 SDK does not support natively. We will probably want to add
        // an option to the trigger to configure if we expect messages to be Base64 encoded or not and then update this method accodringly.
        public static string AsString(this QueueMessage message)
        {
            return message.MessageText;
        }

        // TODO(matell): We need to figure out the correct semantics here.  The v11 SDK had some translation logic that would allow you
        // to Base64 encode strings on send and recieve, which the newer v12 SDK does not support natively. We will probably want to add
        // an option to the trigger to configure if we expect messages to be Base64 encoded or not and then update this method accodringly.
        public static byte[] AsBytes(this QueueMessage message)
        {
            return Convert.FromBase64String(message.MessageText);
        }

        public static string TryGetAsString(this QueueMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            string value;

            try
            {
                value = message.AsString();
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
