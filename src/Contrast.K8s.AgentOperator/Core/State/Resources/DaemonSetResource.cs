﻿using System.Collections.Generic;
using Contrast.K8s.AgentOperator.Core.State.Resources.Interfaces;
using Contrast.K8s.AgentOperator.Core.State.Resources.Primitives;

namespace Contrast.K8s.AgentOperator.Core.State.Resources
{
    public record DaemonSetResource(IReadOnlyCollection<MetadataLabel> Labels,
                                    IReadOnlyCollection<MetadataAnnotations> Annotations,
                                    PodTemplate PodTemplate)
        : IResourceWithPodTemplate;
}
