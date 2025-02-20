﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;

namespace Contrast.K8s.AgentOperator.Core.Telemetry
{
    public interface ITelemetryOptOut
    {
        bool IsOptOutActive();
    }

    public class TelemetryOptOut : ITelemetryOptOut
    {
        // ReSharper disable once StringLiteralTypo
        private const string OptOutEnvironmentVariableNameOld = "CONTRAST_DOTNET_TELEMETRY_OPTOUT";
        private const string OptOutEnvironmentVariableNameNew = "CONTRAST_AGENT_TELEMETRY_OPTOUT";

        private bool? _cache;

        public bool IsOptOutActive()
        {
            return _cache ??= IsOptOutActiveFor(OptOutEnvironmentVariableNameOld)
                              || IsOptOutActiveFor(OptOutEnvironmentVariableNameNew);
        }

        private static bool IsOptOutActiveFor(string variable)
        {
            var optOutFlag = Environment.GetEnvironmentVariable(variable)?.Trim();
            return !string.IsNullOrEmpty(optOutFlag)
                   && (string.Equals(optOutFlag, true.ToString(), StringComparison.OrdinalIgnoreCase)
                       || string.Equals(optOutFlag, 1.ToString()));
        }
    }
}
