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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerCount += Time.deltaTime;
        slider.value = timerCount/endTime;
        if (timerCount >= endTime)
        {
            hudManager.ActivateEndScreen(); 
            //scoreManager.ResetScore();
            timerCount = 0;
            //Destroy(gameObject);
        }
    }
}
