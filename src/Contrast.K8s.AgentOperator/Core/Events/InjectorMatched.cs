﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Contrast.K8s.AgentOperator.Core.State;
using Contrast.K8s.AgentOperator.Core.State.Resources;
using Contrast.K8s.AgentOperator.Core.State.Resources.Interfaces;
using MediatR;

namespace Contrast.K8s.AgentOperator.Core.Events
{
    public record InjectorMatched(ResourceIdentityPair<IResourceWithPodTemplate> Target,
                                  ResourceIdentityPair<AgentInjectorResource>? Injector) : INotification;
}
