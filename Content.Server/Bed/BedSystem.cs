<<<<<<< HEAD
using Content.Server.Bed.Components;
<<<<<<< HEAD
using Content.Server.Body.Systems;
using Content.Server.Power.Components;
=======
>>>>>>> 496c0c511e446e3b6ce133b750e6003484d66e30
using Content.Server.Power.EntitySystems;
=======
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
using Content.Shared.Bed;
using Content.Shared.Bed.Sleep;
using Content.Shared.Buckle.Components;
using Content.Shared.Damage;
using Content.Shared.Mobs.Systems;
using Content.Shared.Power;
using Robust.Shared.Timing;
using Robust.Shared.Utility;
using Content.Shared._EE.Silicon.Components;

namespace Content.Server.Bed
{
    public sealed class BedSystem : EntitySystem
    {
        [Dependency] private readonly DamageableSystem _damageableSystem = default!;
<<<<<<< HEAD
        [Dependency] private readonly ActionsSystem _actionsSystem = default!;
        [Dependency] private readonly EmagSystem _emag = default!;
        [Dependency] private readonly SleepingSystem _sleepingSystem = default!;
        [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
=======
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        [Dependency] private readonly MobStateSystem _mobStateSystem = default!;
        [Dependency] private readonly IGameTiming _timing = default!;

        public override void Initialize()
        {
            base.Initialize();
<<<<<<< HEAD
            SubscribeLocalEvent<HealOnBuckleComponent, StrappedEvent>(OnStrapped);
            SubscribeLocalEvent<HealOnBuckleComponent, UnstrappedEvent>(OnUnstrapped);
            SubscribeLocalEvent<StasisBedComponent, StrappedEvent>(OnStasisStrapped);
            SubscribeLocalEvent<StasisBedComponent, UnstrappedEvent>(OnStasisUnstrapped);
            SubscribeLocalEvent<StasisBedComponent, PowerChangedEvent>(OnPowerChanged);
            SubscribeLocalEvent<StasisBedComponent, GotEmaggedEvent>(OnEmagged);
=======

            _sleepingQuery = GetEntityQuery<SleepingComponent>();
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        }

        private void OnStrapped(Entity<HealOnBuckleComponent> bed, ref StrappedEvent args)
        {
            EnsureComp<HealOnBuckleHealingComponent>(bed);
            bed.Comp.NextHealTime = _timing.CurTime + TimeSpan.FromSeconds(bed.Comp.HealTime);
            _actionsSystem.AddAction(args.Buckle, ref bed.Comp.SleepAction, SleepingSystem.SleepActionId, bed);

            // Single action entity, cannot strap multiple entities to the same bed.
            DebugTools.AssertEqual(args.Strap.Comp.BuckledEntities.Count, 1);
        }

        private void OnUnstrapped(Entity<HealOnBuckleComponent> bed, ref UnstrappedEvent args)
        {
            _actionsSystem.RemoveAction(args.Buckle, bed.Comp.SleepAction);
            _sleepingSystem.TryWaking(args.Buckle.Owner);
            RemComp<HealOnBuckleHealingComponent>(bed);
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            var query = EntityQueryEnumerator<HealOnBuckleHealingComponent, HealOnBuckleComponent, StrapComponent>();
            while (query.MoveNext(out var uid, out _, out var bedComponent, out var strapComponent))
            {
                if (_timing.CurTime < bedComponent.NextHealTime)
                    continue;

                bedComponent.NextHealTime += TimeSpan.FromSeconds(bedComponent.HealTime);

                if (strapComponent.BuckledEntities.Count == 0)
                    continue;

                foreach (var healedEntity in strapComponent.BuckledEntities)
                {
                    if (_mobStateSystem.IsDead(healedEntity)
                        || HasComp<SiliconComponent>(healedEntity)) // Goobstation
                        continue;

                    var damage = bedComponent.Damage;

                    if (HasComp<SleepingComponent>(healedEntity))
                        damage *= bedComponent.SleepMultiplier;

                    _damageableSystem.TryChangeDamage(healedEntity, damage, true, origin: uid);
                }
            }
        }
    }
}
