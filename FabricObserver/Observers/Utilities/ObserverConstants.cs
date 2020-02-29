﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace FabricObserver.Observers.Utilities
{
    public sealed class ObserverConstants
    {
        public const string ObserverManagerName = "ObserverManager";
        public const string ObserverManagerConfigurationSectionName = "ObserverManagerConfiguration";
        public const string ObserverWebApiAppDeployed = "ObserverWebApiEnabled";
        public const string EnableVerboseLoggingParameter = "EnableVerboseLogging";
        public const string EnableLongRunningCsvLogging = "EnableLongRunningCSVLogging";
        public const string ObserverLogPath = "ObserverLogPath";
        public const string DataLogPath = "DataLogPath";
        public const string Fqdn = "FQDN";
        public const string ObserverRunIntervalParameterName = "RunInterval";
        public const string ObserverEnabled = "Enabled";
        public const string AiKey = "AppInsightsInstrumentationKey";
        public const string TelemetryEnabled = "EnableTelemetryProvider";
        public const string EnableEventSourceProvider = "EnableEventSourceProvider";
        public const string EventSourceProviderName = "EventSourceProviderName";
        public const string FabricObserverTelemetryEnabled = "EnableFabricObserverDiagnosticTelemetry";
        public const string AsyncClusterOperationTimeoutSeconds = "ClusterOperationTimeoutSeconds";

        // The name of the package that contains this Observer's configuration
        public const string ObserverConfigurationPackageName = "Config";

        // Setting name for Runtime frequency of the Observer
        public const string ObserverLoopSleepTimeSeconds = "ObserverLoopSleepTimeSeconds";

        // Default to 1 minute if frequency is not supplied in config
        public const int ObserverRunLoopSleepTimeSeconds = 60;

        public const string AllObserversExecutedMessage = "All Observers have been executed.";

        // Setting name for Grace period of shutdown in seconds.
        public const string ObserverShutdownGracePeriodInSeconds = "ObserverShutdownGracePeriodInSeconds";

        // Setting name for Maximum time an observer should run before being considered hung or in some failure state.
        public const string ObserverExecutionTimeout = "ObserverExecutionTimeout";

        // AppObserver.
        public const string AppObserverName = "AppObserver";
        public const string AppObserverConfigurationSectionName = "AppObserverConfiguration";

        // Certificate Observer
        public const string CertificateObserverName = "CertificateObserver";
        public const string CertificateObserverConfigurationSectionName = "CertificateObserverConfiguration";
        public const string CertificateObserverDaysUntilClusterExpiryWarningThreshold = "DaysUntilClusterExpiryWarningThreshold";
        public const string CertificateObserverDaysUntilAppExpiryWarningThreshold = "DaysUntilAppExpiryWarningThreshold";
        public const string CertificateObserverAppCertificateThumbprints = "AppCertThumbprintsToObserve";
        public const string CertificateObserverAppCertificateCommonNames = "AppCertCommonNamesToObserve";

        // DiskObserver.
        public const string DiskObserverName = "DiskObserver";
        public const string DiskObserverConfigurationSectionName = "DiskObserverConfiguration";
        public const string DiskObserverIntervalParameterName = "RunInterval";
        public const string DiskObserverDiskSpacePercentError = "DiskSpacePercentErrorThreshold";
        public const string DiskObserverDiskSpacePercentWarning = "DiskSpacePercentWarningThreshold";
        public const string DiskObserverIoReadsError = "IOReadsErrorThreshold";
        public const string DiskObserverIoReadsWarning = "IOReadsWarningThreshold";
        public const string DiskObserverIoWritesError = "IOWritesErrorThreshold";
        public const string DiskObserverIoWritesWarning = "IOWritesWarningThreshold";
        public const string DiskObserverAverageQueueLengthError = "AverageQueueLengthErrorThreshold";
        public const string DiskObserverAverageQueueLengthWarning = "AverageQueueLengthWarningThreshold";

        // FabricSystemObserver.
        public const string FabricSystemObserverName = "FabricSystemObserver";
        public const string FabricSystemObserverConfigurationSectionName = "FabricSystemObserverConfiguration";
        public const string FabricSystemObserverErrorCpu = "ErrorCpuThresholdPercent";
        public const string FabricSystemObserverWarnCpu = "WarnCpuThresholdPercent";
        public const string FabricSystemObserverErrorMemory = "ErrorMemoryThresholdMB";
        public const string FabricSystemObserverWarnMemory = "WarnMemoryThresholdMB";
        public const string FabricSystemObserverErrorDiskIoReads = "ErrorDiskIOReadsThreshold";
        public const string FabricSystemObserverWarnDiskIoReads = "WarnDiskIOReadsThreshold";
        public const string FabricSystemObserverErrorDiskIoWrites = "ErrorDiskIOWritesThreshold";
        public const string FabricSystemObserverWarnDiskIoWrites = "WarnDiskIOWritesThreshold";
        public const string FabricSystemObserverErrorPercentUnhealthyNodes = "PercentUnhealthyNodesErrorThreshold";
        public const string FabricSystemObserverWarnPercentUnhealthyNodes = "PercentUnhealthyNodesWarnThreshold";
        public const string FabricSystemObserverMonitorWindowsEventLog = "MonitorWindowsEventLog";

        // NetworkObserver.
        public const string NetworkObserverName = "NetworkObserver";
        public const string NetworkObserverConfigurationSectionName = "NetworkObserverConfiguration";

        // NodeObserver.
        public const string NodeObserverName = "NodeObserver";
        public const string NodeObserverConfigurationSectionName = "NodeObserverConfiguration";
        public const string NodeObserverCpuErrorLimitPct = "CpuErrorLimitPercent";
        public const string NodeObserverCpuWarningLimitPct = "CpuWarningLimitPercent";
        public const string NodeObserverMemoryErrorLimitMb = "MemoryErrorLimitMB";
        public const string NodeObserverMemoryWarningLimitMb = "MemoryWarningLimitMB";
        public const string NodeObserverNetworkErrorActivePorts = "NetworkErrorActivePorts";
        public const string NodeObserverNetworkWarningActivePorts = "NetworkWarningActivePorts";
        public const string NodeObserverNetworkErrorEphemeralPorts = "NetworkErrorEphemeralPorts";
        public const string NodeObserverNetworkWarningEphemeralPorts = "NetworkWarningEphemeralPorts";
        public const string NodeObserverNetworkErrorFirewallRules = "NetworkErrorFirewallRules";
        public const string NodeObserverNetworkWarningFirewallRules = "NetworkWarningFirewallRules";
        public const string NodeObserverMemoryUsePercentError = "MemoryErrorLimitPercent";
        public const string NodeObserverMemoryUsePercentWarning = "MemoryWarningLimitPercent";

        // OSObserver.
        public const string OsObserverName = "OSObserver";
        public const string OsObserverConfigurationSectionName = "OSObserverConfiguration";

        // SFConfigurationObserver.
        public const string SfConfigurationObserverName = "SFConfigurationObserver";
        public const string SfConfigurationObserverVersionName = "InfrastructureConfigurationVersion";
        public const string SfConfigurationObserverConfigurationSectionName = "SFConfigurationObserverConfiguration";
        public const string SfConfigurationObserverRunIntervalParameterName = "RunInterval";
    }
}
