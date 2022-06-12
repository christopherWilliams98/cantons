using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    int cooldown_timer = 300;
    bool onCooldown = false;

    // Update is called once per frame
    void Update()
    {   
        // Freaky stuff in update to make sure it doesn't send multiple method calls on one press
        if(Input.GetKey(KeyCode.Return)){
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
}
