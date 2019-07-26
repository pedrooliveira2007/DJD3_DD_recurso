using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Constant variables to controll the limits of mana/stamina.
    const float maxMana = 100;
    const int maxNode = 3;
    
    //Variables to controll the mana/stamina states.
    [SerializeField]
    internal float manaRegenRate = 1;
    internal float mana = 100;
    internal int node = 3;

    //Variables to controll hp values, max and current.
    internal int maxHP = 100;
    internal int hp = 100;



    /// <summary>
    /// Used to controll the health bar visually.
    /// </summary>
    [SerializeField]
    internal Image healthBar;
    [SerializeField]
    internal Image hurtBar;
    [SerializeField]
    internal Animator hurtBarAnim;
    [SerializeField]
    internal Animator lowHp;

    [SerializeField]
    internal Animator feedbackAnimhurt;

    [SerializeField]
    internal Animator feedbackAnimheal;

    /// <summary>
    /// Used to controll and talk to the ability manager.
    /// </summary>
    [SerializeField]
    private AbilityManager ability;

    /// <summary>
    /// Used to transform the scale of the mana bar. (Note: This is to use
    /// the resolution of the canvas to create a water like effect on it, via
    /// downscalling of the sprite.)
    /// </summary>
    [SerializeField]
    internal Transform manaBar;
    /// <summary>
    /// Reference for the NodeManager, used to talk to the nodes.
    /// </summary>
    [SerializeField]
    internal NodeManager nodeMan;

    /// <summary>
    /// Used to create effect when the manabar has been filled.
    /// </summary>
    [SerializeField]
    internal Animator manaConsume;

    /// <summary>
    /// Text that specifies that the mana is MAX!!!
    /// </summary>
    [SerializeField]
    internal GameObject maxText;

    /// <summary>
    /// Text that specifies how much HP is left and whats the max HP value.
    /// </summary>
    [SerializeField]
    internal TextMeshProUGUI hpText;

    /// <summary>
    /// Screen to display info.
    /// </summary>
    [SerializeField]
    private GameObject helpTab;
    /// <summary>
    /// Spawned HelpTab.
    /// </summary>
    private GameObject spawnedHelpTab;
    private bool helpTabState = false;



    // Start is called before the first frame update
    void Start()
    {
        AdjustMana(0);
        AdjustNode(0);
        AdjustHP(0);
    }

    // Update is called once per frame

    private void Update()
    {
        InputReader();
    }

    void FixedUpdate()
    {
        // Checks if the nodes are superior or equal to the max number of nodes.
        if (node >= maxNode)
        {
            // To make sure it doesn't somehow get over the max.
            node = maxNode;
            mana = maxMana;
            maxText.SetActive(true);
        }

        // Checks if mana is inferior to the max value of mana allowed.
        if(mana < maxMana)
        {
            //Adds mana if then.
            AdjustMana(manaRegenRate);
        }

        // Checks if mana is superior or equal to the max value of mana allowed.
        if(mana >= maxMana)
        {
            // Checks if the maxnode count hasn't been hit already.
            if(node < maxNode)
            {
                AdjustNode(1);
                manaConsume.SetTrigger("Consume");
            }
        }
    }

    /// <summary>
    /// Debug purposes and to handle the help menu.
    /// </summary>
    void InputReader()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            if (helpTabState == false)
            {
                spawnedHelpTab = Instantiate(helpTab, FindObjectOfType<Canvas>().transform);
            }
            helpTabState = true;
        }
        else
        {
            if (helpTabState == true)
            {
                Debug.Log("HELP STOP");
                Destroy(spawnedHelpTab);
            }
            helpTabState = false;
        }

    }



    /// <summary>
    /// Adjusts the HP value of the Character, Returns true if alive, false if dead.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal bool AdjustHP(int value)
    {
        // Defines ammount the HP hurt should be.
        hurtBar.fillAmount = (float)hp / maxHP;
        // Starts the animation of hurtHP to create the HP fade effect.
        hurtBarAnim.SetTrigger("Hurt");
        if(value > 0)
        {
            feedbackAnimheal.SetTrigger("Go");
        }
        else if(value < 0)
        {
            feedbackAnimhurt.SetTrigger("Go");
        }
        // Applies value changes to the HP value.
        hp += value;
        // Checks if the user is dead or overkilled
        if (hp <= 0)
        {
            hp = 0;
            healthBar.fillAmount = (float)hp / maxHP;
            hpText.text = hp.ToString() + " / " + maxHP.ToString();
            return false;
        }
        // Doesn't let the HP overflow its max value.
        else if (hp > maxHP)
            hp = maxHP;
        // Used to trigger the low HP animation.
        if(hp <= 25)
        {
            lowHp.SetTrigger("Low");
        }
        // Used to trigger the low HP animation to stop.
        else
        {
            lowHp.SetTrigger("Ok");
        }
        // Defines the health bar's fill ammount.
        healthBar.fillAmount = (float)hp / maxHP;

        // Updates the text with the current HP values.
        hpText.text = hp.ToString() + " / " + maxHP.ToString();

        return true;
    }

    /// <summary>
    /// Changes the mana value.
    /// </summary>
    /// <param name="value"></param>
    void AdjustMana(float value)
    {
        mana += value;
        manaBar.localScale = new Vector3(1, (float)mana / (float)maxMana, 1);
    }

    /// <summary>
    /// Adds value ammount of nodes.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal bool AdjustNode(int value)
    {
        if (node > maxNode)
        {
            nodeMan.Manage(node);
            return false;
        }
        node += value;
        if(node >= maxNode)
        {
            nodeMan.Manage(node);
            AdjustMana(100);
            return true;
        }
        else
        {
            nodeMan.Manage(node);
            mana = 0;
            return true;
        }

    }

    /// <summary>
    /// Function used to send how many nodes to consume.
    /// </summary>
    /// <param name="value"></param>
    internal bool ConsumeNode(int value)
    {
        // Checks if the node ammount was max or superior to the cost.
        if(node >= maxNode &&  node >= value)
        {
            maxText.SetActive(false);
            node -= value;
            mana = 0;
            nodeMan.Manage(node);
            return true;
        }
        // SafeGuard to costs that are too high to be possible to achieve.
        if(value > maxNode)
        {
            Debug.Log("THIS VALUE IS IMPOSSIBLE TO ACHIEVE ON THIS ABILITY.");
            return false;
        }

        // Checks if there are enough nodes to cast the ability.
        if(node < value)
        {
            nodeMan.Manage(node);
            return false;
        }
        node -= value;
        // Update ui for nodes.
        nodeMan.Manage(node);
        return true;
    }

    public void AdjustMaxHP(int value)
    {
        maxHP += value;
        AdjustHP(0);
    }
}
