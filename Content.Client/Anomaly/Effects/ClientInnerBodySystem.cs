using Content.Shared.Anomaly.Components;
using Content.Shared.Anomaly.Effects;
using Content.Shared.Humanoid;
using Robust.Client.GameObjects;

namespace Content.Client.Anomaly.Effects;

public sealed class ClientInnerBodyAnomalySystem : SharedInnerBodyAnomalySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<InnerBodyAnomalyComponent, AfterAutoHandleStateEvent>(OnAfterHandleState);
        SubscribeLocalEvent<InnerBodyAnomalyComponent, ComponentShutdown>(OnCompShutdown);
    }

    private void OnAfterHandleState(Entity<InnerBodyAnomalyComponent> ent, ref AfterAutoHandleStateEvent args)
    {
        if (!TryComp<SpriteComponent>(ent, out var sprite))
            return;

        if (ent.Comp.FallbackSprite is null)
            return;

        if (!sprite.LayerMapTryGet(ent.Comp.LayerMap, out var index))
            index = sprite.LayerMapReserveBlank(ent.Comp.LayerMap);

        if (TryComp<HumanoidAppearanceComponent>(ent, out var humanoidAppearance) &&
            ent.Comp.SpeciesSprites.TryGetValue(humanoidAppearance.Species, out var speciesSprite))
        {
            sprite.LayerSetSprite(index, speciesSprite);
        }
        else
        {
            sprite.LayerSetSprite(index, ent.Comp.FallbackSprite);
        }

        sprite.LayerSetVisible(index, true);
        sprite.LayerSetShader(index, "unshaded");
    }

    private void OnCompShutdown(Entity<InnerBodyAnomalyComponent> ent, ref ComponentShutdown args)
    {
        if (!TryComp<SpriteComponent>(ent, out var sprite))
            return;

<<<<<<< HEAD
        var index = sprite.LayerMapGet(ent.Comp.LayerMap);
        sprite.LayerSetVisible(index, false);
=======
        if (sprite.LayerMapTryGet(ent.Comp.LayerMap, out var index)) // imp. added this check to prevent errors on anomalites - not having it was bad code on upstream's part
            sprite.LayerSetVisible(index, false);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    }
}
