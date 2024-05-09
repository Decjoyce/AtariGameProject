using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class ControlsMenuNavigation : MonoBehaviour
{
    public GameObject controls;
    public GameObject tutorial;

    /*public void swap()
    {
        if(Input.GetButtonDown(name))
        {
            if(controls.SetActive(false))
            {

            }
        }
    }*/

    public void controlsActive()
    {
        controls.SetActive(true);
        tutorial.SetActive(false);
    }

    public void tutorialActive()
    {
        controls.SetActive(false);
        tutorial.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.SetActive(true);
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        GameManager.instance.EndRun();
    }
}
