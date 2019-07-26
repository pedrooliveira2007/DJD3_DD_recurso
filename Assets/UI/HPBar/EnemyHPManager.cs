using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Class that updates the enemies HP bar accordingly.
/// </summary>
public class EnemyHPManager : MonoBehaviour
{

    //Variables to controll hp values, max and current.
    int maxHP = 100;
    int hp = 100;
    string enemyName;
    private bool isSideBar;

    [SerializeField]
    internal Image healthBar;
    [SerializeField]
    internal Image hurtBar;
    [SerializeField]
    internal Animator hurtBarAnim;
    [SerializeField]
    private TextMeshProUGUI creatureName;
    [SerializeField]
    private TextMeshProUGUI creatureNameBG;

    [SerializeField]
    internal Animator anim;

    [SerializeField]
    internal TextMeshProUGUI hpText;

    // Start is called before the first frame update
    internal void TransferData(EnemyController enemy, bool state)
    {
        maxHP = enemy.maxHP;
        hp = enemy.HP;
        enemyName = enemy.mobName;
        AdjustHP(0);
        isSideBar = state;
        if (isSideBar)
        {
            AdjustName();
            anim.SetTrigger("Start");
        }
    }

    /// <summary>
    /// Used to adjust the HP value of the Target.
    /// </summary>
    /// <param name="value"></param>
    internal void AdjustHP(int value)
    {
        hurtBar.fillAmount = (float)hp / maxHP;
        hurtBarAnim.SetTrigger("Hurt");
        hp += value;
        if (hp <= 0)
        {
            hp = 0;
            healthBar.fillAmount = (float)hp / maxHP;
            hpText.text = hp.ToString() + " / " + maxHP.ToString();
        }
        else if (hp > maxHP)
            hp = maxHP;
        healthBar.fillAmount = (float)hp / maxHP;

        hpText.text = hp.ToString() + " / " + maxHP.ToString();
    }

    /// <summary>
    /// Used to change the name of the creature in the UI.
    /// </summary>
    private void AdjustName()
    {
        creatureName.text = enemyName;
        creatureNameBG.text = enemyName;
    }

    /// <summary>
    /// Used to start highlighting the Bar. Use if this was the last attacked enemy.
    /// </summary>
    internal void StartFocus()
    {
        if (isSideBar)
        {
            Debug.Log("WOOP");
            anim.ResetTrigger("Loss");
            anim.SetTrigger("Focus");
        }

    }

    /// <summary>
    /// Used to start highlighting the Bar. Use if this was the previous attacked enemy.
    /// </summary>
    internal void LoseFocus()
    {
        if (isSideBar)
        {
            anim.ResetTrigger("Focus");
            anim.SetTrigger("Loss");
        }
    }

}
