﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.488
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSVanalyze.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public int InputSize {
            get {
                return ((int)(this["InputSize"]));
            }
            set {
                this["InputSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int OutPutSize {
            get {
                return ((int)(this["OutPutSize"]));
            }
            set {
                this["OutPutSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int Hidden1 {
            get {
                return ((int)(this["Hidden1"]));
            }
            set {
                this["Hidden1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int Hidden2 {
            get {
                return ((int)(this["Hidden2"]));
            }
            set {
                this["Hidden2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2008-01-01")]
        public global::System.DateTime StartDate {
            get {
                return ((global::System.DateTime)(this["StartDate"]));
            }
            set {
                this["StartDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2008-02-01")]
        public global::System.DateTime EndDate {
            get {
                return ((global::System.DateTime)(this["EndDate"]));
            }
            set {
                this["EndDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2011-01-01")]
        public global::System.DateTime EvalStartDate {
            get {
                return ((global::System.DateTime)(this["EvalStartDate"]));
            }
            set {
                this["EvalStartDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2011-12-07")]
        public global::System.DateTime EvalEndDate {
            get {
                return ((global::System.DateTime)(this["EvalEndDate"]));
            }
            set {
                this["EvalEndDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int MaxTrainingTime {
            get {
                return ((int)(this["MaxTrainingTime"]));
            }
            set {
                this["MaxTrainingTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public int MaxEpochs {
            get {
                return ((int)(this["MaxEpochs"]));
            }
            set {
                this["MaxEpochs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TrainingOpen.egb")]
        public string TrainingOpen {
            get {
                return ((string)(this["TrainingOpen"]));
            }
            set {
                this["TrainingOpen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TrainingClose.egb")]
        public string TrainingClose {
            get {
                return ((string)(this["TrainingClose"]));
            }
            set {
                this["TrainingClose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TrainingHigh.egb")]
        public string TrainingHigh {
            get {
                return ((string)(this["TrainingHigh"]));
            }
            set {
                this["TrainingHigh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TrainingLow.egb")]
        public string TrainingLow {
            get {
                return ((string)(this["TrainingLow"]));
            }
            set {
                this["TrainingLow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FeedNetworkOpen.eg")]
        public string NetworkFeedOpen {
            get {
                return ((string)(this["NetworkFeedOpen"]));
            }
            set {
                this["NetworkFeedOpen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ElmanNetworkOpen.eg")]
        public string NetworkElmanOpen {
            get {
                return ((string)(this["NetworkElmanOpen"]));
            }
            set {
                this["NetworkElmanOpen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ElmannetworkClose.eg")]
        public string NetworkElmanClose {
            get {
                return ((string)(this["NetworkElmanClose"]));
            }
            set {
                this["NetworkElmanClose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ElmannetworkHigh.eg")]
        public string NetworkElmanHigh {
            get {
                return ((string)(this["NetworkElmanHigh"]));
            }
            set {
                this["NetworkElmanHigh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ElmannetworkLow.eg")]
        public string NetworkElmanLow {
            get {
                return ((string)(this["NetworkElmanLow"]));
            }
            set {
                this["NetworkElmanLow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FeedNetworkHigh.eg")]
        public string NetworkFeedHigh {
            get {
                return ((string)(this["NetworkFeedHigh"]));
            }
            set {
                this["NetworkFeedHigh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FeedNetWorkLow.eg")]
        public string NetworkFeedLow {
            get {
                return ((string)(this["NetworkFeedLow"]));
            }
            set {
                this["NetworkFeedLow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FeednetworkClose.eg")]
        public string NetworkFeedClose {
            get {
                return ((string)(this["NetworkFeedClose"]));
            }
            set {
                this["NetworkFeedClose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("JordanNetworkOpen.eg")]
        public string NetworkJordanOpen {
            get {
                return ((string)(this["NetworkJordanOpen"]));
            }
            set {
                this["NetworkJordanOpen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("JordanNetworkHigh.eg")]
        public string NetworkJordanHigh {
            get {
                return ((string)(this["NetworkJordanHigh"]));
            }
            set {
                this["NetworkJordanHigh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("JordanNetworkLow.eg")]
        public string NetworkJordanLow {
            get {
                return ((string)(this["NetworkJordanLow"]));
            }
            set {
                this["NetworkJordanLow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("JordanNetworkClose.eg")]
        public string NetworkJordanClose {
            get {
                return ((string)(this["NetworkJordanClose"]));
            }
            set {
                this["NetworkJordanClose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("EURGBP")]
        public string Symbol {
            get {
                return ((string)(this["Symbol"]));
            }
            set {
                this["Symbol"] = value;
            }
        }
    }
}
