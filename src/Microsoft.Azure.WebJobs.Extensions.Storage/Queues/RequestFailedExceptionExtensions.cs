// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Azure;

namespace Microsoft.Azure.WebJobs.Extensions.Storage.Queues
{
    internal static class RequestFailedExceptionExtensions
    {
        public static bool IsServerSideError(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.Status >= 500 && exception.Status < 600;
        }

        /// <summary>
        /// Determines whether the exception is due to a task cancellation.
        /// </summary>
        /// <param name="exception">The storage exception.</param>
        /// <returns><see langword="true"/> if the inner exception is a <see cref="TaskCanceledException"/>. Otherwise, <see langword="false"/>.</returns>
        public static bool IsTaskCanceled(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.InnerException is TaskCanceledException;
        }

        /// <summary>
        /// Determines whether the exception is due to a 404 Not Found error with the error code QueueNotFound.
        /// </summary>
        /// <param name="exception">The storage exception.</param>
        /// <returns>
        /// <see langword="true"/> if the exception is due to a 404 Not Found error with the error code QueueNotFound;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsNotFoundQueueNotFound(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.Status == 404 && exception.ErrorCode == "QueueNotFound";
        }

        /// <summary>
        /// Determines whether the exception is due to a 409 Conflict error with the error code QueueBeingDeleted or
        /// QueueDisabled.
        /// </summary>
        /// <param name="exception">The storage exception.</param>
        /// <returns>
        /// <see langword="true"/> if the exception is due to a 409 Conflict error with the error code QueueBeingDeleted
        /// or QueueDisabled; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsConflictQueueBeingDeletedOrDisabled(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.Status == 409 && exception.ErrorCode == "QueueBeingDeleted";
        }

        /// <summary>
        /// Determines whether the exception is due to a 400 Bad Request error with the error code PopReceiptMismatch.
        /// </summary>
        /// <param name="exception">The storage exception.</param>
        /// <returns>
        /// <see langword="true"/> if the exception is due to a 400 Bad Request error with the error code
        /// PopReceiptMismatch; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsBadRequestPopReceiptMismatch(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.Status == 400 && exception.ErrorCode == "PopReceiptMismatch";
        }

        /// <summary>
        /// Determines whether the exception is due to a 404 Not Found error with the error code MessageNotFound or
        /// QueueNotFound.
        /// </summary>
        /// <param name="exception">The storage exception.</param>
        /// <returns>
        /// <see langword="true"/> if the exception is due to a 404 Not Found error with the error code MessageNotFound
        /// or QueueNotFound; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsNotFoundMessageOrQueueNotFound(this RequestFailedException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return exception.Status == 404 && (exception.ErrorCode == "MessageNotFound" || exception.ErrorCode == "QueueNotFound");
        }
    }
}
