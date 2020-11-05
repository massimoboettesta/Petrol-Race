using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LoadingScreenMessage : MonoBehaviour
{
    private TextMeshProUGUI actualText;
    public string _message;
    private void Awake()
    {
        actualText = GetComponentInChildren<TextMeshProUGUI>();
        actualText.text = _message;
    }
    public void UpdateMessageText()
    {
        actualText.text = _message;
    }
}
