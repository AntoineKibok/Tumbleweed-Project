using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaText : MonoBehaviour
{

    private TextMeshProUGUI text;
    private string version;
    [TextArea(minLines: 2, maxLines: 5)] public string details;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            return;
        #endif
        text = GetComponent<TextMeshProUGUI>();
        version = Application.productName + " version: " + Application.version;
        text.text = version + "\n"
                            + details;
    }
}

