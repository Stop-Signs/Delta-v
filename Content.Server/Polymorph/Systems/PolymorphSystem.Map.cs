using Content.Shared.GameTicking;

namespace Content.Server.Polymorph.Systems;

public sealed partial class PolymorphSystem
{
    public EntityUid? PausedMap { get; private set; }

    /// <summary>
    /// Used to subscribe to the round restart event
    /// </summary>
    private void InitializeMap()
    {
        SubscribeLocalEvent<RoundRestartCleanupEvent>(OnRoundRestart);
    }

    private void OnRoundRestart(RoundRestartCleanupEvent _)
    {
        if (PausedMap == null || !Exists(PausedMap))
            return;

        Del(PausedMap.Value);
    }

    /// <summary>
    /// Used internally to ensure a paused map that is
    /// stores polymorphed entities.
    /// </summary>
    private void EnsurePausedMap()
    {
        if (PausedMap != null && Exists(PausedMap))
            return;

<<<<<<< HEAD
        var newmap = _mapManager.CreateMap();
        _mapManager.SetMapPaused(newmap, true);
        PausedMap = _mapManager.GetMapEntityId(newmap);
=======
        var mapUid = _map.CreateMap();
        _metaData.SetEntityName(mapUid, Loc.GetString("polymorph-paused-map-name"));
        _map.SetPaused(mapUid, true);
        PausedMap = mapUid;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    }
}
