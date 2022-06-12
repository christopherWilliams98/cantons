using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColors : MonoBehaviour
{
    public Toggle orange1;
    public Toggle blue1;
    public Toggle yellow1;
    public Toggle turquoise1;
    public Toggle pink1;
    public Toggle orange2;
    public Toggle blue2;
    public Toggle yellow2;
    public Toggle turquoise2;
    public Toggle pink2;
    public static Color player1Color;
    public static Color player2Color;

    public void ChooseColors(){
        CelluloAgent player1Agent = GameObject.FindGameObjectWithTag("Player2").GetComponent<CelluloAgent>();
        CelluloAgent player2Agent = GameObject.FindGameObjectWithTag("Player1").GetComponent<CelluloAgent>();
        //Player 1 COLOR SELECT
        if(orange1.isOn){
            player1Agent.SetVisualEffect(0, new Color32(255,145,0,255), 0);
            player1Color = new Color32(255,145,0,255);
        }else if(blue1.isOn){
            player1Agent.SetVisualEffect(0, new Color32(0,0,245,255), 0);
            player1Color = new Color(0,0,245,255);
        }else if(yellow1.isOn){
            player1Agent.SetVisualEffect(0, new Color32(245,245,0,255), 0);
             player1Color = new Color32(245,245,0,255);
        }else if(turquoise1.isOn){
            player1Agent.SetVisualEffect(0, new Color32(0,245,231,255), 0);
             player1Color = new Color32(0,245,231,255);

        }else if(pink1.isOn){
            player1Agent.SetVisualEffect(0, new Color32(242,3,242,255), 0);
                 player1Color = new Color32(242,3,242,255);
        }

        //Player 2 COLOR SELECT
        if(orange2.isOn){
            player2Agent.SetVisualEffect(0, new Color32(255,145,0,255), 0);
            player2Color = new Color32(255,145,0,255);
        }else if(blue2.isOn){
            player2Agent.SetVisualEffect(0, new Color32(0,0,245,255), 0);
            player2Color = new Color32(0,0,245,255);
        }else if(yellow2.isOn){
            player2Agent.SetVisualEffect(0, new Color32(245,245,0,255), 0);
            player2Color = new Color32(245,245,0,255);
        }else if(turquoise2.isOn){
            player2Agent.SetVisualEffect(0, new Color32(0,245,231,255), 0);
            player2Color = new Color32(0,245,231,255);
        }else if(pink2.isOn){
            player2Agent.SetVisualEffect(0, new Color32(242,3,242,255), 0);
            player2Color = new Color32(242,3,242,255);
        }
    }
    

}
