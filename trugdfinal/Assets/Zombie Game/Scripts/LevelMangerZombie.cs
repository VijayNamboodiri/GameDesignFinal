using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelMangerZombie : MonoBehaviour
{
    public static LevelMangerZombie instance;

    public Transform startPoint;
    public GameObject User;

    
    public string nextLevel;
    public float waitToLoad = 4f;

    public GameObject deathScreen;
    public GameObject victoryScreen;
    public GameObject pauseScreen;
    
    public GameObject livingMap;
    public GameObject deathMap;
    public GameObject winMap;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;
    public string newGameScene;

    public AudioSource mayday;
    public GameObject player;
    //public GameObject reloadButton;

    public string menuLevel;

    bool paused = false;


    public List<GameObject> enemies = new List<GameObject>();


    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       User.transform.position = startPoint.position;
       deathScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a,0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a,1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }        
        if(enemies.Count > 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);

                    i--;
                }
            }

            if(enemies.Count == 0)
            {
                victoryScreen.SetActive(true);
                winMap.SetActive(true);
            }
        }
        if(deathScreen.activeSelf == true)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("SPACE DETECTED");
                NewGame();
            }
        }           

        if(victoryScreen.activeSelf == true)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("SPACE DETECTED");
                StartCoroutine(LevelEnd());
            }
        }   
        if(TopDownHealthControll.instance.health <= 0)
        {
            RunDeathSound();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            pasueFlip();
        }
    }
    void pasueFlip()
    {
        paused = !paused;   
        pauseScreen.SetActive(paused);
        if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public IEnumerator LevelEnd()
    {
        Debug.Log("level end called");
        StartFadeToBlack();
        
        yield return new WaitForSeconds(waitToLoad);
    
        SceneManager.LoadScene(nextLevel);
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
    public void NewGame()
    {
        Debug.Log("called");
        SceneManager.LoadScene(newGameScene);

    }
    public void RunDeathSound()
    {
        Debug.Log("Sound Played");
        mayday.Play();
    }
    public void TakeToMenu()
    {
        SceneManager.LoadScene(menuLevel);
    }
    public void nextLvl()
    {
        StartCoroutine(LevelEnd());
    }
   
}
