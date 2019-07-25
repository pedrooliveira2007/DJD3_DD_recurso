using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject Enemy;

    public float WaveTimer = 2f;
    public int CurrentWave = 0;
    public int LastWave = 10;

    public bool WaveStarted = false;
    public bool WaveEnded = false;

    public bool EndedWave = false;
    public bool StartWave = false;

    private void Update()
    {
        if (!StartWave && WaveStarted == true)
        {

            StartWave = true;
            CallWave();
        }

        if (CurrentWave == LastWave && EndedWave == false)
        {
            EndedWave = true;
            EndWave();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WaveStarted = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("starting wave");
        }
    }

    public void CallWave()
    {
        if (WaveStarted == true)
        {
            InvokeRepeating("Spawn", 1f, WaveTimer);
            //Spawn();
            Debug.Log("calling next wave");
        }

    }

    public void EndWave()
    {
        WaveEnded = true;
        WaveStarted = false;
        CancelInvoke("Spawn");
        //Open next door

        Debug.Log("Wave ended");
    }

    public void Spawn()
    {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        CurrentWave++;
        foreach (Transform spawnpoint in SpawnPoint)
        {
            Instantiate(Enemy, spawnpoint.transform.position, spawnpoint.rotation);
        }
        Debug.Log("Next Wave");
        yield return new WaitForSeconds(WaveTimer);
    }
}
