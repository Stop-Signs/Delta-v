using Content.Client.Hands.Systems;
using Content.Shared.Interaction;
using Content.Shared.RCD;
using Content.Shared.RCD.Components;
using Robust.Client.Placement;
using Robust.Client.Player;
using Robust.Shared.Enums;

namespace Content.Client.RCD;

/// <summary>
/// System for handling structure ghost placement in places where RCD can create objects.
/// </summary>
public sealed class RCDConstructionGhostSystem : EntitySystem
{
    private const string PlacementMode = nameof(AlignRCDConstruction);

    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly RCDSystem _rcdSystem = default!;
    [Dependency] private readonly IPlacementManager _placementManager = default!;
<<<<<<< HEAD

    private string _placementMode = typeof(AlignRCDConstruction).Name;
=======
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly HandsSystem _hands = default!;
    
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    private Direction _placementDirection = default;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        // Get current placer data
        var placerEntity = _placementManager.CurrentPermission?.MobUid;
        var placerProto = _placementManager.CurrentPermission?.EntityType;
        var placerIsRCD = HasComp<RCDComponent>(placerEntity);

        // Exit if erasing or the current placer is not an RCD (build mode is active)
        if (_placementManager.Eraser || (placerEntity != null && !placerIsRCD))
            return;

        // Determine if player is carrying an RCD in their active hand
        if (_playerManager.LocalSession?.AttachedEntity is not { } player)
            return;

        var heldEntity = _hands.GetActiveItem(player);

        if (!TryComp<RCDComponent>(heldEntity, out var rcd))
        {
            // If the player was holding an RCD, but is no longer, cancel placement
            if (placerIsRCD)
                _placementManager.Clear();

            return;
        }

        // Update the direction the RCD prototype based on the placer direction
        if (_placementDirection != _placementManager.Direction)
        {
            _placementDirection = _placementManager.Direction;
            RaiseNetworkEvent(new RCDConstructionGhostRotationEvent(GetNetEntity(heldEntity.Value), _placementDirection));
        }

        // If the placer has not changed, exit
        _rcdSystem.UpdateCachedPrototype(heldEntity.Value, rcd);

        if (heldEntity == placerEntity && rcd.CachedPrototype.Prototype == placerProto)
            return;

        // Create a new placer
        var newObjInfo = new PlacementInformation
        {
            MobUid = heldEntity.Value,
<<<<<<< HEAD
            PlacementOption = _placementMode,
            EntityType = rcd.CachedPrototype.Prototype,
=======
            PlacementOption = PlacementMode,
            EntityType = prototype.Prototype,
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
            Range = (int) Math.Ceiling(SharedInteractionSystem.InteractionRange),
            IsTile = (rcd.CachedPrototype.Mode == RcdMode.ConstructTile),
            UseEditorContext = false,
        };

        _placementManager.Clear();
        _placementManager.BeginPlacing(newObjInfo);
    }
}
