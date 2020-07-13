using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierTripletController : MonoBehaviour, HandlesLevelNext
{
    // References
    public GameObject m_ModifierClickObject;
    public GameObject m_ModifierSpriteObject;
    public ModifierImport[] m_ModifierImports;

    // Private State
    private bool m_AxesWereIdle = true;
    private bool m_Locked = false;
    private int m_RoomInitialModifierIdx = 0;
    private int m_ModifierIdx = 0;
    private int m_OrderIdx = 0;
    private int[] m_Order = {0, 1, 2};
    //private int m_ModifierIdx = 0;

    public void ChangeModifierType(int m_SwitchDirection)
    {
      if (!m_Locked)
      {
          // Figures out new type name WIP
          int m_NewModifierIdx = (m_ModifierIdx + m_SwitchDirection) % 3;

          // Changes GFX sprite
          SpriteRenderer renderer = m_ModifierSpriteObject.GetComponent<SpriteRenderer>();
          renderer.sprite = m_ModifierImports[m_NewModifierIdx].sprite;

          // Changes triplet object tag
          gameObject.tag = m_ModifierImports[m_NewModifierIdx].tag;

          // Updates the index state
          m_ModifierIdx = m_NewModifierIdx;
          Debug.Log("ModifierIdx is: " + m_ModifierIdx);
      }
    }

    private void Lock()
    {
        m_Locked = true;
        SpriteRenderer renderer = m_ModifierSpriteObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0,0,1);
    }

    public void OnNextLevel()
    {

        // If the modifier has been changed, change the order and lock this
        if (m_RoomInitialModifierIdx != m_ModifierIdx)
        {
            m_Order[m_RoomInitialModifierIdx] = m_ModifierIdx;
            m_Order[m_ModifierIdx] = m_RoomInitialModifierIdx;
            Lock();
        }

        // Then Advance through the order
        SpriteRenderer renderer = m_ModifierSpriteObject.GetComponent<SpriteRenderer>();
        renderer.sprite = m_ModifierImports[m_Order[++m_OrderIdx]].sprite;
        // Changes triplet object tag
        gameObject.tag = m_ModifierImports[m_Order[m_OrderIdx]].tag;

        // Update the initial modifier idx
        m_RoomInitialModifierIdx = m_ModifierIdx;
    }
}
