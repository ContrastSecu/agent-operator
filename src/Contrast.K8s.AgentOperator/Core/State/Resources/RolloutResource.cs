﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Contrast.K8s.AgentOperator.Core.State.Resources.Interfaces;
using Contrast.K8s.AgentOperator.Core.State.Resources.Primitives;


namespace Contrast.K8s.AgentOperator.Core.State.Resources
{

        public record RolloutResource(string Uid,
                                 IReadOnlyCollection<MetadataLabel> Labels,
                                 PodTemplate PodTemplate,
                                 PodSelector Selector)
    : IResourceWithPodTemplate;
    
}


