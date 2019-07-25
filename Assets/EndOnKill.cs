using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOnKill : MonoBehaviour
{
    private EndManager finish;

    void Start()
    {
        finish = GameObject.FindGameObjectWithTag
            ("End").GetComponent<EndManager>();
    }

    public void ToggleEnd()
    {
        finish.ChangeState();
    }
}
