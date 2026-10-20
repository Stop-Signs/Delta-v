using Content.Shared._DV.Demon;
using Content.Shared._DV.Demon.Abilities;
using Microsoft.Extensions.Logging;
using Robust.Shared.GameObjects;

namespace Content.Server._DV.Demon;

public sealed partial class DemonEmpowerAbilitySystem : EntitySystem
{
    [Dependency] private readonly DemonEmpowerSystem _empower = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        foreach (var (uid, comp) in EntityQuery<DemonEmpowerAbilityComponent>())
        {
            if (comp.CooldownRemaining > 0)
                return;

            if (EntityManager.TryGetComponent(uid, out DemonEmpowerableComponent? empowerable))
            {
                _empower.EnterEmpoweredState(uid, empowerable, comp.EmpowerDuration);
            }

            // Start Cooldown
            comp.CooldownRemaining = comp.CooldownRemaining;
        }
    }
}
