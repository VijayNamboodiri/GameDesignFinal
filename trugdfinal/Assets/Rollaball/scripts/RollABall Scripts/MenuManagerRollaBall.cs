using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManagerRollaBall : MonoBehaviour
{
    public string MenuLvl;
    public string nextLvl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMainMenu()
    {
        Debug.Log("called");
        SceneManager.LoadScene(MenuLvl);

    }
    public void NeccLevl()
    {
        Debug.Log("called");
        SceneManager.LoadScene(nextLvl);
    }
}
