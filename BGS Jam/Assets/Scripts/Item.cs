using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemId;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string name;
    [SerializeField] private GameObject fx;
    public int ItemId => itemId;
    public Sprite ItemSprite => sprite;
    public string ItemName => name;
    public void ActivateFx()
    {
        fx.SetActive(true);
    }

}
