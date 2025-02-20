﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Contrast.K8s.AgentOperator.Core.Telemetry.Models
{
    public enum TelemetrySubmissionResult
    {
        Success,
        TransientError,
        PermanentError
    }
}
