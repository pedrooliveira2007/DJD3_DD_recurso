using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    [SerializeField]
    internal Animator node1;
    [SerializeField]
    internal Animator node2;
    [SerializeField]
    internal Animator node3;

    internal void Manage(int nodes)
    {
        switch(nodes)
        {
            case 3:
                GetNode1();
                GetNode2();
                GetNode3();
                break;

            case 2:
                GetNode1();
                GetNode2();
                KillNode3();
                break;

            case 1:
                GetNode1();
                KillNode2();
                KillNode3();
                break;
            case 0:
                KillNode1();
                KillNode2();
                KillNode3();
                break;
        }


    }

    void KillNode1()
    {
        node1.ResetTrigger("Live");
        node1.SetTrigger("Kill");
    }

    void GetNode1()
    {
        node1.ResetTrigger("Kill");
        node1.SetTrigger("Live");
    }

    void KillNode2()
    {
        node2.ResetTrigger("Live");
        node2.SetTrigger("Kill");
    }

    void GetNode2()
    {
        node2.ResetTrigger("Kill");
        node2.SetTrigger("Live");
    }
    void KillNode3()
    {
        node3.ResetTrigger("Live");
        node3.SetTrigger("Kill");
    }

    void GetNode3()
    {
        node3.ResetTrigger("Kill");
        node3.SetTrigger("Live");
    }
}
