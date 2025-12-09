using Content.Server.Administration.Logs;
using Content.Server.Power.Components;
using Content.Shared.Database;
using Content.Shared.Interaction;
using Content.Shared.Maps;
using Content.Shared.Stacks;
using Robust.Shared.Map.Components;

namespace Content.Server.Power.EntitySystems;

public sealed partial class CableSystem
{
    [Dependency] private readonly IAdminLogManager _adminLogger = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly SharedMapSystem _map = default!;

    private void InitializeCablePlacer()
    {
        SubscribeLocalEvent<CablePlacerComponent, AfterInteractEvent>(OnCablePlacerAfterInteract);
    }

    private void OnCablePlacerAfterInteract(Entity<CablePlacerComponent> placer, ref AfterInteractEvent args)
    {
        if (args.Handled || !args.CanReach)
            return;

        var component = placer.Comp;
        if (component.CablePrototypeId == null)
            return;

        if(!TryComp<MapGridComponent>(_transform.GetGrid(args.ClickLocation), out var grid))
            return;

        var gridUid = _transform.GetGrid(args.ClickLocation)!.Value;
        var snapPos = grid.TileIndicesFor(args.ClickLocation);
        var tileDef = (ContentTileDefinition) _tileManager[_map.GetTileRef(gridUid, grid,snapPos).Tile.TypeId];

        if (!tileDef.IsSubFloor || !tileDef.Sturdy)
            return;

        foreach (var anchored in grid.GetAnchoredEntities(snapPos))
        {
            if (TryComp<CableComponent>(anchored, out var wire) && wire.CableType == component.BlockingCableType)
                return;
        }

        if (TryComp<StackComponent>(placer, out var stack) && !_stack.Use(placer, 1, stack))
            return;

<<<<<<< HEAD
        var newCable = EntityManager.SpawnEntity(component.CablePrototypeId, grid.GridTileToLocal(snapPos));
=======
        var newCable = Spawn(component.CablePrototypeId, _map.GridTileToLocal(gridUid, grid, snapPos));
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        _adminLogger.Add(LogType.Construction, LogImpact.Low,
            $"{ToPrettyString(args.User):player} placed {ToPrettyString(newCable):cable} at {Transform(newCable).Coordinates}");
        args.Handled = true;
    }
}
