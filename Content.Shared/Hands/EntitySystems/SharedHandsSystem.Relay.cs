<<<<<<< HEAD
=======
using Content.Shared.Armor; // DeltaV - Addition of HandHeldArmor
using Content.Shared.Atmos;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
using Content.Shared.Camera;
using Content.Shared.Cuffs;
using Content.Shared.Damage; // DeltaV End - Addition of HandHeldArmor
using Content.Shared.Hands.Components;
using Content.Shared.Movement.Systems;
<<<<<<< HEAD
=======
using Content.Shared.Projectiles;
using Content.Shared.Weapons.Ranged.Events;
using Content.Shared.Wieldable;
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30

namespace Content.Shared.Hands.EntitySystems;

public abstract partial class SharedHandsSystem
{
    private void InitializeRelay()
    {
        SubscribeLocalEvent<HandsComponent, GetEyeOffsetRelayedEvent>(RelayEvent);
        SubscribeLocalEvent<HandsComponent, GetEyePvsScaleRelayedEvent>(RelayEvent);
        SubscribeLocalEvent<HandsComponent, RefreshMovementSpeedModifiersEvent>(RelayEvent);
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
        // DeltaV Start - Addition of HandHeldArmor
        SubscribeLocalEvent<HandsComponent, CoefficientQueryEvent>(RelayEvent);
        SubscribeLocalEvent<HandsComponent, DamageModifyEvent>(RelayEvent);
        // DeltaV End - Addition of HandHeldArmor
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d

        // By-ref events.
        SubscribeLocalEvent<HandsComponent, ExtinguishEvent>(RefRelayEvent);
        SubscribeLocalEvent<HandsComponent, ProjectileReflectAttemptEvent>(RefRelayEvent);
        SubscribeLocalEvent<HandsComponent, HitScanReflectAttemptEvent>(RefRelayEvent);
        SubscribeLocalEvent<HandsComponent, WieldAttemptEvent>(RefRelayEvent);
        SubscribeLocalEvent<HandsComponent, UnwieldAttemptEvent>(RefRelayEvent);
<<<<<<< HEAD
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
=======
        SubscribeLocalEvent<HandsComponent, TargetHandcuffedEvent>(RefRelayEvent);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    }

    private void RelayEvent<T>(Entity<HandsComponent> entity, ref T args) where T : EntityEventArgs
    {
        var ev = new HeldRelayedEvent<T>(args);
<<<<<<< HEAD
        foreach (var held in EnumerateHeld(entity, entity.Comp))
=======

        foreach (var held in EnumerateHeld(entity.AsNullable()))
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        {
            RaiseLocalEvent(held, ref ev);
        }
    }
}
