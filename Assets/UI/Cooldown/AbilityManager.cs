using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used as a central hub for the abilitys. Making it easier to activate them.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    internal CooldownManager firstAbility;

    [SerializeField]
    internal CooldownManager secondAbility;

    [SerializeField]
    internal CooldownManager thirdAbility;

    [SerializeField]
    internal CooldownManager fourthAbility;

    [SerializeField]
    internal CooldownManager fifthAbility;

    [SerializeField]
    internal CooldownManager sixthAbility;



    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool FirstTrigger()
    {
        return firstAbility.Activate();
    }

    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool SecondTrigger()
    {
        return secondAbility.Activate();
    }

    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool ThirdTrigger()
    {
        return thirdAbility.Activate();
    }

    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool FourthTrigger()
    {
        return fourthAbility.Activate();
    }

    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool FifthTrigger()
    {
        return fifthAbility.Activate();
    }

    /// <summary>
    /// Returns false if the ability cannot be activated.
    /// True if activated.
    /// </summary>
    /// <returns></returns>
    internal bool SixthTrigger()
    {
        return sixthAbility.Activate();
    }

    public void FirstUnlock()
    {
        firstAbility.Unlock();
    }

    public void SecondUnlock()
    {
        secondAbility.Unlock();
    }

    public void ThirdUnlock()
    {
        thirdAbility.Unlock();
    }

    public void FourthUnlock()
    {
        fourthAbility.Unlock();
    }

    public void FifthUnlock()
    {
        fifthAbility.Unlock();
    }

    public void SixthUnlock()
    {
        sixthAbility.Unlock();
    }
}
