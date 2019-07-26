using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    internal HP_SideBar_Manager manager;
    internal Animator anim;
    public Animator animMenu;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag
            ("SideBar").GetComponent<HP_SideBar_Manager>();
        anim = GetComponent<Animator>();
    }
    

    public void OpenMenu()
    {
        animMenu.SetTrigger("open menu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && manager.ListCount())
        {
            this.GetComponent<Collider>().enabled = false;
            anim.SetTrigger("open");
        }
    }
}
