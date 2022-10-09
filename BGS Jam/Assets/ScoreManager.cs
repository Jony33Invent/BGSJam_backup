using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currScore = 0;
    [SerializeField] private HudManager hud;
    private int scoreAcertou = 15;
    int highestScore = 0;
    private int scoreErrou = 5;
    [SerializeField] private GameObject errouFx;
    [SerializeField] private GameObject acertouFx;
    [SerializeField] private ItemSpawner spawner;
    [SerializeField] private Timer timer;
    [SerializeField] private SoundManager soundManager;
    public void AddScore(int score)
    {
        spawner.SpawnItem();
        currScore += score;
        hud.SetScore(currScore);
        if (currScore > highestScore)
        {
            PlayerPrefs.SetInt("Highscore", currScore);
            highestScore = currScore;
            hud.SetHighscore(highestScore);
        }

    }
    public void ResetScore()
    {
        currScore = 0;
        hud.SetScore(currScore);
    }
    public void AcertouScore(Vector3 pos)
    {
        soundManager.Play("Acertou");
        timer.AddTime();
        AddScore(scoreAcertou);
        Instantiate(acertouFx, pos, Quaternion.identity);
    }
    public void ErrouScore(Vector3 pos)
    {
        soundManager.Play("Errou");
        AddScore(scoreErrou);
        Instantiate(errouFx, pos, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Highscore", 0);
        highestScore = PlayerPrefs.GetInt("Highscore");
        hud.SetHighscore(highestScore);
        hud.SetScore(currScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
