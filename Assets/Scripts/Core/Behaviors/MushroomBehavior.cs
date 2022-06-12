using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    public static bool player1Boosted = false;
    public static bool player2Boosted = false;
    public AudioSource appearAudio;
    public AudioSource collectedAudio;

    public void Start(){
        Invoke("SpawnMushroom", Random.Range(0f,0f));
    }

    void OnTriggerEnter(Collider other){
        if(!Timer.Pause){
            if(other.transform.parent.tag == "Player1"){
                player1Boosted = true;
                collectedAudio.Play();
                Invoke("DisappearMushroom", 0.3f);
                Invoke("SpawnMushroom", Random.Range(10, 20));

            }else if(other.transform.parent.tag == "Player2"){
                player2Boosted = true;
                collectedAudio.Play();
                Invoke("DisappearMushroom", 0.3f);
                Invoke("SpawnMushroom", Random.Range(10, 20));
            }
        }
        
    }
    void DisappearMushroom(){
        this.transform.parent.gameObject.SetActive(false);
    }

    void SpawnMushroom(){
        this.transform.parent.gameObject.SetActive(true);
        appearAudio.Play();
        float newX = Random.Range(0f, 27f);
        float newZ = Random.Range(-1f, -18f);
        this.transform.parent.gameObject.transform.position = new Vector3(newX, 0, newZ);
    }
}
