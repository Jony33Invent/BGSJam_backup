using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int initialSpawn = 3;
    [SerializeField] private List<GameObject> itens;
    [SerializeField] private Transform posParent;
     private List<Transform> spawnPos=new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform pos in posParent)
        {
            spawnPos.Add(pos);
        }
        for(int i = 0; i < initialSpawn; i++)
        {
            SpawnItem();
        }
    }

    public void SpawnItem()
    {
        int randomItem = Random.Range(0, itens.Count);
        int posCount = spawnPos.Count;
        int n = 0;
        do
        {
            n++;
            int randomPos = Random.Range(0, posCount);
            if (spawnPos[randomPos].childCount == 0)
            {

                Instantiate(itens[randomItem], spawnPos[randomPos].position, Quaternion.identity, spawnPos[randomPos]);
                return;
            }
        } while (n < posCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
