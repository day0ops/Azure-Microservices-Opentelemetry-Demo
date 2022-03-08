using System.Collections;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using OpenTelemetry;

namespace TelemetryKitchenSink
{
	public class TelemetryBaggageLogger : BaseProcessor<Activity>
	{
		private readonly ILogger<TelemetryBaggageLogger> _logger;

		public TelemetryBaggageLogger(ILogger<TelemetryBaggageLogger> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public override void OnStart(Activity activity)
		{
			_logger.LogInformation("-------------------> Logging Baggages In BaseProcessor:OnStart <-------------------");
            IDictionaryEnumerator baggageEnumerator = Baggage.Current.GetEnumerator();
            while (baggageEnumerator.MoveNext())
            {
                _logger.LogInformation($"OnStart->Received baggage {baggageEnumerator.Key}:{baggageEnumerator.Value}");
            }
        }

		public override void OnEnd(Activity activity)
		{
			_logger.LogInformation("-------------------> Logging Baggages In BaseProcessor:OnEnd <-------------------");
            IDictionaryEnumerator baggageEnumerator = Baggage.Current.GetEnumerator();
            while (baggageEnumerator.MoveNext())
            {
                _logger.LogInformation($"OnEnd->Received baggage {baggageEnumerator.Key}:{baggageEnumerator.Value}");
            }
        }
	}
}