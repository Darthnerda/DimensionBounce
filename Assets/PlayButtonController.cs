using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("Attempting");
        SceneManager.LoadScene(1);
    }
}
