using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.Demon;

[RegisterComponent] [NetworkedComponent]
public sealed partial class DemonEmpowerableComponent : Component
{
    [DataField]
    public bool Empowered = false;

    [DataField]
    public float EmpowerTimeRemaining = 0f;

    [DataField]
    public string? CurrentSpell = null;

    [DataField]
    public Dictionary<ProtoId<DemonSpellProtoype>, float> SpellWeights = new();
}
