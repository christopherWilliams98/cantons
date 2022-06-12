using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume =0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other){
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        if(other.transform.parent.gameObject.CompareTag("Sheep")){
            Scoreboard.givePoint();
            audioSource.volume =1f;
            audioSource.Play();
        }
    }

}
