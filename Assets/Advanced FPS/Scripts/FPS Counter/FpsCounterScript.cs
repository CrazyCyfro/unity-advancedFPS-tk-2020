using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCounterScript : MonoBehaviour
{
    private TextMeshProUGUI fps;

    void Awake()
    {
        fps = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        fps.text = (1.0f/Time.smoothDeltaTime).ToString();
    }
    
}
