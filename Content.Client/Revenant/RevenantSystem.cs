using Content.Client.Alerts;
using Content.Shared.Alert;
using Content.Shared.Alert.Components;
using Content.Shared.Revenant;
using Content.Shared.Revenant.Components;
using Robust.Client.GameObjects;

namespace Content.Client.Revenant;

public sealed class RevenantSystem : EntitySystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RevenantComponent, AppearanceChangeEvent>(OnAppearanceChange);
        SubscribeLocalEvent<RevenantComponent, GetGenericAlertCounterAmountEvent>(OnGetCounterAmount);
    }

    private void OnAppearanceChange(EntityUid uid, RevenantComponent component, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        if (_appearance.TryGetData<bool>(uid, RevenantVisuals.Harvesting, out var harvesting, args.Component) && harvesting)
        {
            args.Sprite.LayerSetState(0, component.HarvestingState);
        }
        else if (_appearance.TryGetData<bool>(uid, RevenantVisuals.Stunned, out var stunned, args.Component) && stunned)
        {
            args.Sprite.LayerSetState(0, component.StunnedState);
        }
        else if (_appearance.TryGetData<bool>(uid, RevenantVisuals.Corporeal, out var corporeal, args.Component))
        {
            if (corporeal)
                args.Sprite.LayerSetState(0, component.CorporealState);
            else
                args.Sprite.LayerSetState(0, component.State);
        }
    }

    private void OnGetCounterAmount(Entity<RevenantComponent> ent, ref GetGenericAlertCounterAmountEvent args)
    {
        if (args.Handled)
            return;

<<<<<<< HEAD
        var sprite = args.SpriteViewEnt.Comp;
        var essence = Math.Clamp(ent.Comp.Essence.Int(), 0, 999);
        sprite.LayerSetState(RevenantVisualLayers.Digit1, $"{(essence / 100) % 10}");
        sprite.LayerSetState(RevenantVisualLayers.Digit2, $"{(essence / 10) % 10}");
        sprite.LayerSetState(RevenantVisualLayers.Digit3, $"{essence % 10}");
=======
        if (ent.Comp.EssenceAlert != args.Alert)
            return;

        args.Amount = ent.Comp.Essence.Int();
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    }
}
