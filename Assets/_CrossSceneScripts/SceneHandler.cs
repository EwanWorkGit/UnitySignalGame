using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;


    public int DayIndex = 0;
    public int GameplayIndex = 1;

    //singleton that destroys others if they exist
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
        else if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EnterGameplayScene()
    {
        SceneManager.LoadScene(GameplayIndex);
    }
}
