namespace Content.Server.Warps
{
<<<<<<< HEAD:Content.Server/Warps/WarpPointComponent.cs
=======
    [DataField]
    public string? Location;

>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d:Content.Shared/Warps/WarpPointComponent.cs
    /// <summary>
    /// Allows ghosts etc to warp to this entity by name.
    /// </summary>
    [RegisterComponent]
    public sealed partial class WarpPointComponent : Component
    {
        [ViewVariables(VVAccess.ReadWrite), DataField]
        public string? Location;

        /// <summary>
        ///     If true, ghosts warping to this entity will begin following it.
        /// </summary>
        [DataField]
        public bool Follow;
    }
}
