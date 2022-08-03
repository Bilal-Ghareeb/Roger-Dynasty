using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject demon;
    [SerializeField]
    private float demonsInterval = 3.5f;

    public GameObject Player;

    public Transform spawnerPosition;

    public int demonsCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnDemon(demonsInterval, demon));
    }

    private IEnumerator spawnDemon(float interval , GameObject enemy)
    {
        if (demonsCount > 0)
        {
            demonsCount--;
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawnerPosition.position, spawnerPosition.rotation);
            newEnemy.GetComponent<Demon>().player = Player;
            newEnemy.GetComponent<Demon>().speed = 0.8f;
            StartCoroutine(spawnDemon(interval, enemy));
        }
      
           
    }
}
