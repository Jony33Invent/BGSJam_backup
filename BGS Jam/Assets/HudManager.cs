using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreView;
    [SerializeField] private TMP_Text highscoreView;
    [SerializeField] private GameObject itemHud;
    [SerializeField] private TMP_Text itemView;
    [SerializeField] private Image itemImage;
    Coroutine scaleScore;
    [SerializeField] private float animSpeed = 0.1f;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text endScreenScore;
    [SerializeField] private TMP_Text endScreenHighscore;
    [SerializeField] private GameObject gameHud;
    IEnumerator ScaleAnim(Transform transf,float scale)
    {
        float t = 0;
        Vector3 initial = transf.localScale;
        Vector3 final = new Vector3(initial.x * scale, initial.y * scale, initial.z);
        do
        {
            transf.localScale = Vector3.Lerp(initial, final, t);
            t += animSpeed;
            yield return new WaitForEndOfFrame();
        } while (t < 1f);

        transf.localScale = Vector3.Lerp(initial, final, 1f);
        do
        {
            transf.localScale = Vector3.Lerp(initial, final, t);
            t -= animSpeed;
            yield return new WaitForEndOfFrame();
        } while (t > 0f);
        transf.localScale = Vector3.Lerp(initial, final, 0);
    }
    public void SetHighscore(int score)
    {
        highscoreView.text = score.ToString("0");
    }
    public void SetScore(int score)
    {
        scoreView.text = score.ToString("0");
        if(scaleScore!=null)
        StopCoroutine(scaleScore);
        scaleScore=StartCoroutine(ScaleAnim(scoreView.transform,1.2f));
    }

    public void ActivateEndScreen()
    {
        gameHud.GetComponent<Animator>().Play("HudOut");
        endScreen.SetActive(true);
        endScreenScore.text = scoreView.text;
        endScreenHighscore.text = highscoreView.text;
        Time.timeScale = 0;
    }
    public void SetItem(Sprite itemSprite,string itemName)
    {
        itemHud.SetActive(true);
        itemView.text = itemName;
        itemImage.sprite = itemSprite;
    }
    public void CloseItem()
    {

        itemHud.SetActive(false);
    }
    public void PlayScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
