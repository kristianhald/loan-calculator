using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Website.Model.Calculation;

namespace Website.ApplicationInsights
{
    public class Telemetry
    {
        private const string TelemetryKey = "Telemetry";

        private readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient());

        private Telemetry()
        { }

        public static Telemetry Create()
        {
            var telemetry = (Telemetry)CallContext.GetData(TelemetryKey);
            if (telemetry == null)
            {
                telemetry = new Telemetry();
                CallContext.SetData(TelemetryKey, telemetry);
            }

            return telemetry;
        }

        public T TrackDependency<T>(
            string dependency,
            string command,
            Func<T> executeDependencyCall)
        {
            var success = false;
            var client = _telemetryClient.Value;
            var startTime = DateTime.UtcNow;
            var timer = Stopwatch.StartNew();
            try
            {
                var result = executeDependencyCall();

                success = true;

                return result;
            }
            finally
            {
                timer.Stop();
                client.TrackDependency(dependency, command, startTime, timer.Elapsed, success);
            }
        }

        public void TrackUserInitialVisit()
        {
            var client = _telemetryClient.Value;
            client.TrackEvent("User initial visit");
        }

        public void TrackUserLoadingData()
        {
            var client = _telemetryClient.Value;
            client.TrackEvent("User loading data");
        }

        public void TrackUserCalculation(CalculationData data)
        {
            var client = _telemetryClient.Value;
            client.TrackEvent(
                "User input",
                new Dictionary<string, string>
                {
                    { "Amount", data.Information.Amount.ToString("C") },
                    { "Value", data.Information.Value.ToString("C") },
                    { "Number of loans", data.Information.NumberOfLoans.ToString("N") }
                });
        }
    }
}