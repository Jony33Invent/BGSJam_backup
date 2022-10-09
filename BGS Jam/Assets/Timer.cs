using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{

    float timerCount = 0;
    [SerializeField] private float endTime;
    [SerializeField] private Slider slider;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HudManager hudManager;
    [SerializeField] private float addTime;

    public void AddTime()
    {
        timerCount -= addTime;
        timerCount = Mathf.Max(timerCount, 0);
    }
    // Update is called once per frame
    void Update()
    {
        timerCount += Time.deltaTime;
        slider.value = 1f-timerCount/endTime;
        if (timerCount >= endTime)
        {
            hudManager.ActivateEndScreen(); 
            //scoreManager.ResetScore();
            timerCount = 0;
            //Destroy(gameObject);
        }
    }
}
