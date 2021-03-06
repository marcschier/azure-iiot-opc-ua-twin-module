// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Modules.Twin.v1.Models {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DiscoveryConfigApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiscoveryConfigApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public DiscoveryConfigApiModel(DiscoveryConfigModel model) {
            AddressRangesToScan = model.AddressRangesToScan;
            NetworkProbeTimeout = model.NetworkProbeTimeout;
            MaxNetworkProbes = model.MaxNetworkProbes;
            PortRangesToScan = model.PortRangesToScan;
            PortProbeTimeout = PortProbeTimeout;
            MaxPortProbes = model.MaxPortProbes;
            MinPortProbesPercent = model.MinPortProbesPercent;
            IdleTimeBetweenScans = IdleTimeBetweenScans;
            Callbacks = model.Callbacks?
                .Where(c => c != null)
                .Select(c => new CallbackApiModel(c))
                .ToList();
            DiscoveryUrls = model.DiscoveryUrls;
            Locales = model.Locales;
            ActivationFilter = model.ActivationFilter == null ? null :
               new EndpointActivationFilterApiModel(model.ActivationFilter);
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public DiscoveryConfigModel ToServiceModel() {
            return new DiscoveryConfigModel {
                AddressRangesToScan = AddressRangesToScan,
                NetworkProbeTimeout = NetworkProbeTimeout,
                MaxNetworkProbes = MaxNetworkProbes,
                PortRangesToScan = PortRangesToScan,
                PortProbeTimeout = PortProbeTimeout,
                MaxPortProbes = MaxPortProbes,
                MinPortProbesPercent = MinPortProbesPercent,
                IdleTimeBetweenScans = IdleTimeBetweenScans,
                ActivationFilter = ActivationFilter?.ToServiceModel(),
                Callbacks = Callbacks?
                    .Where(c => c != null)
                    .Select(c => c.ToServiceModel())
                    .ToList(),
                Locales = Locales,
                DiscoveryUrls = DiscoveryUrls
            };
        }

        /// <summary>
        /// Address ranges to scan (null == all wired)
        /// </summary>
        public string AddressRangesToScan { get; set; }

        /// <summary>
        /// Network probe timeout.
        /// </summary>
        public TimeSpan? NetworkProbeTimeout { get; set; }

        /// <summary>
        /// Max network probes that should ever run.
        /// </summary>
        public int? MaxNetworkProbes { get; set; }

        /// <summary>
        /// Port ranges to scan (null == all unassigned)
        /// </summary>
        public string PortRangesToScan { get; set; }

        /// <summary>
        /// Port probe timeout
        /// </summary>
        public TimeSpan? PortProbeTimeout { get; set; }

        /// <summary>
        /// Max port probes that should ever run.
        /// </summary>
        public int? MaxPortProbes { get; set; }

        /// <summary>
        /// Probes that must always be there as percent of max.
        /// </summary>
        public int? MinPortProbesPercent { get; set; }

        /// <summary>
        /// Delay time between discovery sweeps
        /// </summary>
        public TimeSpan? IdleTimeBetweenScans { get; set; }

        /// <summary>
        /// List of preset discovery urls to use
        /// </summary>
        public List<string> DiscoveryUrls { get; set; }

        /// <summary>
        /// List of locales to filter with during discovery
        /// </summary>
        public List<string> Locales { get; set; }

        /// <summary>
        /// Callbacks to invoke once onboarding finishes
        /// </summary>
        public List<CallbackApiModel> Callbacks { get; set; }

        /// <summary>
        /// Activate all twins with this filter during onboarding.
        /// </summary>
        public EndpointActivationFilterApiModel ActivationFilter { get; set; }
    }
}
