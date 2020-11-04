using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceSourceUI : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI resourceQuantityText;
    public ResourceSource resource;
    private void Awake()
    {
        resourceQuantityText.text = resource.quantity.ToString();
    }
    private void OnMouseEnter()
    {
        popupPanel.SetActive(true);
    }
    private void OnMouseExit()
    {
        popupPanel.SetActive(false);
    }

    public void OnResourceQuantityChange()
    {
        resourceQuantityText.text = resource.quantity.ToString();
    }
}
