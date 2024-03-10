using GlobalImpact.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalImpact.Utils
{
    /// <summary>
    /// Classe de implementação da API do Google Maps.
    /// </summary>
    public static class GoogleMapsServiceExtensions
	{
		public static IServiceCollection AddGoogleMapsAPI(this IServiceCollection services, string apiKey)
		{
			// Aqui você pode configurar o serviço da API do Google Maps conforme necessário
			// Por exemplo, você pode configurar as opções de autenticação com a chave da API

			// Você pode usar a chave da API do Google Maps passada como parâmetro
			services.AddSingleton<IGoogleMapsService>(new GoogleMapsService(apiKey));

			return services;
		}
	}
}
