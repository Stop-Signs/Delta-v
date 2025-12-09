using Content.Shared.Item.ItemToggle.Components;

namespace Content.Shared.Item.ItemToggle;

/// <summary>
/// Handles <see cref="ComponentTogglerComponent"/> component manipulation.
/// </summary>
public sealed class ComponentTogglerSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ComponentTogglerComponent, ItemToggledEvent>(OnToggled);
    }

    private void OnToggled(Entity<ComponentTogglerComponent> ent, ref ItemToggledEvent args)
    {
        var target = ent.Comp.Parent ? Transform(ent).ParentUid : ent.Owner;

        if (args.Activated)
<<<<<<< HEAD
            EntityManager.AddComponents(target, ent.Comp.Components);
<<<<<<< HEAD
        else
            EntityManager.RemoveComponents(target, ent.Comp.RemoveComponents ?? ent.Comp.Components);
=======
=======
        {
            var target = ent.Comp.Parent ? Transform(ent).ParentUid : ent.Owner;

            if (TerminatingOrDeleted(target))
                return;

            ent.Comp.Target = target;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

            // Begin DeltaV - allow swapping components
            if (ent.Comp.DeactivatedComponents is { } deactivatedComps)
                EntityManager.RemoveComponents(target, deactivatedComps);

            EntityManager.AddComponents(target, ent.Comp.Components);
            // End DeltaV
        }
        else
        {
            if (ent.Comp.Target == null)
                return;

            if (TerminatingOrDeleted(ent.Comp.Target.Value))
                return;

            EntityManager.RemoveComponents(ent.Comp.Target.Value, ent.Comp.RemoveComponents ?? ent.Comp.Components);

            // Begin DeltaV - allow swapping components
            if (ent.Comp.DeactivatedComponents is { } reactivatedComps)
                EntityManager.AddComponents(ent.Comp.Target.Value, reactivatedComps);
            // End DeltaV
        }
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
    }
}
