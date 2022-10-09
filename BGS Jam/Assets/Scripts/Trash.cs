using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private int typeId;
    private ScoreManager scoreManager;
    private void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ScoreManager");
        if(obj!=null)
        scoreManager = obj.GetComponent<ScoreManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Item") && other.transform.parent==null)
        {
            //print("Acertou");
            if (other.gameObject.GetComponent<Item>().ItemId == typeId)
            {
                scoreManager.AcertouScore(other.transform.position);
            }
            else
            {
                scoreManager.ErrouScore(other.transform.position);
            }
            Destroy(other.gameObject);
        }
    }
}
