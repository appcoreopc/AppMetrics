﻿// <copyright file="DefaultGaugeManager.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using App.Metrics.Core.Abstractions;
using App.Metrics.Core.Options;
using App.Metrics.Gauge.Abstractions;
using App.Metrics.Registry.Abstractions;
using App.Metrics.Tagging;

namespace App.Metrics.Gauge
{
    internal sealed class DefaultGaugeManager : IMeasureGaugeMetrics
    {
        private readonly IBuildGaugeMetrics _gaugeBuilder;
        private readonly IMetricsRegistry _registry;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultGaugeManager" /> class.
        /// </summary>
        /// <param name="gaugeBuilder">The gauge builder.</param>
        /// <param name="registry">The registry storing all metric data.</param>
        public DefaultGaugeManager(IBuildGaugeMetrics gaugeBuilder, IMetricsRegistry registry)
        {
            _registry = registry;
            _gaugeBuilder = gaugeBuilder;
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, Func<double> valueProvider)
        {
            _registry.Gauge(options, () => _gaugeBuilder.Build(valueProvider));
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, double value)
        {
            _registry.Gauge(options, () => _gaugeBuilder.Build()).SetValue(value);
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, MetricTags tags, double value)
        {
            _registry.Gauge(options, tags, () => _gaugeBuilder.Build()).SetValue(value);
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, Func<IMetricValueProvider<double>> valueProvider)
        {
            _registry.Gauge(options, () => _gaugeBuilder.Build(valueProvider));
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, MetricTags tags, Func<IMetricValueProvider<double>> valueProvider)
        {
            _registry.Gauge(options, tags, () => _gaugeBuilder.Build(valueProvider));
        }

        /// <inheritdoc />
        public void SetValue(GaugeOptions options, MetricTags tags, Func<double> valueProvider)
        {
            _registry.Gauge(options, tags, () => _gaugeBuilder.Build(valueProvider));
        }
    }
}