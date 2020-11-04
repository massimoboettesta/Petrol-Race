using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimFinish : MonoBehaviour
{
    private Animation currAnim;

    // Start is called before the first frame update
    void Start()
    {
        currAnim = GetComponentInChildren<Animation>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!currAnim.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
