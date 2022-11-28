using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameState state;
    // public static event Action<GameState> onGameStateChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGamesState(GameState newState)
    {
        state = newState;

        switch (state){
            case GameState.spawn:
                Debug.Log("Spawn");
                break;
            case GameState.boss:
                Debug.Log("Boss");
                break;
            case GameState.lose:
                Debug.Log("Lose");
                break;
            // default:
                // throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }

        // onGameStateChanged?.Invoke(state);
    }
    
}

public enum GameState
{
    spawn,
    boss,
    lose
}