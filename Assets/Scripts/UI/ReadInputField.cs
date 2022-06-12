using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInputField : MonoBehaviour
{
    public void SetMaxTimer(string s){
        Timer.setMinutes(float.Parse(s));
    }
}
