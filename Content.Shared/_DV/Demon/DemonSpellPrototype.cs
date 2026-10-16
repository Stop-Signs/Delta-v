using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared._DV.Demon;

[Prototype("demonSpell")]
public sealed partial class DemonSpellProtoype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField("auraColor")]
    public Color AuraColor { get; private set; }

    [DataField("ability")]
    public string AbilityComponent  { get; private set; } = default!;

    [DataField("weight")]
    public float Weight { get; private set; } = 1f;
}
