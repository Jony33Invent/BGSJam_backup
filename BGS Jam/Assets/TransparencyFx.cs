using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyFx : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeAlpha());
    }
    IEnumerator ChangeAlpha()
    {
        Color cor= material.color;
        cor.a = 1f;
        material.color = cor;
        do
        {
            cor = material.color;
            cor.a -= speed;
            material.color = cor;
            yield return new WaitForEndOfFrame();
        } while (cor.a > 0);
        cor.a -= 0;
        material.color = cor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
