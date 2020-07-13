using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierClickObjectController : MonoBehaviour, Runs
{

  // References
  public ModifierTripletController m_ModifierTripletController;

  // Private State
  private bool m_AxesWereIdle = true;
  private bool m_CanClick = true;

  public void OnMouseOver()
  {
      if(m_CanClick)
      {
          if(Input.GetAxisRaw("Fire1") != 0 ^ Input.GetAxisRaw("Fire2") != 0)
          {
              if(m_AxesWereIdle)
              {
                  Debug.Log("Clicked on: " + gameObject.transform.parent.gameObject.tag);
                  m_ModifierTripletController.ChangeModifierType( (int)(Input.GetAxisRaw("Fire1") - Input.GetAxisRaw("Fire2")) );
                  m_AxesWereIdle = false;
              }
          }
          else if (!m_AxesWereIdle) {
              m_AxesWereIdle = true;
          }
      }
  }

  public void OnRunEnter()
  {
      m_CanClick = false;
  }

  public void OnRunExit()
  {
      m_CanClick = true;
  }
}
