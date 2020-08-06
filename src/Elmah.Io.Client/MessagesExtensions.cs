// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Elmah.Io.Client
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Messages.
    /// </summary>
    public static partial class MessagesExtensions
    {
            /// <summary>
            /// Fetch messages from a log.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the messages.
            /// </param>
            /// <param name='pageIndex'>
            /// The page number of the result.
            /// </param>
            /// <param name='pageSize'>
            /// The number of messages to load (max 100) or 15 if not set.
            /// </param>
            /// <param name='query'>
            /// A full-text or Lucene query to limit the messages by.
            /// </param>
            /// <param name='fromParameter'>
            /// A start date and time to search from (not included).
            /// </param>
            /// <param name='to'>
            /// An end date and time to search to (not included).
            /// </param>
            /// <param name='includeHeaders'>
            /// Include headers like server variables and cookies in the result (slower).
            /// </param>
            public static MessagesResult GetAll(this IMessages operations, string logId, int? pageIndex = 0, int? pageSize = 15, string query = default(string), System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?), bool? includeHeaders = false)
            {
                return operations.GetAllAsync(logId, pageIndex, pageSize, query, fromParameter, to, includeHeaders).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Fetch messages from a log.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the messages.
            /// </param>
            /// <param name='pageIndex'>
            /// The page number of the result.
            /// </param>
            /// <param name='pageSize'>
            /// The number of messages to load (max 100) or 15 if not set.
            /// </param>
            /// <param name='query'>
            /// A full-text or Lucene query to limit the messages by.
            /// </param>
            /// <param name='fromParameter'>
            /// A start date and time to search from (not included).
            /// </param>
            /// <param name='to'>
            /// An end date and time to search to (not included).
            /// </param>
            /// <param name='includeHeaders'>
            /// Include headers like server variables and cookies in the result (slower).
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MessagesResult> GetAllAsync(this IMessages operations, string logId, int? pageIndex = 0, int? pageSize = 15, string query = default(string), System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?), bool? includeHeaders = false, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllWithHttpMessagesAsync(logId, pageIndex, pageSize, query, fromParameter, to, includeHeaders, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes a list of messages by logid and query.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='body'>
            /// A search object containing query, time filters etc.
            /// </param>
            public static void DeleteAll(this IMessages operations, string logId, Search body = default(Search))
            {
                operations.DeleteAllAsync(logId, body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes a list of messages by logid and query.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='body'>
            /// A search object containing query, time filters etc.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAllAsync(this IMessages operations, string logId, Search body = default(Search), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteAllWithHttpMessagesAsync(logId, body, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Create a new message.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log which should contain the new message.
            /// </param>
            /// <param name='body'>
            /// The message object to create.
            /// </param>
            public static CreateMessageResult Create(this IMessages operations, string logId, CreateMessage body = default(CreateMessage))
            {
                return operations.CreateAsync(logId, body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Create a new message.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log which should contain the new message.
            /// </param>
            /// <param name='body'>
            /// The message object to create.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<CreateMessageResult> CreateAsync(this IMessages operations, string logId, CreateMessage body = default(CreateMessage), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateWithHttpMessagesAsync(logId, body, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Fetch a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to fetch.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            public static Message Get(this IMessages operations, string id, string logId)
            {
                return operations.GetAsync(id, logId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Fetch a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to fetch.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Message> GetAsync(this IMessages operations, string id, string logId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(id, logId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Delete a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to delete.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            public static void Delete(this IMessages operations, string id, string logId)
            {
                operations.DeleteAsync(id, logId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to delete.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAsync(this IMessages operations, string id, string logId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteWithHttpMessagesAsync(id, logId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Hide a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to hide.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            public static void Hide(this IMessages operations, string id, string logId)
            {
                operations.HideAsync(id, logId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Hide a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to hide.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task HideAsync(this IMessages operations, string id, string logId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.HideWithHttpMessagesAsync(id, logId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Fix a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to fix.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='markAllAsFixed'>
            /// If set to true, all instances of the log message are set to fixed.
            /// </param>
            public static void Fix(this IMessages operations, string id, string logId, bool? markAllAsFixed = false)
            {
                operations.FixAsync(id, logId, markAllAsFixed).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Fix a message by its ID.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The ID of the message to fix.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log containing the message.
            /// </param>
            /// <param name='markAllAsFixed'>
            /// If set to true, all instances of the log message are set to fixed.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task FixAsync(this IMessages operations, string id, string logId, bool? markAllAsFixed = false, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.FixWithHttpMessagesAsync(id, logId, markAllAsFixed, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Create one or more new messages.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log which should contain the new messages.
            /// </param>
            /// <param name='body'>
            /// The messages to create.
            /// </param>
            public static IList<CreateBulkMessageResult> CreateBulk(this IMessages operations, string logId, IList<CreateMessage> body = default(IList<CreateMessage>))
            {
                return operations.CreateBulkAsync(logId, body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Create one or more new messages.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='logId'>
            /// The ID of the log which should contain the new messages.
            /// </param>
            /// <param name='body'>
            /// The messages to create.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<CreateBulkMessageResult>> CreateBulkAsync(this IMessages operations, string logId, IList<CreateMessage> body = default(IList<CreateMessage>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateBulkWithHttpMessagesAsync(logId, body, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
