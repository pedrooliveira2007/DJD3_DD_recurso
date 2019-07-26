using System;
using UnityEngine;
[Serializable]
public class WeaponStatus : MonoBehaviour
{
    [SerializeField]
    internal float damageModifier = 2;
    [SerializeField]
    internal bool heavyHit = false;

    private void OnEnable()
    {
        if (heavyHit == true)
        gameObject.GetComponent<Animator>().SetTrigger("GO");
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
