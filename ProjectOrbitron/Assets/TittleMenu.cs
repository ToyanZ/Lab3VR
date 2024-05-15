using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadLVL1()
    {
        SceneManager.LoadScene("Level 01");
    }
}