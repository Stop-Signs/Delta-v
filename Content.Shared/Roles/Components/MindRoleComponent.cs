using Content.Shared.Mind;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Roles.Components;

/// <summary>
/// This holds data for, and indicates, a Mind Role entity
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class MindRoleComponent : BaseMindRoleComponent
{
    /// <summary>
    /// Marks this Mind Role as Antagonist.
    /// A single antag Mind Role is enough to make the owner mind count as Antagonist.
    /// </summary>
    [DataField]
    public bool Antag { get; set; } = false;

    /// <summary>
    /// The mind's current antagonist/special role, or lack thereof.
    /// </summary>
    [DataField]
    public ProtoId<RoleTypePrototype>? RoleType;

    /// <summary>
<<<<<<< HEAD:Content.Shared/Roles/MindRoleComponent.cs
    ///     True if this mindrole is an exclusive antagonist. Antag setting is not checked if this is True.
=======
    /// The role's subtype, shown only to admins to help with antag categorization.
    /// </summary>
    [DataField]
    public LocId? Subtype;

    /// <summary>
    /// True if this mindrole is an exclusive antagonist. Antag setting is not checked if this is True.
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d:Content.Shared/Roles/Components/MindRoleComponent.cs
    /// </summary>
    [DataField]
    public bool ExclusiveAntag { get; set; } = false;

    /// <summary>
    /// The Antagonist prototype of this role.
    /// </summary>
    [DataField]
    public ProtoId<AntagPrototype>? AntagPrototype;

    /// <summary>
    /// The Job prototype of this role.
    /// </summary>
    [DataField]
    public ProtoId<JobPrototype>? JobPrototype;

    /// <summary>
    /// Used to order the characters on by role/antag status. Highest numbers are shown first.
    /// </summary>
    [DataField]
    public int SortWeight;
}

// Why does this base component actually exist? It does make auto-categorization easy, but before that it was useless?
// I used it for easy organisation/bookkeeping of what components are for mindroles
[EntityCategory("Roles")]
public abstract partial class BaseMindRoleComponent : Component;
