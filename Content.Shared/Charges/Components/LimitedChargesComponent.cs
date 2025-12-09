using Content.Shared.Charges.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared.Charges.Components;

[RegisterComponent, NetworkedComponent]
[Access(typeof(SharedChargesSystem))]
[AutoGenerateComponentState]
public sealed partial class LimitedChargesComponent : Component
{
    /// <summary>
    /// The maximum number of charges
    /// </summary>
<<<<<<< HEAD
    [DataField("maxCharges"), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
=======
    [DataField, AutoNetworkedField]
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    public int MaxCharges = 3;

    /// <summary>
    /// The current number of charges
    /// </summary>
    [DataField("charges"), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public int Charges = 3;
}
