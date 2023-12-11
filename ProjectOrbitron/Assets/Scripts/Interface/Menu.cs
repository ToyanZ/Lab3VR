using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Menu : MonoBehaviour
{
    private Vector3 objectToLookPosition;
    public GameObject objectToLook;
    public GameObject objectThatLook;
    public GameObject scrollBar;
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject player;
    public GameObject disc;
    // Start is called before the first frame update
    void Start()
    {
        scrollBar.GetComponent<Scrollbar>().value = 0;
        objectToLook = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Awake()
    {
        scrollBar.GetComponent<Scrollbar>().value = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtObject();
        UpdateColorButton();
        setLocationMenu();
    }
    public void LookAtObject()
    {
        objectToLookPosition = objectToLook.transform.position;
        objectToLookPosition.y = objectThatLook.transform.position.y;
        objectThatLook.transform.LookAt(objectToLookPosition);
    }

    public void SetScrollBarValue(float value)
    {
        scrollBar.GetComponent<Scrollbar>().value = value;
    }

    public void UpdateColorButton()
    {
        if(scrollBar.GetComponent<Scrollbar>().value <= 0.1f)
        {
            leftButton.GetComponent<Image>().color = Color.gray;
            rightButton.GetComponent<Image>().color = Color.white;
        }else if(scrollBar.GetComponent<Scrollbar>().value >= 0.9f)
        {
            leftButton.GetComponent<Image>().color = Color.white;
            rightButton.GetComponent<Image>().color = Color.gray;
        }
    }

    public void HealthButton(float  amount)
    {
        float healthPlayer; 
        healthPlayer = player.GetComponent<DynamicTarget>().GetHealth() + amount;
        player.GetComponent<DynamicTarget>().SetHealth(healthPlayer);   
    }

    public void MicroHealthbutton(float amount)
    {
        for(int i = 0; i < 3; i++)
        {
            float healthPlayer;
            healthPlayer = player.GetComponent<DynamicTarget>().GetHealth() + amount;
            player.GetComponent<DynamicTarget>().SetHealth(healthPlayer);
        }
    }

    public void setLocationMenu()
    {
        objectThatLook.transform.position = new Vector3(disc.transform.position.x, disc.transform.position.y + 1, disc.transform.position.z);
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("Tittle");
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
