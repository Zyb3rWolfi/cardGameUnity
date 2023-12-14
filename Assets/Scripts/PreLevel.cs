using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLevel : MonoBehaviour
{
    public int doorNumber;

    private void OnMouseDown()
    {
        SceneManager.LoadScene("levelType1");
    }
}
