using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
           
        if(this.gameObject.name == "Player1ScoreText"){
            this.gameObject.GetComponent<TextMeshProUGUI>().color = GetColors.player2Color;
            if(Scoreboard.player1Score < 10 && Scoreboard.player1Score >= 0){
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "0" + Scoreboard.player1Score.ToString();
            }else{
                this.gameObject.GetComponent<TextMeshProUGUI>().text = Scoreboard.player1Score.ToString();
            }
            
        }else if(this.gameObject.name == "Player2ScoreText"){
            this.gameObject.GetComponent<TextMeshProUGUI>().color = GetColors.player1Color;
            if(Scoreboard.player2Score < 10 && Scoreboard.player2Score >= 0){
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "0" + Scoreboard.player2Score.ToString();
            }else{
                this.gameObject.GetComponent<TextMeshProUGUI>().text = Scoreboard.player2Score.ToString();
            }
            
        }
    
    }
 
}