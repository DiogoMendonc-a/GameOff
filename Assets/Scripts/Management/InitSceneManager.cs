using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneManager : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.ReturnToMenu();
    }
}
