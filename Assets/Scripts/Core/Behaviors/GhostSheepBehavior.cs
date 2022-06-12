using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    

    const float sheepRadius = 7f;
    const float ghostRadius = 100f;
    const float sheepSpeed = 2f;
    const float ghostSpeed = 0.4f;

    const float minTransformTime = 5.0f;
    const float maxTransformTime = 15.0f;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    
    public AudioSource audioSource3;

    private Color32 green = new Color32(26, 255, 0, 255);
    private Color32 red = new Color32(255, 0, 0, 255); 

    public void Start(){
        audioSource1.volume = 0f;
        audioSource3.volume = 0f;
        this.gameObject.tag = "Sheep";
        Invoke("TransformBehavior", Random.Range(minTransformTime, maxTransformTime));

    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        if(Timer.Pause == false){

        Vector3 player1Positions = GameObject.FindGameObjectWithTag("Player1").transform.position;
        Vector3 player2Positions = GameObject.FindGameObjectWithTag("Player2").transform.position;
        
        float horizontal = 0.0f;
        float vertical = 0.0f;

        if(this.gameObject.tag == "Sheep"){

            Vector3 sheepPositions = GameObject.FindGameObjectWithTag("Sheep").transform.position;
            float distanceP1Sheep = Vector3.Distance(player1Positions, sheepPositions);
            float distanceP2Sheep = Vector3.Distance(player2Positions, sheepPositions);

            if( distanceP1Sheep <= sheepRadius && distanceP1Sheep < distanceP2Sheep){
                horizontal = horizontal + sheepPositions.x - player1Positions.x;
                vertical = vertical + sheepPositions.z -player1Positions.z;
            }

            else if(distanceP2Sheep <= sheepRadius && distanceP2Sheep <= distanceP1Sheep){
                horizontal = horizontal + sheepPositions.x - player2Positions.x;
                vertical = vertical + sheepPositions.z - player2Positions.z;
            }
            steering.linear = new Vector3(horizontal*sheepSpeed, 0, vertical*sheepSpeed)* agent.maxAccel;

        }else if(this.gameObject.tag == "Ghost"){

            Vector3 ghostPositions = GameObject.FindGameObjectWithTag("Ghost").transform.position;
            float distanceP1Ghost = Vector3.Distance(player1Positions, ghostPositions);
            float distanceP2Ghost = Vector3.Distance(player2Positions, ghostPositions);

            if(distanceP1Ghost < distanceP2Ghost){
                horizontal = horizontal - ghostPositions.x + player1Positions.x;
                vertical = vertical - ghostPositions.z + player1Positions.z;
            }

            else if(distanceP2Ghost <= distanceP1Ghost){
                horizontal = horizontal - ghostPositions.x + player2Positions.x;
                vertical = vertical - ghostPositions.z + player2Positions.z;
            }

            steering.linear = new Vector3(horizontal*ghostSpeed, 0, vertical*ghostSpeed)* agent.maxAccel;
        }
        
        
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel));
        }
        

        return steering;
    }

    void TransformBehavior()
    {
        audioSource1.volume = 1f;
        if(Timer.Pause == false){
            if(this.gameObject.tag == "Sheep"){
                this.gameObject.tag = "Ghost";
                    audioSource1.Play();
                    agent.SetVisualEffect(0, red, 0);
                    if(agent._celluloRobot != null){
                        agent._celluloRobot.SetVisualEffect(0, 255, 0, 0, 255);
                    }
                    
                GameObject.FindGameObjectWithTag("Player1").GetComponent<CelluloAgent>().MoveOnStone();
                GameObject.FindGameObjectWithTag("Player2").GetComponent<CelluloAgent>().MoveOnStone();

            }else{
                this.gameObject.tag = "Sheep";
                audioSource2.Play();
                agent.SetVisualEffect(0, green, 0);
                if(agent._celluloRobot != null){
                agent._celluloRobot.SetVisualEffect(0, 26, 255, 0, 255);
                }
                GameObject.FindGameObjectWithTag("Player1").GetComponent<CelluloAgent>().MoveOnIce();
                GameObject.FindGameObjectWithTag("Player2").GetComponent<CelluloAgent>().MoveOnIce();


            }

            Invoke("TransformBehavior", Random.Range(minTransformTime, maxTransformTime));
        }
    }

    void OnCollisionEnter(Collision collisionInfo){
        if(this.gameObject.CompareTag("Ghost")){
            audioSource3.volume = 1f;
            audioSource3.Play();
            Scoreboard.removePoint(collisionInfo.gameObject.tag);
        }
    }
    
        
    

}
