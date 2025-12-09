<<<<<<< HEAD
﻿using Content.Shared.Actions;
=======
using Content.Shared.Actions;
using Content.Shared.Actions.Components;
using Content.Shared.Charges.Components;
using Content.Shared.Charges.Systems;
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
using Content.Shared.DoAfter;
using Content.Shared.Interaction.Events;
using Content.Shared.Magic.Components;
using Content.Shared.Mind;
using Robust.Shared.Network;

namespace Content.Shared.Magic;

public sealed class SpellbookSystem : EntitySystem
{
    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly INetManager _netManager = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<SpellbookComponent, MapInitEvent>(OnInit, before: [typeof(SharedMagicSystem)]);
        SubscribeLocalEvent<SpellbookComponent, UseInHandEvent>(OnUse);
        SubscribeLocalEvent<SpellbookComponent, SpellbookDoAfterEvent>(OnDoAfter);
    }

    private void OnInit(Entity<SpellbookComponent> ent, ref MapInitEvent args)
    {
        foreach (var (id, charges) in ent.Comp.SpellActions)
        {
            var action = _actionContainer.AddAction(ent, id);
            if (action is not { } spell)
                continue;

<<<<<<< HEAD
            int? charge = charges;
            if (_actions.GetCharges(spell) != null)
                charge = _actions.GetCharges(spell);

            _actions.SetCharges(spell, charge < 0 ? null : charge);
            ent.Comp.Spells.Add(spell.Value);
=======
            // Null means infinite charges.
            if (charges is { } count)
            {
                EnsureComp<LimitedChargesComponent>(spell, out var chargeComp);
                _sharedCharges.SetMaxCharges((spell, chargeComp), count);
                _sharedCharges.SetCharges((spell, chargeComp), count);
            }

            ent.Comp.Spells.Add(spell);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        }
    }

    private void OnUse(Entity<SpellbookComponent> ent, ref UseInHandEvent args)
    {
        if (args.Handled)
            return;

        AttemptLearn(ent, args);

        args.Handled = true;
    }

    private void OnDoAfter<T>(Entity<SpellbookComponent> ent, ref T args) where T : DoAfterEvent // Sometimes i despise this language
    {
        if (args.Handled || args.Cancelled)
            return;

        args.Handled = true;

        if (!ent.Comp.LearnPermanently)
        {
            _actions.GrantActions(args.Args.User, ent.Comp.Spells, ent.Owner);
            return;
        }

        if (_mind.TryGetMind(args.Args.User, out var mindId, out _))
        {
            var mindActionContainerComp = EnsureComp<ActionsContainerComponent>(mindId);

            if (_netManager.IsServer)
                _actionContainer.TransferAllActionsWithNewAttached(ent, mindId, args.Args.User, newContainer: mindActionContainerComp);
        }
        else
        {
            foreach (var (id, charges) in ent.Comp.SpellActions)
            {
                EntityUid? actionId = null;
<<<<<<< HEAD
                if (_actions.AddAction(args.Args.User, ref actionId, id))
                    _actions.SetCharges(actionId, charges < 0 ? null : charges);
=======
                if (!_actions.AddAction(args.Args.User, ref actionId, id)
                    || charges is not { } count // Null means infinite charges
                    || !TryComp<LimitedChargesComponent>(actionId, out var chargeComp))
                    continue;

                _sharedCharges.SetMaxCharges((actionId.Value, chargeComp), count);
                _sharedCharges.SetCharges((actionId.Value, chargeComp), count);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
            }
        }

        ent.Comp.SpellActions.Clear();
    }

    private void AttemptLearn(Entity<SpellbookComponent> ent, UseInHandEvent args)
    {
        var doAfterEventArgs = new DoAfterArgs(EntityManager, args.User, ent.Comp.LearnTime, new SpellbookDoAfterEvent(), ent, target: ent)
        {
            BreakOnMove = true,
            BreakOnDamage = true,
            NeedHand = true, //What, are you going to read with your eyes only??
        };

        _doAfter.TryStartDoAfter(doAfterEventArgs);
    }
}
