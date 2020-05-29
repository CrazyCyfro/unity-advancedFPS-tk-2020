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

    void Start()
    {
        StartCoroutine(DisplayFPS());
    }

    IEnumerator DisplayFPS()
    {
        while (true) {
            fps.text = Mathf.FloorToInt(1/Time.smoothDeltaTime).ToString();
            yield return new WaitForSeconds(0.25f);
        }
        
    }
    
}
