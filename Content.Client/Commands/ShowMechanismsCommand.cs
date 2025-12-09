using Content.Shared.Body.Organ;
using Robust.Client.GameObjects;
using Robust.Shared.Console;

namespace Content.Client.Commands;

public sealed class ShowMechanismsCommand : LocalizedEntityCommands
{
    [Dependency] private readonly SpriteSystem _spriteSystem = default!;

    public override string Command => "showmechanisms";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
<<<<<<< HEAD
        var query = _entManager.AllEntityQueryEnumerator<OrganComponent, SpriteComponent>();
=======
        var query = EntityManager.AllEntityQueryEnumerator<OrganComponent, SpriteComponent>();
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

        while (query.MoveNext(out _, out var sprite))
        {
<<<<<<< HEAD
            sprite.ContainerOccluded = false;
=======
            _spriteSystem.SetContainerOccluded((uid, sprite), false);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        }
    }
}
