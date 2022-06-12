using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public static int player1Score = 0;
    public static int player2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        player1Score = 0;
        player2Score = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void givePoint(){
        
        Vector3 sheepPositions = GameObject.FindGameObjectWithTag("Sheep").transform.position;
        Vector3 player1Positions = GameObject.FindGameObjectWithTag("Player1").transform.position;
        Vector3 player2Positions = GameObject.FindGameObjectWithTag("Player2").transform.position;
        
        float distanceP1Sheep = Vector3.Distance(player1Positions, sheepPositions);
        float distanceP2Sheep = Vector3.Distance(player2Positions, sheepPositions);

        if(distanceP1Sheep < distanceP2Sheep){
            player1Score++;
        }else{
            player2Score++;
        }
        showScore();
    }

    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.transform.parent.tag == "Player2" || collisionInfo.collider.transform.parent.tag == "Player1"){
            if(MushroomBehavior.player1Boosted){
                MushroomBehavior.player1Boosted = false;
                player1Score+=2;
                player2Score-=2;
            }if(MushroomBehavior.player2Boosted){
                MushroomBehavior.player2Boosted = false;
                player2Score+=2;
                player1Score-=2;
                
            }
        }
       
    }
    public static void removePoint(string tag){
        if(tag == "Player1"){
            player1Score--;
        }else{
            player2Score--;
        }
        showScore();
    }
    static void showScore(){
        Debug.Log("Player 1 Score : " + player1Score);
        Debug.Log("Player 2 Score : " + player2Score);
        Debug.Log("-------------------------------------------------------------------");
    }


}