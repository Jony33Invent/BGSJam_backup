using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currScore = 0;
    [SerializeField] private HudManager hud;
    private int scoreAcertou = 15;
    private int scoreErrou = 5;
    [SerializeField] private GameObject errouFx;
    [SerializeField] private GameObject acertouFx;
    [SerializeField] private ItemSpawner spawner;
    public void AddScore(int score)
    {
        spawner.SpawnItem();
        currScore += score;
        hud.SetScore(currScore);

    }
    public void AcertouScore(Vector3 pos)
    {
        AddScore(scoreAcertou);
        Instantiate(acertouFx, pos, Quaternion.identity);
    }
    public void ErrouScore(Vector3 pos)
    {
        AddScore(scoreErrou);
        Instantiate(errouFx, pos, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        hud.SetScore(currScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
