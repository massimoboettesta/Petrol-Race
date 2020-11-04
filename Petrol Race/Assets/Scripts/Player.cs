﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI resourcesAmmountUI;
    public int currentPlayerResources = 0;
    public List<Unit> units = new List<Unit>();

    public void UpdateResources()
    {
        resourcesAmmountUI.text = currentPlayerResources.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
