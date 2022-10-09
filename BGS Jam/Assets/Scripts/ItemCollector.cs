using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ItemCollector : MonoBehaviour
{
    private GameObject itemCollected;
    private Rigidbody itemRigidbody;
    Item item;
    [SerializeField] private Collider collectorCollider;
    [SerializeField] private HudManager hudManager;
    private bool hasItem = false;
    [SerializeField] private GameObject activeCam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private List<GameObject> trashCams;
    public bool HasItem => hasItem;
    [SerializeField] private float colDelayTime = 0.5f;
    [SerializeField] private GameObject collectFx;
    [SerializeField] private Transform playerTransf;
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
        Instantiate(collectFx, itemCollected.transform.position, Quaternion.identity);
        itemCollected.transform.localPosition = Vector3.zero;
        itemRigidbody = itemCollected.GetComponent<Rigidbody>();
        itemRigidbody.isKinematic= true;
        item = itemCollected.GetComponent<Item>();

        // muda camera
        trashCams[item.ItemId].SetActive(true);
        activeCam.SetActive(false);
        activeCam = trashCams[item.ItemId];
        // mostra item na hud
        hudManager.SetItemColor(item.ItemId);
        hudManager.SetItem(item.ItemSprite, item.ItemName);
        hasItem = true;
    }
    public void ThrowItem(Vector3 direction)
    {
        // muda camera pro player
        playerCam.SetActive(true);
        activeCam.SetActive(false);
        activeCam = playerCam;

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
