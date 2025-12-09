<<<<<<< HEAD
﻿using Content.Shared.StatusIcon;
=======
using Content.Shared.CCVar;
using Content.Shared.StatusIcon;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.SSDIndicator;

/// <summary>
/// Shows status icon when an entity is SSD, based on if a player is attached or not.
/// </summary>
[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class SSDIndicatorComponent : Component
{
    /// <summary>
    /// Whether or not the entity is SSD.
    /// </summary>
    [AutoNetworkedField]
    [DataField, ViewVariables(VVAccess.ReadOnly)]
    public bool IsSSD = true;

    /// <summary>
    /// The icon displayed next to the associated entity when it is SSD.
    /// </summary>
    [DataField]
    public ProtoId<SsdIconPrototype> Icon = "SSDIcon";
<<<<<<< HEAD
=======

    /// <summary>
    /// The time at which the entity will fall asleep, if <see cref="CCVars.ICSSDSleep"/> is true.
    /// </summary>
    [AutoNetworkedField, AutoPausedField]
    [Access(typeof(SSDIndicatorSystem))]
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan FallAsleepTime = TimeSpan.Zero;

    /// <summary>
    /// The next time this component will be updated.
    /// </summary>
    [AutoNetworkedField, AutoPausedField]
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan NextUpdate = TimeSpan.Zero;

    /// <summary>
    /// The time between updates checking if the entity should be force slept.
    /// </summary>
    [DataField]
    public TimeSpan UpdateInterval = TimeSpan.FromSeconds(1);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
}
