using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GlobalImpact.Services
{
    public class GoogleMapsApiKeyService
    {
        private readonly ViewDataDictionary _viewData;

        public GoogleMapsApiKeyService(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        public string GetApiKey()
        {
            return _viewData["GoogleMapsApiKey"] as string;
        }
    }
}
