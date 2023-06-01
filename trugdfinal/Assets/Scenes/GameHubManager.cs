using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHubManager : MonoBehaviour
{
    // Scene names for the zombie game and rolla bolla
    public string zombieGame;
    public string rollaBolla;
    
    // Delay before loading the scene
    public float waitToLoad = 4f;

    void Start()
    {
        // Start the coroutine to load the zombie scene with a delay
        StartCoroutine(LoadZombieDelayed());
    }

    void Update()
    {
        // Update method can be used for any necessary continuous actions or checks
    }

    // Coroutine to load the zombie scene with a delay
 private IEnumerator LoadZombieDelayed()
{
    yield return new WaitForSeconds(waitToLoad);
    SceneManager.LoadScene("ZombieGameLvl1");
}


    // Coroutine to load the rolla bolla scene with a delay
    private IEnumerator LoadRollaDelayed()
    {
        // Wait for the specified delay before proceeding
        yield return new WaitForSeconds(waitToLoad);

        // Load the rolla bolla scene using the SceneManager
        SceneManager.LoadScene(rollaBolla);
    }

    // Public method to trigger loading the zombie scene
    public void LoadZombie()
    {
        // Start the coroutine to load the zombie scene with a delay
        StartCoroutine(LoadZombieDelayed());
    }

    // Public method to trigger loading the rolla bolla scene
    public void LoadRolla()
    {
        // Start the coroutine to load the rolla bolla scene with a delay
        StartCoroutine(LoadRollaDelayed());
    }
}
