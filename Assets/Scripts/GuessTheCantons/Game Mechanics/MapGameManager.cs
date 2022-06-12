using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class MapGameManager : MonoBehaviour
{
    enum GameStates
    {
        PLAYER_1_TURN,
        PLAYER_2_TURN,
        ASSISTANT_TURN,
        PAUSED,
        END_OF_GAME,
        MISC,
        IN_MENU
    }

    enum GameMode
    {
        NAMES,
        FACTS
    }

    const int NB_OF_CANTONS = 26;
    public CelluloAgent assistant;
    public CelluloAgent player;
    

    // public Scoreboard scoreboard;

    public int number_players = 1;

    private List<string> questionBank;
    private List<string> cantonList;
    private List<string> cantonFacts;

    private List<bool> visited;

    private int randomIndex;

    private int questionsLeft;
    
    private Vector3 HOME_LOCATION;

    private GameStates current_state;
    private GameStates old_state;

    private bool firstTry = true;

    private List<string> correctReplies;
    private List<string> comboReplies;

    private List<string> wrongReplies;
    private TextMeshProUGUI textBox;

    private int cooldownCounter = 400;

    private Color32 red = new Color32(255, 0, 0, 255);
    private Color32 green = new Color32(26, 255, 0, 255);
    private Color32 emiliaColor8D = new Color32(255, 0, 178, 255);
    private Color32 idk = new Color32(0, 28, 255,255);

    private string name = "Subaru";

    private List<string> activeQuestionList;

    // Start is called before the first frame update
    void Start()
    {
        
        
        current_state = GameStates.IN_MENU;
        old_state = GameStates.MISC;
        questionsLeft = NB_OF_CANTONS;
        HOME_LOCATION = new Vector3(-5.75f, 0f, 1.92f);

        visited = new List<bool>();

        cantonList = new List<string>{"Zurich", "Bern", "Luzern", "Uri", "Schwyz", "Obwalden", 
                    "Nidwalden", "Glarus", "Zug", "Fribourg", "Solothurn", "Basel-Stadt", "Basel-Landschaft", "Schaffhausen", "Appenzell Ausserhoden",
                    "Appenzell Innerhoden", "Saint Gallen", "Graubunden", "Aargau", "Thurgau", "Ticino", "Vaud", "Valais", "Neuchatel", "Geneva", "Jura"};

        cantonFacts = new List<string>{
            "This canton's city boasts over 1200 public drinking water fountains.",
            "This canton has a bear pit (Barengraben) inside the main city.",
            "This canton's city initiated the creation of the Swiss Confederation in 1332.",
            "The possibly fictional folk hero William tell came from this canton.",
            "The world-famous swiss army knives come from this canton.",
            "This canton is home to the highest elevation suspension bridge in Europe, on Mount Titlis, it is called the Titlis Cliff Walk.",
            "Some 30 small cable cars in this canton are used by children to go to school.",
            "This canton carried out Europe's last execution for witchcraft in 1782.",
            "This canton is known as the swiss crypto valley.",
            "This canton is the home of the beloved gruyeres cheese.",
            "This canton's holy number is 11. With 11 churches, museums, historic fountains, towers and breweries named 11. It was the 11th canton to join the swiss confederation.",
            "This city canton is well known for its pharmaceutical industry.",
            "This canton was originally a Celtic settlement of the Rauraci tribe.",
            "The Rheinfalls are located in this canton, Europe's largest waterfall.",
            "Women's right to vote on a cantonal level was introduced only in 1989 in this canton.",
            "This canton is the smallest in terms population, with about 16'000 residents.",
            "This canton's city is notable for reporting the highest maximum radioactivity measurements of any swiss city.",
            "Switzerland's oldest known settlement is located in this canton.",
            "Feldschloesschen beer originates from this canton and is still produced there.",
            "This canton is known for its production of cider.",
            "The local transport company is called FART.",
            "The seat of the international olympic committee is based in this canton.",
            "There are 50 mountains over 4000m tall in this canton.",
            "This canton recorded the lowest temperature in switzerland (-41.8°C).",
            "The world wide web was created in this canton.",
            "This canton has 0 traffic lights in any of its roads."
        };
        correctReplies = new List<string>{"Well done!",
    "That's correct!",
    "Bingo!",
    "Even Puck didn't know that one!",
    "Exactly!",    "Right as rain!",
    "Nice Going!",
    "Yay you got it!",
    "Way to go!",
    "Magnificent!",
    "Purrrfect! Ain't it puck?",
    "^UwU^, Awesome job!", "Ok."};

    comboReplies = new List<string> {" You are learning soo fast!",
    "You're making it look too easy!",
    "Nothing can stop you now!",
    "I'm speechless! You're amazing!"};

    wrongReplies = new List<string> {"Oh shoot, keep on trying!",
    "One more time and you'll have it!",
    "Almost! I believe in you!",
    "Better luck next time!",
    "So close!",
    "Nope, never give up!",
    "Not this time, come on you can do it!"};

        for(int i = 0; i< NB_OF_CANTONS; i++){
            visited.Add(false);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameObject.Find("Emilia Cellulo") != null && GameObject.Find("Player") != null){
            assistant = GameObject.Find("Emilia Cellulo").GetComponent<CelluloAgent>();
            player = GameObject.Find("Player").GetComponent<CelluloAgent>();
        }
        if(cooldownCounter > 400){
            if(GameObject.Find("Dialog text")){
                textBox = GameObject.Find("Dialog text").GetComponent<TextMeshProUGUI>();
            }
            player.SetVisualEffect(0, idk, 0);
            assistant.SetVisualEffect(0, emiliaColor8D, 0);
            if(old_state != current_state){
                
                if(GameObject.Find("Cantons named")){
                    GameObject.Find("Cantons named").GetComponent<TextMeshProUGUI>().text = "Canton: " + (Score.MAX_SCORE-questionsLeft + 1) + "/" + Score.MAX_SCORE;
                }
                switch (current_state)
                {
                    case GameStates.IN_MENU:
                        if(assistant._celluloRobot != null){
                            assistant._celluloRobot.ClearTracking();
                            assistant.MoveOnIce();
                        }
                        if(player._celluloRobot != null){
                            player._celluloRobot.ClearTracking();
                            player.MoveOnIce();
                        }
                        if(GameObject.Find("Play") == null){
                            current_state = GameStates.ASSISTANT_TURN;
                            old_state = GameStates.IN_MENU;
                            FindClosestCanton.closestCanton = "";
                        }
                        if(GameObject.Find("Player name") != null){ 
                            name = GameObject.Find("Player name").GetComponent<TMP_InputField>().text;
                        }
                        if(GameObject.Find("DropdownMode") != null){
                            if(GameObject.Find("DropdownMode").GetComponent<Dropdown>().value == 0){
                                activeQuestionList = cantonList;
                            }else{
                                activeQuestionList = cantonFacts;
                            }
                        }
                        for(int i = 0; i< NB_OF_CANTONS; i++){
                            visited[i]= false;
                        }
                        break;
                    case GameStates.ASSISTANT_TURN:
                    {
                        
                        changeColor(2);
                        // Ask Question
                        System.Random rnd = new System.Random();
                        do
                        {
                            randomIndex = rnd.Next(0,NB_OF_CANTONS);
                        } while (visited[randomIndex] == true);
            
                    
                        visited[randomIndex] = true;
                        current_state = GameStates.PLAYER_1_TURN;
                        old_state = GameStates.ASSISTANT_TURN;
                    }
                    break;
                    
                    case GameStates.PLAYER_1_TURN:
                    {
                        
                        if(textBox != null){
                            if(activeQuestionList == cantonList){
                                textBox.text = "Emilia: " + name + "-san, where is " + activeQuestionList[randomIndex] + "?";
                            }else{
                                textBox.text = "Emilia: " + name + "-san, " + activeQuestionList[randomIndex];
                            }
                        }

                        // Check if answer submitted
                        if(FindClosestCanton.closestCanton == ""){
                            break;
                        }

                    
                        // If submitted, check that it is correct, and remove it from total questions
                        if(FindClosestCanton.closestCanton == cantonList[randomIndex]){
                            if(firstTry){
                                Score.add_point();   
                            }
                            textBox.text = correctReplies[randomIndex % correctReplies.Count];
                            assistant.SetVisualEffect(0, green, 0);
                            player.SetVisualEffect(0, green, 0);
                            cooldownCounter = 0;
                            changeColor(1);
                            // Flash green emilia and display text
                        }else{
                            // Move to the correct position of the answer
                            firstTry = false;
                            Score.break_streak();
                            // Emilia try again random text

                            textBox.text = "Emilia: " + wrongReplies[randomIndex % wrongReplies.Count];
                            FindClosestCanton.closestCanton = "";
                            assistant.SetVisualEffect(0, red, 0);
                            player.SetVisualEffect(0, red, 0);
                            changeColor(0);
                            cooldownCounter = 0;
                            
                            break;
                        }
                        firstTry = true;
                        if(questionsLeft == 0){
                            current_state = GameStates.END_OF_GAME;
                            old_state = GameStates.PLAYER_1_TURN;
                            break;
                        }
                        questionsLeft--;

                        // Reset closest canton
                        FindClosestCanton.closestCanton = "";

                        // Go to next state
                        old_state = GameStates.PLAYER_1_TURN;
                        current_state = GameStates.ASSISTANT_TURN;
                        
                    }
                    break;

                    case GameStates.END_OF_GAME:
                    {
                    }
                    break;
                    default:
                    break;
                    }
                }
        }else{
            cooldownCounter++;
        }
        
    }

    public void changeColor(int colorCode){
        switch (colorCode){
            case 0: // RED
                if(assistant._celluloRobot != null){
                    assistant._celluloRobot.SetVisualEffect(0, 255, 0, 0, 255);
                }
                if( player._celluloRobot != null){
                    player._celluloRobot.SetVisualEffect(0, 255, 0, 0, 255);
                }
                break;
            case 1: // GREEN
                if(assistant._celluloRobot != null){
                    assistant._celluloRobot.SetVisualEffect(0, 26, 255, 0, 255);
                }
                if( player._celluloRobot != null){
                    player._celluloRobot.SetVisualEffect(0, 26, 255, 0, 255);
                }
                break;
            default: // DEFAULT
                if(assistant._celluloRobot != null){
                    assistant._celluloRobot.SetVisualEffect(0, 0, 255, 100, 255);
                }
                if( player._celluloRobot != null){
                    player._celluloRobot.SetVisualEffect(0,0, 28, 255,255);
                }
                break;
        }
    }   
}

