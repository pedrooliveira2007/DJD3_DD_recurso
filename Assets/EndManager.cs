using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{

    private bool EndGame = false;

    private HP_SideBar_Manager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag
    ("SideBar").GetComponent<HP_SideBar_Manager>();
    }

    void Update()
    {
        if (EndGame == true && manager.ListCount())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void ChangeState()
    {
        EndGame = true;
    }
}
