using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluloButtonInteract : AgentBehaviour
{
    public CelluloAgent playerAgent;
    private int cooldown_timer = 300;
    private bool onCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkButtonPressed()){
            if(!onCooldown){
                FindClosestCanton.findClosestCanton(this.gameObject.transform.position);
                onCooldown = true;
                cooldown_timer = 0;
            }
        }
        // Reset cooldown
        if(cooldown_timer < 300){
            cooldown_timer++;
        }else{
            onCooldown = false;
        }
    }

    // Checks if a single button has been long pressed for a while
    bool checkButtonPressed(){
        Cellulo robot = playerAgent._celluloRobot;
        if(robot == null){
            return false;
        }
        bool isLongPressed = false;
        for(int i = 0; i < 6; i++){
            if(robot.TouchKeys[i] == Touch.LongTouch){
                isLongPressed = true;
                return true;
            }else{
                return false;
            }
        }
        return isLongPressed;
    }
}
