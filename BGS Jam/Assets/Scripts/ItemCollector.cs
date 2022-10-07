using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private GameObject itemCollected;
    private Rigidbody itemRigidbody;
    Item item;
    [SerializeField] private Collider collectorCollider;
    [SerializeField] private HudManager hudManager;
    private bool hasItem = false;
    public bool HasItem => hasItem;
    [SerializeField] private float colDelayTime = 0.5f;
    IEnumerator DesactivateCollider(Collider col)
    {
        col.enabled = false;
        yield return new WaitForSeconds(colDelayTime);
        col.enabled = true;
    }
    public void GetItem(GameObject obj)
    {

        itemCollected = obj;
        itemCollected.transform.SetParent(transform);
        itemCollected.transform.localPosition = Vector3.zero;
        itemRigidbody = itemCollected.GetComponent<Rigidbody>();

        itemRigidbody.isKinematic= true;
        item = itemCollected.GetComponent<Item>();
        hudManager.SetItem(item.ItemSprite, item.ItemName);
        hasItem = true;
    }
    public void ThrowItem(Vector3 direction)
    {
        hudManager.CloseItem();
        StartCoroutine(DesactivateCollider(collectorCollider));
        itemRigidbody.isKinematic = false;
        itemCollected.transform.parent = null;
        itemRigidbody.AddForce(direction, ForceMode.Impulse);
        item.ActivateFx();
        hasItem = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item") &&!hasItem)
        {
            GetItem(other.gameObject);
        }
    }
}
