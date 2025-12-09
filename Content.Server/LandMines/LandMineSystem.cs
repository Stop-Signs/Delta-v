<<<<<<< HEAD
using Content.Server.Explosion.EntitySystems;
using Content.Shared.Popups;
using Content.Shared.StepTrigger.Systems;
using Robust.Shared.Audio;
=======
using Content.Shared.Armable;
using Content.Shared.Item.ItemToggle.Components;
using Content.Shared.LandMines;
using Content.Shared.Popups;
using Content.Shared.StepTrigger.Systems;
using Content.Shared.Trigger.Systems;
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
using Robust.Shared.Audio.Systems;

namespace Content.Server.LandMines;

public sealed class LandMineSystem : EntitySystem
{
    [Dependency] private readonly SharedAudioSystem _audioSystem = default!;
    [Dependency] private readonly SharedPopupSystem _popupSystem = default!;
    [Dependency] private readonly TriggerSystem _trigger = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<LandMineComponent, StepTriggeredOnEvent>(HandleStepOnTriggered);
        SubscribeLocalEvent<LandMineComponent, StepTriggeredOffEvent>(HandleStepOffTriggered);

        SubscribeLocalEvent<LandMineComponent, StepTriggerAttemptEvent>(HandleStepTriggerAttempt);
    }

    private void HandleStepOnTriggered(EntityUid uid, LandMineComponent component, ref StepTriggeredOnEvent args)
    {
<<<<<<< HEAD
        _popupSystem.PopupCoordinates(
            Loc.GetString("land-mine-triggered", ("mine", uid)),
            Transform(uid).Coordinates,
            args.Tripper,
            PopupType.LargeCaution);

=======
        if (!string.IsNullOrEmpty(component.TriggerText))
        {
            _popupSystem.PopupCoordinates(
                Loc.GetString(component.TriggerText, ("mine", uid)),
                Transform(uid).Coordinates,
                args.Tripper,
                PopupType.LargeCaution);
        }
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
        _audioSystem.PlayPvs(component.Sound, uid);
    }

    private void HandleStepOffTriggered(EntityUid uid, LandMineComponent component, ref StepTriggeredOffEvent args)
    {
        // TODO: Adjust to the new trigger system
        _trigger.Trigger(uid, args.Tripper, TriggerSystem.DefaultTriggerKey);
    }

    private static void HandleStepTriggerAttempt(EntityUid uid, LandMineComponent component, ref StepTriggerAttemptEvent args)
    {
        args.Continue = true;
    }
}
