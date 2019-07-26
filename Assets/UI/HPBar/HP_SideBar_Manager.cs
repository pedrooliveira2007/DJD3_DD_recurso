using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages all of the HP bars on the sidebar.
/// </summary>
public class HP_SideBar_Manager : MonoBehaviour
{

    private List<EnemyHPManager> sidebarList = new List<EnemyHPManager>();

    internal void AddThis(EnemyHPManager victim)
    {
        sidebarList.Add(victim);
    }

    internal void RestartAnim()
    {
        foreach (EnemyHPManager temp in sidebarList)
        {
            temp.anim.SetTrigger("Done");
            Debug.Log("Ohai");
        }
    }

    internal void KillThis(EnemyHPManager victim)
    {
        bool foundHim = false;
        bool buffer = false;
        int location = 0;
        foreach(EnemyHPManager temp in sidebarList)
        {
            buffer = false;
            if(temp == victim)
            {
                foundHim = true;
                buffer = true;
            }
            if (!foundHim)
            {
                location++;
            }
            if (foundHim && !buffer)
            {
                temp.anim.SetTrigger("Move");
            }
        }
        sidebarList.RemoveAt(location);
    }

    /// <summary>
    /// Returns true if there are any healthbars active.
    /// </summary>
    /// <returns></returns>
    internal bool ListCount()
    {
        if (sidebarList.Count >= 1)
        {
            return false;
        }
        return true;
    }
}
