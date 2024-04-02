using GlobalImpact.Interfaces;

namespace GlobalImpact.Utils
{
    /// <summary>
    /// Classe de implemetação do Google Maps.
    /// </summary>
    public class GoogleMapsService : IGoogleMapsService
	{
		private readonly string _apiKey;

		public GoogleMapsService(string apiKey)
		{
			_apiKey = apiKey;
		}

		// Implemente os métodos para interagir com a API do Google Maps aqui
	}
}
