using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update

    public void restartStuff(){
        SceneManager.LoadScene("Restart");
        SceneManager.LoadScene("GuessTheCantons");
        Score.reset_game();
    }

}
