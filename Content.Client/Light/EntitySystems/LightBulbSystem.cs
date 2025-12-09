using Content.Shared.Light.Components;
using Content.Shared.Light.EntitySystems;
using Robust.Client.GameObjects;

namespace Content.Client.Light.EntitySystems;

public sealed class LightBulbSystem : SharedLightBulbSystem
{
    [Dependency] private readonly AppearanceSystem _appearance = default!;
    [Dependency] private readonly SpriteSystem _sprite = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LightBulbComponent, AppearanceChangeEvent>(OnAppearanceChange);
    }

    private void OnAppearanceChange(EntityUid uid, LightBulbComponent comp, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        // update sprite state
        if (_appearance.TryGetData<LightBulbState>(uid, LightBulbVisuals.State, out var state, args.Component))
        {
            switch (state)
            {
                case LightBulbState.Normal:
<<<<<<< HEAD
<<<<<<< HEAD
                    args.Sprite.LayerSetState(LightBulbVisualLayers.Base, comp.NormalSpriteState);
                    break;
                case LightBulbState.Broken:
                    args.Sprite.LayerSetState(LightBulbVisualLayers.Base, comp.BrokenSpriteState);
                    break;
                case LightBulbState.Burned:
                    args.Sprite.LayerSetState(LightBulbVisualLayers.Base, comp.BurnedSpriteState);
=======
                    SpriteSystem.LayerSetRsiState((uid, args.Sprite), LightBulbVisualLayers.Base, comp.NormalSpriteState);
=======
                    _sprite.LayerSetRsiState((uid, args.Sprite), LightBulbVisualLayers.Base, comp.NormalSpriteState);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
                    break;
                case LightBulbState.Broken:
                    _sprite.LayerSetRsiState((uid, args.Sprite), LightBulbVisualLayers.Base, comp.BrokenSpriteState);
                    break;
                case LightBulbState.Burned:
<<<<<<< HEAD
                    SpriteSystem.LayerSetRsiState((uid, args.Sprite), LightBulbVisualLayers.Base, comp.BurnedSpriteState);
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
=======
                    _sprite.LayerSetRsiState((uid, args.Sprite), LightBulbVisualLayers.Base, comp.BurnedSpriteState);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
                    break;
            }
        }

        // also update sprites color
        if (_appearance.TryGetData<Color>(uid, LightBulbVisuals.Color, out var color, args.Component))
        {
<<<<<<< HEAD
<<<<<<< HEAD
            args.Sprite.Color = color;
=======
            SpriteSystem.SetColor((uid, args.Sprite), color);
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
=======
            _sprite.SetColor((uid, args.Sprite), color);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        }
    }
}
