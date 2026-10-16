using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;

namespace Content.Shared._DV.Demon.Abilities;

[RegisterComponent] [NetworkedComponent]
public sealed partial class DemonEmpowerAbilityComponent : Component
{
    [DataField]
    public float CooldownRemaining = 0f;

    [DataField]
    public float EmpowerDuration = 20f;

    [DataField]
    public float EmpowerCooldown = 15f;
}
