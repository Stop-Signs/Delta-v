using Content.Shared.Charges.Components;
using Content.Shared.Examine;

namespace Content.Shared.Charges.Systems;

public abstract class SharedChargesSystem : EntitySystem
{
    protected EntityQuery<LimitedChargesComponent> Query;

    public override void Initialize()
    {
        base.Initialize();

        Query = GetEntityQuery<LimitedChargesComponent>();

        SubscribeLocalEvent<LimitedChargesComponent, ExaminedEvent>(OnExamine);
    }

    protected virtual void OnExamine(EntityUid uid, LimitedChargesComponent comp, ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        using (args.PushGroup(nameof(LimitedChargesComponent)))
        {
            args.PushMarkup(Loc.GetString("limited-charges-charges-remaining", ("charges", comp.Charges)));
            if (comp.Charges == comp.MaxCharges)
            {
                args.PushMarkup(Loc.GetString("limited-charges-max-charges"));
            }
        }
    }

    /// <summary>
    /// Tries to add a number of charges. If it over or underflows it will be clamped, wasting the extra charges.
    /// </summary>
<<<<<<< HEAD
    public virtual void AddCharges(EntityUid uid, int change, LimitedChargesComponent? comp = null)
=======
    /// <param name="action">
    /// The action to add charges to. If it doesn't have <see cref="LimitedChargesComponent"/>, it will be added.
    /// </param>
    /// <param name="addCharges">
    /// The number of charges to add. Can be negative. Resulting charge count is clamped to [0, MaxCharges].
    /// </param>
    public void AddCharges(Entity<LimitedChargesComponent?, AutoRechargeComponent?> action, int addCharges)
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    {
        if (!Query.Resolve(uid, ref comp, false))
            return;

        var old = comp.Charges;
        comp.Charges = Math.Clamp(comp.Charges + change, 0, comp.MaxCharges);
        if (comp.Charges != old)
            Dirty(uid, comp);
    }

    /// <summary>
    /// Gets the limited charges component and returns true if there are no charges. Will return false if there is no limited charges component.
    /// </summary>
    public bool IsEmpty(EntityUid uid, LimitedChargesComponent? comp = null)
    {
        // can't be empty if there are no limited charges
        if (!Query.Resolve(uid, ref comp, false))
            return false;

        return comp.Charges <= 0;
    }

    /// <summary>
    /// Uses a single charge. Must check IsEmpty beforehand to prevent using with 0 charge.
    /// </summary>
    public void UseCharge(EntityUid uid, LimitedChargesComponent? comp = null)
    {
        AddCharges(uid, -1, comp);
    }

    /// <summary>
    /// Checks IsEmpty and uses a charge if it isn't empty.
    /// </summary>
    public bool TryUseCharge(Entity<LimitedChargesComponent?> ent)
    {
        if (!Query.Resolve(ent, ref ent.Comp, false))
            return true;

        if (IsEmpty(ent, ent.Comp))
            return false;

        UseCharge(ent, ent.Comp);
        return true;
    }

    /// <summary>
    /// Gets the limited charges component and returns true if the number of charges remaining is less than the specified value.
    /// Will return false if there is no limited charges component.
    /// </summary>
    public bool HasInsufficientCharges(EntityUid uid, int requiredCharges, LimitedChargesComponent? comp = null)
    {
        // can't be empty if there are no limited charges
        if (!Resolve(uid, ref comp, false))
            return false;

        return comp.Charges < requiredCharges;
    }

    /// <summary>
    /// Uses up a specified number of charges. Must check HasInsufficentCharges beforehand to prevent using with insufficient remaining charges.
    /// </summary>
    public virtual void UseCharges(EntityUid uid, int chargesUsed, LimitedChargesComponent? comp = null)
    {
<<<<<<< HEAD
        AddCharges(uid, -chargesUsed, comp);
=======
        if (!Resolve(action.Owner, ref action.Comp, false))
            return;

        var charges = GetCurrentCharges((action.Owner, action.Comp, null));

        if (charges == action.Comp.MaxCharges)
            return;

        action.Comp.LastCharges = action.Comp.MaxCharges;
        action.Comp.LastUpdate = _timing.CurTime;
        Dirty(action);
    }

    /// <summary>
    /// Set the number of charges an action has.
    /// </summary>
    /// <param name="action">The action in question</param>
    /// <param name="value">
    /// The number of charges. Clamped to [0, MaxCharges].
    /// </param>
    /// <remarks>
    /// This method doesn't implicitly add <see cref="LimitedChargesComponent"/>
    /// unlike some other methods in this system.
    /// </remarks>
    public void SetCharges(Entity<LimitedChargesComponent?> action, int value)
    {
        if (!Resolve(action, ref action.Comp))
            return;

        var adjusted = Math.Clamp(value, 0, action.Comp.MaxCharges);

        if (action.Comp.LastCharges == adjusted)
        {
            return;
        }

        action.Comp.LastCharges = adjusted;
        action.Comp.LastUpdate = _timing.CurTime;
        Dirty(action);
    }

    /// <summary>
    /// Sets the maximum charges of a given action.
    /// </summary>
    /// <param name="action">The action being modified.</param>
    /// <param name="value">The new maximum charges of the action. Clamped to zero.</param>
    /// <remarks>
    /// Does not change the current charge count, or adjust the
    /// accumulator for auto-recharge. It also doesn't implicitly add
    /// <see cref="LimitedChargesComponent"/> unlike some other methods
    /// in this system.
    /// </remarks>
    public void SetMaxCharges(Entity<LimitedChargesComponent?> action, int value)
    {
        if (!Resolve(action, ref action.Comp))
            return;

        // You can't have negative max charges (even zero is a bit goofy but eh)
        var adjusted = Math.Max(0, value);
        if (action.Comp.MaxCharges == adjusted)
            return;

        action.Comp.MaxCharges = adjusted;
        Dirty(action);
    }

    /// <summary>
    /// The next time a charge will be considered to be filled.
    /// </summary>
    /// <returns>0 timespan if invalid or no charges to generate.</returns>
    [Pure]
    public TimeSpan GetNextRechargeTime(Entity<LimitedChargesComponent?, AutoRechargeComponent?> entity)
    {
        if (!Resolve(entity.Owner, ref entity.Comp1, ref entity.Comp2, false))
        {
            return TimeSpan.Zero;
        }

        // Okay so essentially we need to get recharge time to full, then modulus that by the recharge timer which should be the next tick.
        var fullTime = ((entity.Comp1.MaxCharges - entity.Comp1.LastCharges) * entity.Comp2.RechargeDuration) + entity.Comp1.LastUpdate;
        var timeRemaining = fullTime - _timing.CurTime;

        if (timeRemaining < TimeSpan.Zero)
        {
            return TimeSpan.Zero;
        }

        var nextChargeTime = timeRemaining.TotalSeconds % entity.Comp2.RechargeDuration.TotalSeconds;
        return TimeSpan.FromSeconds(nextChargeTime);
    }

    /// <summary>
    /// Derives the current charges of an entity.
    /// </summary>
    [Pure]
    public int GetCurrentCharges(Entity<LimitedChargesComponent?, AutoRechargeComponent?> entity)
    {
        if (!Resolve(entity.Owner, ref entity.Comp1, false))
        {
            // I'm all in favor of nullable ints however null-checking return args against comp nullability is dodgy
            // so we get this.
            return -1;
        }

        var calculated = 0;

        if (Resolve(entity.Owner, ref entity.Comp2, false) && entity.Comp2.RechargeDuration.TotalSeconds != 0.0)
        {
            calculated = (int)((_timing.CurTime - entity.Comp1.LastUpdate).TotalSeconds / entity.Comp2.RechargeDuration.TotalSeconds);
        }

        return Math.Clamp(entity.Comp1.LastCharges + calculated,
            0,
            entity.Comp1.MaxCharges);
>>>>>>> 9f6826ca6b052f8cef3a47cb9281a73b2877903d
    }
}
