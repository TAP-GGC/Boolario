using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowToScore : MonoBehaviour
{
    public TMP_Text logicType, instructions, example;

    void Start() {
        logicType = GameObject.Find("logicType").GetComponent<TMP_Text>();
        instructions = GameObject.Find("instructions").GetComponent<TMP_Text>();
        example = GameObject.Find("example").GetComponent<TMP_Text>();
    
        if (SceneControl.andGameIndicator){
            logicType.text = "AND Logic";
            instructions.text = "Collect a point if both booleans are \"true\"";
            example.text = "TRUE and TRUE = TRUE\nTRUE and FALSE = FALSE\nFALSE and FALSE = FALSE";
        }
        else {
            logicType.text = "OR Logic";
            instructions.text = "Collect a point if one boolean is \"true\"";
            example.text = "TRUE or TRUE = TRUE\nTRUE or FALSE = TRUE\nFALSE or FALSE = FALSE";
        }
    }
}
