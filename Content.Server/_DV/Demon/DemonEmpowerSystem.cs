using System.Linq;
using Content.Shared._DV.Demon;
using Robust.Server.GameObjects;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._DV.Demon;

public sealed partial class DemonEmpowerSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearanceSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<DemonEmpowerableComponent, ComponentStartup>(OnStartup);
    }

    private void OnStartup(EntityUid uid, DemonEmpowerableComponent comp, ComponentStartup args)
    {
        // Fills dictionary with spells
        foreach (var proto in _prototypeManager.EnumeratePrototypes <DemonSpellProtoype>())
        {
            comp.SpellWeights[proto.ID] = proto.Weight;
        }
    }

    /// <summary>
    /// Call this when the caster empowers
    /// <summary>
    public void EnterEmpoweredState(EntityUid uid, DemonEmpowerableComponent comp, float duration)
    {
        comp.Empowered = true;
        comp.EmpowerTimeRemaining = duration;

        // Spell Picker
        var chosen = PickWeightedSpell(comp.SpellWeights);
        comp.CurrentSpell = chosen;

        var spell = _prototypeManager.Index<DemonSpellProtoype>(chosen);

        // Apply aura
        _appearanceSystem.SetData(uid, "DemonAuraColor" , spell.AuraColor);

        // Adds the ability
        EntityManager.AddComponent(uid, spell.AbilityComponent);
    }

    /// <summary>
    /// Random Weighted Selection
    /// <summary>
    private ProtoId<DemonSpellProtoype> PickWeightedSpell(
        Dictionary<ProtoId<DemonSpellProtoype>, float> weights)
    {
        float total = 0f;

        foreach (var w in weights.Values)
            total += w;

        var roll = _random.NextFloat() * total;

        foreach (var pair in weights)
        {
            roll -= pair.Value;
            if (roll <= 0)
                return pair.Key;
        }
        // Panic
        return weights.Keys.First();
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        // Empowerment timer
        foreach (var (uid, comp, _) in EntityQuery<DemonEmpowerableComponent>())
        {
            if (!comp.Empowered)
                continue;

            comp.EmpowerTimeRemaining -= frameTime;

            if (comp.EmpowerTimeRemaining <= 0)
            {
                ExitEmpoweredState(uid, comp);
            }
        }
    }

    private void ExitEmpoweredState(EntityUid uid, DemonEmpowerableComponent comp)
    {
        comp.Empowered = false;
        comp.EmpowerTimeRemaining = 0f;

        if (comp.CurrentSpell != null)
        {
            var spell = _prototypeManager.Index<DemonSpellProtoype>(comp.CurrentSpell.Value);

            if (EntityManager.HasComponent(uid, spell.AbilityComponent))
                EntityManager.RemoveComponent(uid, spell.AbilityComponent);
        }
        comp.CurrentSpell = null;

        // minus 2 billion aura
        _appearanceSystem.SetData(uid, "DemonAuraColor", null);
    }
}
