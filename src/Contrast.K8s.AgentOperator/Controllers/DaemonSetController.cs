﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Contrast.K8s.AgentOperator.Core.Kube;
using Contrast.K8s.AgentOperator.Core.State;
using JetBrains.Annotations;
using k8s.Models;
using KubeOps.Operator.Rbac;

namespace Contrast.K8s.AgentOperator.Controllers
{
    [EntityRbac(typeof(V1DaemonSet), Verbs = VerbConstants.ReadAndPatch), UsedImplicitly]
    public class DaemonSetController : GenericController<V1DaemonSet>
    {
        public DaemonSetController(IEventStream eventStream) : base(eventStream)
        {
        }
    }
}
