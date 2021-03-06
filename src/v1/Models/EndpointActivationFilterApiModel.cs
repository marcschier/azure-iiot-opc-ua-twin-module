// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Modules.Twin.v1.Models {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Endpoint Activation Filter model
    /// </summary>
    public class EndpointActivationFilterApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public EndpointActivationFilterApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public EndpointActivationFilterApiModel(EndpointActivationFilterModel model) {
            SecurityPolicies = model.SecurityPolicies;
            SecurityPolicies = model.SecurityPolicies;
            SecurityMode = model.SecurityMode;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public EndpointActivationFilterModel ToServiceModel() {
            return new EndpointActivationFilterModel {
                TrustLists = TrustLists,
                SecurityPolicies = SecurityPolicies,
                SecurityMode = SecurityMode
            };
        }

        /// <summary>
        /// Certificate trust list identifiers to use for
        /// activation, if null, all certificates are
        /// trusted.  If empty list, no certificates are
        /// trusted which is equal to no filter.
        /// </summary>
        public List<string> TrustLists { get; set; }

        /// <summary>
        /// Endpoint security policies to filter against.
        /// If set to null, all policies are in scope.
        /// </summary>
        public List<string> SecurityPolicies { get; set; }

        /// <summary>
        /// Security mode level to activate. If null,
        /// then <see cref="SecurityMode.Best"/> is assumed.
        /// </summary>
        public SecurityMode? SecurityMode { get; set; }
    }
}

