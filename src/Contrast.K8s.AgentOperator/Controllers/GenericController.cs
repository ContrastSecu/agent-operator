﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Contrast.K8s.AgentOperator.Core.Events;
using Contrast.K8s.AgentOperator.Core.State;
using k8s;
using k8s.Models;
using KubeOps.Operator.Controller;
using KubeOps.Operator.Controller.Results;

namespace Contrast.K8s.AgentOperator.Controllers
{
    public abstract class GenericController<T> : IResourceController<T> where T : IKubernetesObject<V1ObjectMeta>
    {
        private readonly IEventStream _eventStream;

        protected GenericController(IEventStream eventStream)
        {
            _eventStream = eventStream;
        }

        public async Task<ResourceControllerResult?> ReconcileAsync(T entity)
        {
            await _eventStream.DispatchDeferred(new EntityReconciled<T>(entity));
            return null;
        }

        public virtual Task StatusModifiedAsync(T entity) => Task.CompletedTask;

        public async Task DeletedAsync(T entity)
        {
            await _eventStream.DispatchDeferred(new EntityDeleted<T>(entity));
        }
    }
}
