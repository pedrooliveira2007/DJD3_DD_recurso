using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnable : MonoBehaviour
{
    [SerializeField]
    internal GameObject ability;

    private HP_SideBar_Manager manager;
    
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag
    ("SideBar").GetComponent<HP_SideBar_Manager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && manager.ListCount())
        {
            ability.SetActive(true);
            Destroy(gameObject);
        }
    }
}
