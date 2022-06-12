using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public const int MAX_SCORE = 26; 
    public static int player1Cantons = 0;
    
    public static int player1Streak = 0;

    public static int player1TotalPoints = 0;

    private GameObject comboText;
    private GameObject pointsText;

    // Start is called before the first frame update
    void Start()
    {
        comboText = GameObject.Find("Combo points");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        pointsText = GameObject.Find("Score");
        if(pointsText != null){
            pointsText.GetComponent<TextMeshProUGUI>().text = "SCORE: " + player1TotalPoints;
        }
        
        if(player1Streak >= 3){
            comboText.SetActive(true);
            comboText.GetComponent<TextMeshProUGUI>().text = "COMBO: " + player1Streak + "X";
        }else{
            comboText.SetActive(false);
        }
    }

    public static void add_point(){
            player1Cantons++;
            player1Streak++;
            if(player1Streak >= 3){
                print("COMBO: " + player1Streak);
                player1TotalPoints= player1TotalPoints + player1Streak*10;
            }else{
                player1TotalPoints= player1TotalPoints + 10;
            }
    }

    public static void break_streak(){
        player1Streak = 0;
    } 

    public static void reset_game(){
        player1Cantons = 0;
        player1Streak = 0;
        player1TotalPoints = 0;
    }
}
