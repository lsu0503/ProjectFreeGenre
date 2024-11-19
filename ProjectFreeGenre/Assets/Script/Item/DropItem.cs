using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour, IInteractable
{
    public int id { get; }
    public ItemData data;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject fullSlotAlertUI;
    [SerializeField] private GameObject InteractionDisplay;
    private Coroutine curRoutine;
    private bool isCurrent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = data.sprite;
        fullSlotAlertUI.SetActive(false);
        InteractionDisplay.SetActive(false);
        isCurrent = false;
    }

    public void OnInteraction()
    {
        bool condition = GetItem();

        if (condition)
            Destroy(gameObject);

        else
        {
            if (InteractionDisplay.activeSelf)
                InteractionDisplay.SetActive(false);

            if (!fullSlotAlertUI.activeSelf)
                fullSlotAlertUI.SetActive(true);

            if (curRoutine != null)
                StopCoroutine(curRoutine);
            
            curRoutine = StartCoroutine(FullSlotAlertUIDeactivateAfterSeconds());
        }
    }

    public void SetInterActionTarget()
    {
        isCurrent = true;
        InteractionDisplay.SetActive(true);
    }

    public void UnsetInterActionTarget()
    {
        isCurrent = false;
        InteractionDisplay.SetActive(false);
    }

    public bool GetItem()
    {
        PlayerEquipment equipment = GameManager.Instance.player.equipment;
        bool result = equipment.AddItem(data);
        return result;
    }

    public IEnumerator FullSlotAlertUIDeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(ConstantCollection.ItemSlotFullAlertUIDisplayTime);

        if (!InteractionDisplay.activeSelf)
            if(isCurrent)
                InteractionDisplay.SetActive(true);

        if (fullSlotAlertUI.activeSelf)
            fullSlotAlertUI.SetActive(false);
    }
}