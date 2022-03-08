using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using OpenTelemetry;

namespace TelemetryKitchenSink
{
	public static class TelemetryAspNetCoreEnricher
	{
        public static void EnrichHttpRequests(Activity activity, string eventName, object rawObject)
        {
            if (eventName.Equals("OnStartActivity"))
            {
                if (rawObject is HttpRequest request)
                {
                    Baggage.SetBaggage("test.baggage1", "bag1");
                    Baggage.SetBaggage("test.baggage2", "bag2");
                }
            }
        }
    }
}