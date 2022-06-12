using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FindWinner : MonoBehaviour
{
    public static TextMeshProUGUI winner;

public void Start() {
        winner = this.gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    public static void winnerText(){
        if(Scoreboard.player1Score > Scoreboard.player2Score){
            winner.text = "Player 2!";
            winner.color = GetColors.player2Color;
        }else if(Scoreboard.player1Score < Scoreboard.player2Score){
            winner.text = "Player 1!";
            winner.color = GetColors.player1Color;
        }
        else{
            winner.text = "IT'S A DRAW!";
            winner.color = new Color32(4, 20, 5, 255); 
        }
    }

}
