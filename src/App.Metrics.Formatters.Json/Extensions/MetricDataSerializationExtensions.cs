﻿// <copyright file="MetricDataSerializationExtensions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System.Linq;
using App.Metrics.Core;
using App.Metrics.Infrastructure;

namespace App.Metrics.Formatters.Json.Extensions
{
    public static class MetricDataSerializationExtensions
    {
        public static MetricData ToMetric(this MetricsDataValueSource source)
        {
            var jsonContexts = source.Contexts.ToSerializableMetric();

            return new MetricData
                   {
                       Environment = source.Environment.ToEnvDictionary(),
                       Timestamp = source.Timestamp,
                       Contexts = jsonContexts.ToArray()
                   };
        }

        public static MetricsDataValueSource ToMetricValueSource(this MetricData source)
        {
            var contexts = source.Contexts.FromSerializableMetric();

            return new MetricsDataValueSource(source.Timestamp, new EnvironmentInfo(source.Environment), contexts);
        }
    }
}