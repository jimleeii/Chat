namespace Chat.EndpointDefinitions;

/// <summary>
/// The chat endpoint definition.
/// </summary>
public class ChatEndpointDefinition : IEndpointDefinition
{
	/// <summary>
	/// Defines the endpoints.
	/// </summary>
	/// <param name="app">The app.</param>
	public void DefineEndpoints(WebApplication app)
	{
		app.MapHub<ChatService>("api/chathub");
	}

	/// <summary>
	/// Defines the services.
	/// </summary>
	/// <param name="services">The services.</param>
	public void DefineServices(IServiceCollection services)
	{
		services.AddSignalR();
	}
}