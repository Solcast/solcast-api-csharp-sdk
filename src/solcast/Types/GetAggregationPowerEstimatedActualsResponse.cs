﻿using System.Collections.Generic;
using ServiceStack;

namespace Solcast.Types
{
    public class GetAggregationPowerEstimatedActualsResponse
    {
        public GetAggregationPowerEstimatedActualsResponse()
        {
            EstimatedActuals = new List<ApiAggregationPowerEstimateActual>{};
        }

        public virtual string AggregationName { get; set; }
        public virtual List<ApiAggregationPowerEstimateActual> EstimatedActuals { get; set; }
        public virtual ResponseStatus ResponseStatus { get; set; }
    }
}