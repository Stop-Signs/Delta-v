using Content.Shared.Body.Organ;
using Robust.Client.GameObjects;
using Robust.Shared.Console;
using Robust.Shared.Containers;

namespace Content.Client.Commands;

public sealed class HideMechanismsCommand : LocalizedEntityCommands
{
    [Dependency] private readonly SharedContainerSystem _containerSystem = default!;
    [Dependency] private readonly SpriteSystem _spriteSystem = default!;

    public override string Command => "hidemechanisms";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
<<<<<<< HEAD
        var containerSys = _entityManager.System<SharedContainerSystem>();
        var query = _entityManager.AllEntityQueryEnumerator<OrganComponent>();
=======
        var query = EntityManager.AllEntityQueryEnumerator<OrganComponent, SpriteComponent>();
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

        while (query.MoveNext(out var uid, out _))
        {
<<<<<<< HEAD
            if (!_entityManager.TryGetComponent(uid, out SpriteComponent? sprite))
            {
                continue;
            }

            sprite.ContainerOccluded = false;
=======
            _spriteSystem.SetContainerOccluded((uid, sprite), false);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

            var tempParent = uid;
            while (_containerSystem.TryGetContainingContainer((tempParent, null, null), out var container))
            {
                if (!container.ShowContents)
                {
<<<<<<< HEAD
                    sprite.ContainerOccluded = true;
=======
                    _spriteSystem.SetContainerOccluded((uid, sprite), true);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
                    break;
                }

                tempParent = container.Owner;
            }
        }
    }
}
