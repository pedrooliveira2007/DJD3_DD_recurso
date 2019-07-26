using UnityEngine;
using TMPro;

/// <summary>
/// Class used to manage ability cooldowns of an individual ability and its
/// availability.
/// </summary>
public class CooldownManager : MonoBehaviour
{

    /// <summary>
    /// Used to check for resources.
    /// </summary>
    [SerializeField]
    private UIManager resource;


    /// <summary>
    /// Name of the ability.
    /// </summary>
    [SerializeField]
    private string abilityname = "NAME ME";

    /// <summary>
    /// Used to transmit text directly to the player.
    /// </summary>
    [SerializeField]
    internal TextMeshProUGUI reportText;

    /// <summary>
    /// Used to trigger animations for the text.
    /// </summary>
    [SerializeField]
    internal Animator reportAnim;

    /// <summary>
    /// Used to show the cost of the abilities.
    /// </summary>
    [SerializeField]
    internal Animator costAnim;

    /// <summary>
    /// Used to handle animations.
    /// </summary>
    [SerializeField]
    internal Animator anim;
    /// <summary>
    /// Used to manage the scale animation of the cooldown indicator.
    /// </summary>
    [SerializeField]
    internal Transform scale;

    /// <summary>
    /// Used to manage the Cooldown Text.
    /// </summary>
    [SerializeField]
    internal TextMeshProUGUI counter;

    /// <summary>
    /// Current CD of this ability.
    /// </summary>
    private float currentCD;

    /// <summary>
    /// Defines the cooldown of the ability.
    /// </summary>
    [SerializeField]
    private float maxCD = 20;

    /// <summary>
    /// Defines a multiplier on cooldown rate. Lets say if we add a buff to
    /// increase cooldown rate this boy would handle that.
    /// </summary>
    [SerializeField]
    internal float multiplierCD = 1;

    /// <summary>
    /// Defines if this ability is unlocked from the start.
    /// </summary>
    [SerializeField]
    internal bool available = true;

    /// <summary>
    /// Defines the cost of the ability.
    /// </summary>
    [SerializeField]
    private int cost = 0;

    private void Start()
    {
        costAnim.SetTrigger(cost + "Cost");
        
        //Checks if the ability is unlocked from the start.
        if(!available)
        {
            anim.SetBool("Unavailable", true);
        }
        reportText = GameObject.FindGameObjectWithTag
            ("TroubleShoot").GetComponent<TextMeshProUGUI>();
        reportAnim = GameObject.FindGameObjectWithTag
            ("TroubleShoot").GetComponent<Animator>();
        resource = GameObject.FindGameObjectWithTag
            ("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            reportText.text = "This ability is still on cooldown.";
            reportAnim.SetTrigger("Go");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            reportText.text = "This ability is locked. For now.";
            reportAnim.SetTrigger("Go");
        }
        //Checks if its unlocked.
        if (available)
        {
            //Checks if its on cooldown.
            if (currentCD > 0)
            {
                currentCD -= Time.deltaTime * multiplierCD;
                scale.localScale = new Vector3(1, (float)currentCD / (float)maxCD, 1);
                counter.text = (currentCD * multiplierCD).ToString("F1");
            }
            else
            {
                //Checks if there is enough MP for the ability.
                if (cost > resource.node)
                {
                    anim.SetTrigger("NoMP");
                }
                else
                {
                    counter.text = "";
                    anim.ResetTrigger("Used");
                    anim.SetBool("Unavailable", false);
                    anim.SetTrigger("Ready");
                }
            }
        }
    }

    /// <summary>
    /// Checks if it can activate the ability and activates.
    /// </summary>
    /// <returns></returns>
    internal bool Activate()
    {
        //Checks if the ability has been unlocked.
        if (available)
        {
            //Checks if its on cooldown.
            if (currentCD <= 0)
            {
                //Checks if there is enough MP.
                if (resource.ConsumeNode(cost))
                {
                    currentCD = maxCD;
                    anim.ResetTrigger("Ready");
                    anim.SetBool("Unavailable", false);
                    anim.SetTrigger("Used");
                    return true;
                }
                //If there isnt enough MP
                else
                {
                    reportText.text = "Not enough Mana.";
                    reportAnim.SetTrigger("Go");
                    return false;
                }

            }
            //If it's on cooldown.
            reportText.text = abilityname + " is still on cooldown.";
            reportAnim.SetTrigger("Go");
                return false;
        }
        //If the ability is locked.
        reportText.text = abilityname + " is locked. For now.";
        reportAnim.SetTrigger("Go");
        return false;
    }

    /// <summary>
    /// Unlocks said ability.
    /// </summary>
    internal void Unlock()
    {
        anim.SetBool("Unavailable", false);
        available = true;
        reportText.text = abilityname + " has been unlocked.";
        reportAnim.SetTrigger("Go");
        anim.SetTrigger("Ready");
    }

    public void ReduceCD(float value)
    {
        maxCD -= value;
    }

    public void ReduceCost(int value)
    {
        cost -= value;

        if(cost >= 3)
        {
            cost = 3;
        }
        else if(cost < 0)
        {
            cost = 0;
        }
        costAnim.SetTrigger(cost + "Cost");
    }

}
