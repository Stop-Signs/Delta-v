using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.Fluids.Components;

/// <summary>
/// Added to puddles that contain water so it may evaporate over time.
/// </summary>
[NetworkedComponent, AutoGenerateComponentPause]
[RegisterComponent, Access(typeof(SharedPuddleSystem))]
public sealed partial class EvaporationComponent : Component
{
    /// <summary>
    /// The next time we remove the EvaporationSystem reagent amount from this entity.
    /// </summary>
    [AutoPausedField, DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan NextTick;

    /// <summary>
    /// How much evaporation per second.
    /// </summary>
<<<<<<< HEAD
    [DataField("evaporationAmount")]
    public FixedPoint2 EvaporationAmount = FixedPoint2.New(0.3);
=======
    [DataField]
    public FixedPoint2 EvaporationAmount = FixedPoint2.New(1);

    /// <summary>
    /// The effect spawned when the puddle fully evaporates.
    /// </summary>
    [DataField]
    public EntProtoId EvaporationEffect = "PuddleSparkle";
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
}
