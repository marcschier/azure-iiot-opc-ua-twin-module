// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Modules.Twin.v1.Controllers {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using Microsoft.Azure.IIoT.OpcUa.Edge;
    using Microsoft.Azure.IIoT.Diagnostics;
    using Microsoft.Azure.IIoT.Module.Framework;
    using Microsoft.Azure.IIoT.Hub;
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Endpoint settings controller
    /// </summary>
    [Version(1)]
    public class EndpointSettingsController : ISettingsController {

        /// <summary>
        /// Endoint url for direct server access
        /// </summary>
        public string EndpointUrl {
            get => _endpointUrl;
            set => _endpointUrl = string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// User token to pass to server
        /// </summary>
        public JToken Credential {
            get => _credential;
            set => _credential = value;
        }

        /// <summary>
        /// Type of token
        /// </summary>
        public JToken CredentialType {
            get => JToken.FromObject(_credentialType);
            set => _credentialType = value?.ToObject<CredentialType>();
        }

        /// <summary>
        /// Endpoint security policy to use.
        /// </summary>
        public string SecurityPolicy {
            get => _securityPolicy;
            set => _securityPolicy = string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// Security mode to use for communication
        /// </summary>
        public JToken SecurityMode {
            get => JToken.FromObject(_securityMode);
            set => _securityMode = value?.ToObject<SecurityMode>();
        }

        /// <summary>
        /// Endpoint certificate thumbprint to validate
        /// </summary>
        public Dictionary<string, string> ServerThumbprint {
            get => _serverThumbprint.EncodeAsDictionary();
            set => _serverThumbprint = value.DecodeAsByteArray();
        }

        /// <summary>
        /// Full client certificate to use when connecting to endpoint
        /// </summary>
        public Dictionary<string, string> ClientCertificate {
            get => _clientCertificate.EncodeAsDictionary();
            set => _clientCertificate = value.DecodeAsByteArray();
        }

        /// <summary>
        /// Create controller with service
        /// </summary>
        /// <param name="logger"></param>
        public EndpointSettingsController(IEndpointServices twin, ILogger logger) {
            _twin = twin ?? throw new ArgumentNullException(nameof(twin));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Apply endpoint update
        /// </summary>
        /// <returns></returns>
        public Task ApplyAsync() {
            return _twin.SetEndpointAsync(
                new EndpointModel {
                    SecurityMode = _securityMode,
                    SecurityPolicy = _securityPolicy,
                    User =
                        _credentialType == Registry.Models.CredentialType.None ? null :
                            new CredentialModel {
                                Value = _credential,
                                Type = _credentialType,
                            },
                    Url = _endpointUrl,
                    ServerThumbprint = _serverThumbprint,
                    ClientCertificate = _clientCertificate
                });
        }

        private CredentialType? _credentialType;
        private JToken _credential;
        private string _endpointUrl;
        private string _securityPolicy;
        private SecurityMode? _securityMode;
        private byte[] _serverThumbprint;
        private byte[] _clientCertificate;

        private readonly IEndpointServices _twin;
        private readonly ILogger _logger;
    }
}
