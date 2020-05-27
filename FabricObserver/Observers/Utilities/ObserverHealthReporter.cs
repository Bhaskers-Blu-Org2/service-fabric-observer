// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Fabric;
using System.Fabric.Health;
using FabricObserver.Observers.Utilities.Telemetry;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FabricObserver.Observers.Utilities
{
    /// <summary>
    /// Reports health data to Service Fabric Health Manager and logs locally (optional).
    /// </summary>
    public class ObserverHealthReporter
    {
        private readonly Logger logger;
        private readonly FabricClient fabricClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverHealthReporter"/> class.
        /// </summary>
        /// <param name="logger">file logger instance.</param>
        public ObserverHealthReporter(Logger logger)
        {
            this.fabricClient = ObserverManager.FabricClientInstance;
            this.fabricClient.Settings.HealthReportSendInterval = TimeSpan.FromSeconds(1);
            this.fabricClient.Settings.HealthReportRetrySendInterval = TimeSpan.FromSeconds(3);
            this.logger = logger;
        }

        /// <summary>
        /// Report FabricObserver service health as log event (not to SF Health).
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="propertyName">Name of the health property.</param>
        /// <param name="healthState">Health state (Ok, Error, etc).</param>
        /// <param name="description">Description of the health condition.</param>
        public void ReportFabricObserverServiceHealth(
            string serviceName,
            string propertyName,
            HealthState healthState,
            string description)
        {
            if (healthState == HealthState.Error)
            {
                this.logger.LogError("FabricObserver service health error: " + serviceName + " | " + propertyName + " | {0}", description);
            }

            if (healthState == HealthState.Warning)
            {
                this.logger.LogWarning("FabricObserver service health warning: " + serviceName + " | " + propertyName + " | {0}", description);
            }
        }

        public void ReportHealthToServiceFabric(HealthReport healthReport)
        {
            if (healthReport == null)
            {
                return;
            }

            // There is no real need to change Immediate to true here for errors/warnings. This only adds unecessary stress to the
            // Health subsystem.
            var sendOptions = new HealthReportSendOptions { Immediate = false };

            // Quickly send OK (clears warning/errors states).
            if (healthReport.State == HealthState.Ok)
            {
                sendOptions.Immediate = true;
            }

            var timeToLive = TimeSpan.FromMinutes(5);

            if (healthReport.HealthReportTimeToLive != default)
            {
                timeToLive = healthReport.HealthReportTimeToLive;
            }

            // Set property for health event.
            string property = healthReport.Property;

            if (string.IsNullOrEmpty(property))
            {
                switch (healthReport.Observer)
                {
                    case ObserverConstants.AppObserverName:
                        property = "ApplicationHealth";
                        break;
                    case ObserverConstants.CertificateObserverName:
                        property = "SecurityHealth";
                        break;
                    case ObserverConstants.DiskObserverName:
                        property = "DiskHealth";
                        break;
                    case ObserverConstants.FabricSystemObserverName:
                        property = "FabricSystemServiceHealth";
                        break;
                    case ObserverConstants.NetworkObserverName:
                        property = "NetworkHealth";
                        break;
                    case ObserverConstants.OsObserverName:
                        property = "MachineInformation";
                        break;
                    case ObserverConstants.NodeObserverName:
                        property = "MachineResourceHealth";
                        break;
                    default:
                        property = "FOGenericHealth";
                        break;
                }
            }

            string sourceId = healthReport.Observer;
            TelemetryData healthData = healthReport.HealthData;

            if (!string.IsNullOrEmpty(healthReport.Code))
            {
                // Only use FOErrorWarningCode for source
                sourceId += $"({healthReport.Code})";
            }

            string errWarnPreamble = string.Empty;

            if (healthReport.State == HealthState.Error
                || healthReport.State == HealthState.Warning)
            {
                errWarnPreamble =
                    $"{healthReport.Observer} detected " +
                    $"{Enum.GetName(typeof(HealthState), healthReport.State)} threshold breach. ";

                // OSObserver does not monitor resources and therefore does not support related usage threshold configuration.
                if (healthReport.Observer == ObserverConstants.OsObserverName
                    && property == "OSConfiguration")
                {
                    errWarnPreamble = $"{ObserverConstants.OsObserverName} detected potential problem with OS configuration: ";
                    property = "OSConfiguration";
                }
            }

            string message = $"{errWarnPreamble}{healthReport.HealthMessage}";

            if (healthData != null)
            {
                message = JsonConvert.SerializeObject(healthData);
            }

            var healthInformation = new HealthInformation(sourceId, property, healthReport.State)
            {
                Description = $"{message}",
                TimeToLive = timeToLive,
                RemoveWhenExpired = true,
            };

            // Log event only if ObserverWebApi (REST API Log reader service) app is deployed.
            if (ObserverManager.ObserverWebAppDeployed
                && healthReport.EmitLogEvent)
            {
                if (healthReport.State == HealthState.Error)
                {
                    this.logger.LogError(healthReport.NodeName + ": {0}", healthInformation.Description);
                }
                else if (healthReport.State == HealthState.Warning)
                {
                    this.logger.LogWarning(healthReport.NodeName + ": {0}", healthInformation.Description);
                }
                else
                {
                    this.logger.LogInfo(healthReport.NodeName + ": {0}", healthInformation.Description);
                }
            }

            // To SFX.
            if (healthReport.ReportType == HealthReportType.Application && healthReport.AppName != null)
            {
                var appHealthReport = new ApplicationHealthReport(healthReport.AppName, healthInformation);
                this.fabricClient.HealthManager.ReportHealth(appHealthReport, sendOptions);
            }
            else
            {
                var nodeHealthReport = new NodeHealthReport(healthReport.NodeName, healthInformation);
                this.fabricClient.HealthManager.ReportHealth(nodeHealthReport, sendOptions);
            }
        }
    }

    public enum HealthReportType
    {
        Application,
        Node,
    }
}
