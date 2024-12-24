using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Chat.Services;

/// <summary>
/// A service that manages chat connections and messaging.
/// </summary>
[Authorize]
public class ChatService : Hub
{
	// Describes the connections between connection IDs and user names
	private static readonly ConcurrentDictionary<string, string> _connections = new();

	/// <summary>
	/// Adds the connection ID and user name to the connections dictionary when a client connects.
	/// </summary>
	public override async Task OnConnectedAsync()
	{
		if (_connections == null)
		{
			throw new InvalidOperationException("Connections dictionary is null.");
		}

		var userName = Context.User!.Identity!.Name;
		if (userName != null)
		{
			_connections[Context.ConnectionId] = userName;
		}
		await base.OnConnectedAsync();
	}

	/// <summary>
	/// Removes the connection ID from the connections dictionary when a client disconnects.
	/// </summary>
	/// <param name="exception">The exception associated with the disconnection, if any.</param>
	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		if (_connections == null)
		{
			throw new InvalidOperationException("Connections dictionary is null.");
		}

		if (Context.ConnectionId == null)
		{
			throw new InvalidOperationException("ConnectionId is null.");
		}

		if (!_connections.TryRemove(Context.ConnectionId, out _))
		{
			throw new InvalidOperationException($"Failed to remove connection ID {_connections[Context.ConnectionId]} from the connections dictionary.");
		}

		await base.OnDisconnectedAsync(exception);
	}

	/// <summary>
	/// Sends a message to all connected clients.
	/// </summary>
	/// <param name="user">The name of the user sending the message.</param>
	/// <param name="message">The message to be sent to all clients.</param>

	public async Task SendMessageToAllAsync(string user, string message)
	{
		await Clients.All.SendAsync("ReceiveMessage", user, message);
	}

	/// <summary>
	/// Sends a message to a specific user.
	/// </summary>
	/// <param name="user">The target user's name.</param>
	/// <param name="message">The message to be sent.</param>
	/// <returns>A task representing the asynchronous operation.</returns>

	public async Task SendMessageToUserAsync(string user, string message)
	{
		var connectionId = _connections.FirstOrDefault(c => c.Value == user).Key;
		if (connectionId != null)
		{
			await Clients.Client(connectionId).SendAsync("ReceiveMessage", Context.User!.Identity!.Name, message);
		}
	}
}