using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType 
{ 
    Crystal,
    Gas
}
public class ResourceSource : MonoBehaviour
{
    public ResourceType type;
    public int quantity;

    //events
    public UnityEvent onQuantityChange;

    public int GatherResource(int ammount, Player gatheringPlayer)
    {
        quantity -= ammount;

        int ammountToGive = ammount;

        if (quantity < 0)
        {
            ammountToGive = ammount + quantity;
        }

        if (quantity <= 0)
        {
            Destroy(gameObject);
        }

        if (onQuantityChange != null)
        {
            onQuantityChange.Invoke();
        }
        return ammountToGive;
    }
}
