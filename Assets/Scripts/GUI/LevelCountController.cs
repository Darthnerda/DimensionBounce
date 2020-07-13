using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCountController : MonoBehaviour, HandlesExitPortal, HandlesLevelNext
{
    // References
    private Text t_SwitchCountText;
    public GameObject t_RoomSuccess;

    // Tweak
    [SerializeField] private int t_LevelCount = 1;

    // Private State
    private int t_RoomCount = 1;
    private Text t_RoomSuccessText;
    private int numLevels;

    private void Awake()
    {
        numLevels = SceneManager.sceneCount;
        t_SwitchCountText = GetComponent<Text>();
        t_RoomSuccessText = t_RoomSuccess.GetComponent<Text>();
    }

    public void OnExitPortal()
    {
        t_RoomSuccessText.text = "You Made It To The Next Wormhole!\n Continue?";
    }

    public void OnNextLevel()
    {
        t_RoomCount++;
        t_SwitchCountText.text = System.String.Format("Dimension {0}-{1}", t_LevelCount.ToString(), t_RoomCount.ToString());
        t_RoomSuccessText.text = "";
    }
}
