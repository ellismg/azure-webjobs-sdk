// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Microsoft.Azure.WebJobs.Extensions.Storage.Queues
{
    // QueueMessageInput is used in places where we had been calling new QueueMessage(string). It exists
    // because it is helpful in understanding where we were calling new QueueMessage(string) before, in case
    // we need to do some Base64 encoding stuff in the future.
    class QueueMessageInput
    {
        public string MessageText { get; private set; }

        public QueueMessageInput(string messageText)
        {
            MessageText = messageText;
        }

        public QueueMessageInput (byte[] messageBytes)
        {
            MessageText = Convert.ToBase64String(messageBytes);
        }
    }
}
