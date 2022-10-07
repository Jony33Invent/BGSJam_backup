using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreView;
    [SerializeField] private GameObject itemHud;
    [SerializeField] private TMP_Text itemView;
    [SerializeField] private Image itemImage;
    Coroutine scaleScore;
    [SerializeField] private float animSpeed = 0.1f;
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
    public void SetScore(int score)
    {
        scoreView.text = score.ToString("0000");
        if(scaleScore!=null)
        StopCoroutine(scaleScore);
        scaleScore=StartCoroutine(ScaleAnim(scoreView.transform,1.2f));
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
