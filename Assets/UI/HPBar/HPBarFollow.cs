using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is applied on the enemies. Manages its creation and destruction
/// depending on if they are on screen or not.
/// </summary>
public class HPBarFollow : MonoBehaviour
{
    // Specifys if there already are HP bars setup.
    bool exists = false;
    [SerializeField]
    private GameObject barPrefab;
    [SerializeField]
    private GameObject sidebarPrefab;
    private GameObject spawnedBar;
    private GameObject spawnedSideBar;
    internal EnemyHPManager sideBar;
    internal EnemyController enemyStats;
    private GameObject previousBar;

    internal HP_SideBar_Manager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("SideBar").GetComponent<HP_SideBar_Manager>();
        enemyStats = gameObject.GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(exists)
            spawnedBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnBecameInvisible()
    {
        if (exists)
        {
            previousBar = spawnedSideBar;
            exists = false;
            Debug.Log("Bye");
            StartCoroutine(Despawn());
        }

    }

    /// <summary>
    /// Used to despawn stuff.
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    private IEnumerator Despawn()
    {
        manager.KillThis(sideBar);
        sideBar.anim.SetTrigger("Kill");
        Destroy(spawnedBar);
        yield return new WaitForSeconds((float)(0.75));
        manager.RestartAnim();
        Destroy(previousBar);
    }


    internal void OnBecameVisible()
    {
        exists = true;
        spawnedBar = Instantiate(barPrefab, FindObjectOfType<Canvas>().transform);
        spawnedSideBar = Instantiate(sidebarPrefab, GameObject.FindGameObjectWithTag("SideBar").transform);
        sideBar = spawnedSideBar.GetComponent<EnemyHPManager>();
        sideBar.TransferData(enemyStats, true);
        spawnedBar.GetComponent<EnemyHPManager>().TransferData(enemyStats, false);
        manager.AddThis(sideBar);
    }

    /// <summary>
    /// Please make sure the value you are giving is correct. Negative values
    /// REDUCE HP, Posivite values ADD.
    /// </summary>
    /// <param name="value"></param>
    internal void ChangeHP(int value)
    {
        if(exists)
        {
            spawnedBar.GetComponent<EnemyHPManager>().AdjustHP(value);
            sideBar.AdjustHP(value);
        }
    }

    /// <summary>
    /// Call this before you destroy the gameobject so it removes all the
    /// gameobjects this script removes that aren't its children.
    /// </summary>
    internal void Kill()
    {
        exists = false;
        previousBar = spawnedSideBar;
        StartCoroutine(Despawn());
    }
}
