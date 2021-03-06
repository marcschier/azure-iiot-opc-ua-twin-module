// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Modules.Twin.v1.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Result of attribute write
    /// </summary>
    public class BatchWriteResponseApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BatchWriteResponseApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BatchWriteResponseApiModel(BatchWriteResultModel model) {
            Results = model.Results?
                .Select(a => new AttributeWriteResponseApiModel(a)).ToList();
        }

        /// <summary>
        /// All results of attribute writes
        /// </summary>
        public List<AttributeWriteResponseApiModel> Results { set; get; }
    }
}
