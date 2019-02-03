using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SelectWorlds;

    // Start is called before the first frame update
    public void Start()
    {
        MainMenu.SetActive(false);
        SelectWorlds.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
