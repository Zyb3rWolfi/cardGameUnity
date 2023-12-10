using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardManager : MonoBehaviour
{
    public static event Action<int> onCardSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        print("Clicked");
        onCardSelected?.Invoke(1);
    }
}
