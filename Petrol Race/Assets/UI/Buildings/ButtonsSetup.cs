using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BuildingType
{
    Motherboard,
    Gate
}
public class ButtonsSetup : MonoBehaviour
{
    public TextMeshProUGUI buildingName;
    public GameObject buttonPrefab;
    public HorizontalLayoutGroup HLayout;
    // Start is called before the first frame update
    public string buildingNameString;
    public BuildingType buildingType;

    private void CollectorButton()
    {
        GameObject newButton = Instantiate(buttonPrefab);
        newButton.transform.SetParent(HLayout.transform);
        newButton.transform.localScale = Vector3.one;
        Button but = newButton.GetComponent<Button>();

        but.GetComponent<ButtonInfoHolder>().costText.text = 50.ToString();
        but.GetComponent<ButtonInfoHolder>().SetImageInIcon(0);
        but.onClick.AddListener(() => GenerateCollector());

    }

    private void HarrierButton()
    {
        GameObject newButton = Instantiate(buttonPrefab);
        newButton.transform.SetParent(HLayout.transform);
        newButton.transform.localScale = Vector3.one;
        Button but = newButton.GetComponent<Button>();

        but.GetComponent<ButtonInfoHolder>().costText.text = 120.ToString();
        but.GetComponent<ButtonInfoHolder>().SetImageInIcon(1);
        but.onClick.AddListener(() => GenerateHarrier());

    }
    //SET UP BUTTONS DEPENDING ON BUILDING TYPE
    private void SetBuildingButtons()
    {
        switch (buildingType)
        {
            case BuildingType.Motherboard:
                CollectorButton();
                HarrierButton();
                break;
            case BuildingType.Gate:
                break;
            default:
                break;
        }
        
    }
    private void GenerateCollector()
    {
        GetComponentInParent<UnitSpawner>().SpawnCollector();
    }
    private void GenerateHarrier()
    {
        GetComponentInParent<UnitSpawner>().SpawnHarrier();
    }
    private void Awake()
    {
        buildingName.text = buildingNameString;

        SetBuildingButtons();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
