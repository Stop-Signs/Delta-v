using System.Text.RegularExpressions;
using Content.Shared.Tools;
using Content.Shared.Tools.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Configurable
{
<<<<<<< HEAD
    [RegisterComponent, NetworkedComponent]
=======
    /// <summary>
    /// Configuration for mailing units.
    /// </summary>
    /// <remarks>
    /// If you want a more detailed description ask the original coder.
    /// </remarks>
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    public sealed partial class ConfigurationComponent : Component
    {
        [DataField("config")]
        public Dictionary<string, string?> Config = new();

        [DataField("qualityNeeded", customTypeSerializer: typeof(PrototypeIdSerializer<ToolQualityPrototype>))]
        public string QualityNeeded = SharedToolSystem.PulseQuality;

        [DataField("validation")]
        public Regex Validation = new("^[a-zA-Z0-9 ]*$", RegexOptions.Compiled);

        [Serializable, NetSerializable]
        public sealed class ConfigurationBoundUserInterfaceState : BoundUserInterfaceState
        {
            public Dictionary<string, string?> Config { get; }

            public ConfigurationBoundUserInterfaceState(Dictionary<string, string?> config)
            {
                Config = config;
            }
        }

        /// <summary>
        ///     Message data sent from client to server when the device configuration is updated.
        /// </summary>
        [Serializable, NetSerializable]
        public sealed class ConfigurationUpdatedMessage : BoundUserInterfaceMessage
        {
            public Dictionary<string, string> Config { get; }

            public ConfigurationUpdatedMessage(Dictionary<string, string> config)
            {
                Config = config;
            }
        }

        [Serializable, NetSerializable]
        public sealed class ValidationUpdateMessage : BoundUserInterfaceMessage
        {
            public string ValidationString { get; }

            public ValidationUpdateMessage(string validationString)
            {
                ValidationString = validationString;
            }
        }

        [Serializable, NetSerializable]
        public enum ConfigurationUiKey
        {
            Key
        }
    }
}
