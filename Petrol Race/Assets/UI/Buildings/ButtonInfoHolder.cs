using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfoHolder : MonoBehaviour
{
    public Sprite[] availableImages;
    public Image currentIcon;
    public TextMeshProUGUI costText;

    public void SetImageInIcon(int i)
    {
        currentIcon.sprite = availableImages[i];
    }
    private void Awake()
    {
       
    }
}
