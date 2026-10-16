using System.Linq;
using Content.Shared._DV.Demon;
using Robust.Client.GameObjects;
using Robust.Shared.GameObjects;

namespace Content.Client._DV.Demon;

public sealed partial class DemonAuraVisualsSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DemonAuraVisaulsComponent, AppearanceChangeEvent>(OnAppearanceChanged);
    }

    private void OnAppearanceChanged(EntityUid uid, DemonAuraVisaulsComponent comp, AppearanceChangeEvent args)
    {
        if (!AppearanceSystem.TryGetData<Color>(uid, "DemonAuraColor", out var color))
            return;

        if (!EntityManager.TryGetComponent(uid, out SpriteComponent? sprite))
            return;

        const int AuraLayer = 1;

        if (sprite.AllLayers.Count() <= AuraLayer)
            return;

        if (color == Color.Transparent)
        {
            sprite.LayerSetVisible(AuraLayer, false);
            return;
        }

        sprite.LayerSetColor(AuraLayer, color);
        sprite.LayerSetVisible(AuraLayer, true);
    }
}
