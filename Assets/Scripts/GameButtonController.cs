using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour, HandlesExitPortal
{
    // References
    public GameObject b_DimensionTilemap;
    public GameObject b_Football;
    public GameObject b_ModifierHandler;
    public GameObject b_LevelCounter;

    // Tweak zone
    public string[] b_ButtonStateStrings = {"Run", "Stop", "Next"};

    // Private state
    private int b_ButtonState = 0;
    private WallTilemapController b_DimensionTileController;

    private void Start()
    {
        b_DimensionTileController = b_DimensionTilemap.GetComponent(typeof(WallTilemapController)) as WallTilemapController;
    }

    public void OnExitPortal()
    {
        Debug.Log("attempted exit portal shit");
        if(b_ButtonState == 1)
        {
            if(b_DimensionTileController.IsLastDimension())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            b_ButtonState++;
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
      Text b_ButtonText = gameObject.GetComponentInChildren<Text>();
      b_ButtonText.text = b_ButtonStateStrings[b_ButtonState];
    }

    public void ButtonClicked()
    {

        switch (b_ButtonState)
        {
        case 0:
            ExecuteEvents.Execute<Runs>(b_Football, null, (x,y)=>x.OnRunEnter());
            ExecuteEvents.Execute<Runs>(b_ModifierHandler, null, (x,y)=>x.OnRunEnter());
            break;
        case 1:
            ExecuteEvents.Execute<Runs>(b_Football, null, (x,y)=>x.OnRunExit());
            break;
        case 2:
            ExecuteEvents.Execute<HandlesLevelNext>(b_DimensionTilemap, null, (x,y)=>x.OnNextLevel());
            ExecuteEvents.Execute<HandlesLevelNext>(b_Football, null, (x,y)=>x.OnNextLevel());
            ExecuteEvents.Execute<HandlesLevelNext>(b_ModifierHandler, null, (x,y)=>x.OnNextLevel());
            ExecuteEvents.Execute<HandlesLevelNext>(b_LevelCounter, null, (x,y)=>x.OnNextLevel());
            break;
        }

        // Change the button text
        int b_StateAdjuster = b_ButtonState == 1 ? -1 : 1;
        b_ButtonState = (b_ButtonState + b_StateAdjuster) % 3;
        UpdateButtonText();
    }
}
