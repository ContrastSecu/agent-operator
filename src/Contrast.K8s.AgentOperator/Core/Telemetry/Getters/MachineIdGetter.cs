﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using Contrast.K8s.AgentOperator.Core.Telemetry.Cluster;
using Contrast.K8s.AgentOperator.Core.Telemetry.Helpers;

namespace Contrast.K8s.AgentOperator.Core.Telemetry.Getters
{
    public class MachineIdGetter
    {
        private readonly IClusterIdState _clusterIdState;
        private readonly Sha256Hasher _hasher;

        public MachineIdGetter(IClusterIdState clusterIdState, Sha256Hasher hasher)
        {
            _clusterIdState = clusterIdState;
            _hasher = hasher;
        }

        public string GetMachineId()
        {
            var uniqueId = _clusterIdState.GetClusterId()?.ToString();
            if (uniqueId != null)
            {
                return _hasher.Hash(uniqueId);
            }

            return "_" + Guid.NewGuid().ToString("N");
        }
    }
}
