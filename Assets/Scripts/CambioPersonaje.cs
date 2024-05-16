using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CambioPersonaje : MonoBehaviour
{
    public List<GameObject> characters; 
    private int currentIndex = 0;
    public List<IAFollow> IaControllers;
    public TPSController controllers;
    public TPSControllerJac controllerJac;

    public GameObject lightNat; 
    public GameObject lightJac;

    public NavMeshAgent AgentNat;
    public NavMeshAgent AgentJac;

    FollowCamera cameraScript;

    void Awake()
    {
        cameraScript = Camera.main.GetComponent<FollowCamera>();
    }

    void Start()
    {
        //ChangeCharacter(0);
        foreach(GameObject character in characters)
        {
            IAFollow Ia = character.GetComponent<IAFollow>();
            IaControllers.Add(Ia);
            TPSController controller = character.GetComponentInChildren<TPSController>();
            if( controller != null)
            {
                controllers = controller;
            }
            TPSControllerJac Jac = character.GetComponentInChildren<TPSControllerJac>();
            if( Jac != null)
            {
                controllerJac = Jac;
            }
            NavMeshAgent Nav = character.GetComponentInChildren<NavMeshAgent>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeCharacter((currentIndex + 1) % characters.Count);
        }
    }

    void ChangeCharacter(int newIndex)
    {
        currentIndex = newIndex;
        //IaControllers[currentIndex].enabled = true;
        //IaControllers[(currentIndex + 1) % characters.Count].enabled = false;
        /*if(currentIndex == 0)
        {
            controllers.enabled = false;
        }
        else
        {
            controllerJac.enabled = false;
        }
        if(currentIndex == 0)
        {
            controllers.enabled = false;
        }
        else
        {
            controllerJac.enabled = false;
        }*/

        if(currentIndex == 0)
        {
            IaControllers[1].agent.Stop();

            controllers.enabled = false;
            controllerJac.enabled = true;
            IaControllers[0].enabled = true;
            IaControllers[1].enabled = false;
            cameraScript.followPlayer = characters[1].transform;

            IaControllers[0].agent.Resume();

            lightJac.SetActive(true);
            lightNat.SetActive(false);

            AgentNat.enabled = true;
            AgentJac.enabled = false;
            
        }
        else
        {
            IaControllers[0].agent.Stop();

            controllers.enabled = true;
            controllerJac.enabled = false;
            IaControllers[0].enabled = false;
            IaControllers[1].enabled = true;
            cameraScript.followPlayer = characters[0].transform;

            IaControllers[1].agent.Resume();

            lightNat.SetActive(true);
            lightJac.SetActive(false);

            
            AgentNat.enabled = false;
            AgentJac.enabled = true;
        }
    }
}