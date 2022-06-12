using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
    private TextMeshProUGUI timerText;
    public Slider slider;
    public float maxMinutes = 2;
    public static float lengthMinutes; // default value
    public Text time_field;


    public static bool Pause = false;
    public static bool Pause2 = false;


    public GameObject gameOverCanvas;

    private float minutes = 0.0f;

    
    public static void setMinutes(float mins){
        if(mins <= 99.0f && mins > 0.0f)
        lengthMinutes = mins;
    }


    public void Awake() {
        initTimerValue = Time.time; 
    }
    
    // Start is called before the first frame update
    public void Start() {

        timerText = this.gameObject.GetComponent<TextMeshProUGUI>();
        timerText.text = "TIME " + string.Format("{0:00}:{1:00}", 0, 0);
        initTimerValue = 0.0f;
        lengthMinutes = maxMinutes;
    }

    // Update is called once per frame
    public void Update() {

        if(!Pause) {
            initTimerValue += Time.deltaTime;
        }

        if(initTimerValue >= 60*(minutes + 1)){
            minutes++;
        }   
        
        timerText.text = "TIME " + string.Format("{0:00}:{1:00}", minutes, initTimerValue % 60.0f);
        slider.value = initTimerValue/(60* (int)lengthMinutes);
        if(initTimerValue >= 60*lengthMinutes){
                gameOverCanvas.SetActive(true);
                Pause = true;
                if(Pause2){
                    FindWinner.winnerText();
                    if (GameObject.FindGameObjectWithTag("Player1") != null){
                        GameObject.FindGameObjectWithTag("Player1").SetActive(false);
                    }
                    if (GameObject.FindGameObjectWithTag("Player2") != null){
                        GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                    }
                    if (GameObject.FindGameObjectWithTag("Sheep") != null){
                        GameObject.FindGameObjectWithTag("Sheep").SetActive(false);
                    }
                    if (GameObject.FindGameObjectWithTag("Ghost") != null){
                        GameObject.FindGameObjectWithTag("Ghost").SetActive(false);
                    }

                }
                Pause2 = true;
        }
    }

}