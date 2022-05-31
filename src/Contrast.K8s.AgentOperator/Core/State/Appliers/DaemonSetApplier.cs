﻿using System.Threading;
using System.Threading.Tasks;
using Contrast.K8s.AgentOperator.Core.Kube;
using Contrast.K8s.AgentOperator.Core.State.Resources;
using JetBrains.Annotations;
using k8s.Models;
using MediatR;

namespace Contrast.K8s.AgentOperator.Core.State.Appliers
{
    [UsedImplicitly]
    public class DaemonSetApplier : BaseApplier<V1DaemonSet, DaemonSetResource>
    {
        public DaemonSetApplier(IStateContainer stateContainer, IMediator mediator) : base(stateContainer, mediator)
        {
        }

        protected override ValueTask<DaemonSetResource> CreateFrom(V1DaemonSet entity, CancellationToken cancellationToken = default)
        {
            var resource = new DaemonSetResource(
                entity.Uid(),
                entity.Metadata.GetLabels(),
                entity.Spec.Template.GetPod(),
                entity.Spec.Selector.ToPodSelector()
            );
            return ValueTask.FromResult(resource);
        }
    }
}
