using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void OnClick(){
        if(this.gameObject.name == "Pause Button"){
            Timer.Pause = true;
        }else if(this.gameObject.name == "Resume Button" || this.gameObject.name == "Play Button"){
            Timer.Pause = false;
            Timer.Pause2 = false;
        }
    }
	/*void Update(){
        GameObject pauseButton  = GameObject.Find("Resume Button");
        GameObject 
        if(pauseButton != null){
            if(pauseButton.activeInHierarchy == true){
                Timer.Pause = true;
            }
        }
    }*/
}
