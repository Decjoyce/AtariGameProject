using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fpsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;

    // Update is called once per frame
    void Update()
    {
        int fps = (int)(1.0f / Time.unscaledDeltaTime);
        fpsText.text = fps.ToString();
    }
}
