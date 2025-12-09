using Content.Shared.Mind;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Administration;

[Serializable, NetSerializable]
public sealed record PlayerInfo(
    string Username,
    string CharacterName,
    string IdentityName,
    string StartingJob,
    bool Antag,
<<<<<<< HEAD
    RoleTypePrototype RoleProto,
=======
    ProtoId<RoleTypePrototype>? RoleProto,
    LocId? Subtype,
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    int SortWeight,
    NetEntity? NetEntity,
    NetUserId SessionId,
    bool Connected,
    bool ActiveThisRound,
    TimeSpan? OverallPlaytime)
{
    private string? _playtimeString;

    public bool IsPinned { get; set; }

    public string PlaytimeString => _playtimeString ??=
        OverallPlaytime?.ToString("%d':'hh':'mm") ?? Loc.GetString("generic-unknown-title");

    public bool Equals(PlayerInfo? other)
    {
        return other?.SessionId == SessionId;
    }

    public override int GetHashCode()
    {
        return SessionId.GetHashCode();
    }
}
