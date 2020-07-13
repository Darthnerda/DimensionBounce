using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FootballController : MonoBehaviour, Runs, HandlesLevelNext
{
    // Tweak zone
    [SerializeField] private float m_Speed = 0.5f;
    [SerializeField] private float m_AngleRotate = 45f;
    [SerializeField] private float m_ColliderCooldown = 4f;
    [SerializeField] private Color[] ChannelColors = { new Color(1,1,1,1), new Color(1,0,0,1) };

    //References
    public GameObject ModifierHandler;
    [HideInInspector]
    public GameObject m_StartPortal;
    public GameObject m_GameButton;
    public GameObject m_SuccessText;
    private AudioManager d_AudioManager;

    // Private State
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_PlayerSpriteRenderer;
    private ModifierHandler m_ModifierHandler;
    private int m_Channel = 1;
    private int m_RoomInitialChannel = 1;

    private void Start()
    {
        m_PlayerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_ModifierHandler = ModifierHandler.GetComponent(typeof(ModifierHandler)) as ModifierHandler;
        m_StartPortal = System.Array.Find(m_ModifierHandler.g_ModifierTriplets, ele => ele.tag == "StartPortal");
        //Debug.Log("LOUD AND PROUD: " + m_StartPortal);
        d_AudioManager = FindObjectOfType<AudioManager>();
        d_AudioManager.Play("mystic");
    }

    private void EffectedBy( int m_ModifierIdx )
    {

        int m_ModifierEffect = m_ModifierHandler.g_ModifierImports[m_ModifierIdx].effect;
        m_Rigidbody2D.velocity = Quaternion.Euler(0,0,m_AngleRotate * m_ModifierEffect * m_Channel) * m_Rigidbody2D.velocity;
        if ( m_ModifierEffect == 0 ) {
            m_Channel = m_Channel * -1;
            m_PlayerSpriteRenderer.color = ChannelColors[(m_Channel+1) % 2];
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ExecuteEvents.Execute<DeathHandler>(gameObject, null, (x,y)=>x.OnDied());
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col) // collided with either the click or the gfx
    {
        string TagOfCollider = col.gameObject.tag; // get the tag of the collider
        string TagOfTriggerParent = col.gameObject.transform.parent.gameObject.tag; // get the tag of the parent
        int m_ModifierIdx = System.Array.IndexOf(m_ModifierHandler.g_ModifierImports.Select(x => x.tag).ToArray(),TagOfTriggerParent); // get the idx of the tag in the modiferimports array

        if(TagOfCollider != "ClickTarget" && m_ModifierIdx >= 0) // If we didn't collide with the click collider
        {
            if (TagOfTriggerParent == "ExitPortal") { // and we hit the exit portal
                //Debug.Log("Hit exit portal");

                // freeze the ball
                m_Rigidbody2D.velocity = new Vector3(0,0,0);

                // Send off a message to the game button
                ExecuteEvents.Execute<HandlesExitPortal>(m_GameButton, null, (x,y)=>x.OnExitPortal());
                ExecuteEvents.Execute<HandlesExitPortal>(m_SuccessText, null, (x,y)=>x.OnExitPortal());
            } else if (TagOfTriggerParent != "StartPortal") { // but if we didn't hit the exit portal or start portal
              //Debug.Log("Triggered by modifier with tag: " + TagOfTriggerParent + ". Channel is: " + m_Channel + ".");
              EffectedBy(m_ModifierIdx);

              col.enabled = false;
              yield return new WaitForSeconds (m_ColliderCooldown);
              col.enabled = true;
            }
        }
    }

    public void OnNextLevel()
    {
        ReturnToStart();
    }

    public void OnDied()
    {
        m_RoomInitialChannel = m_Channel;
        ReturnToStart();
    }

    private void ReturnToStart()
    {
      // reset channel to room Initialization
      //Debug.Log(m_Channel + " " + m_RoomInitialChannel);
      m_Channel = m_RoomInitialChannel;
      m_PlayerSpriteRenderer.color = ChannelColors[(m_Channel+1) % 2];

      // teleport to start portal
      m_Rigidbody2D.rotation = 0;
      m_Rigidbody2D.angularVelocity = 0;
      m_Rigidbody2D.velocity = new Vector3(0,0,0);
      m_Rigidbody2D.position = m_StartPortal.transform.position + new Vector3(0.5f,-0.3f,0);
    }

    public void OnRunEnter()
    {
        m_Rigidbody2D.velocity = Quaternion.Euler(0,0,-30) * new Vector3(m_Speed,0,0);
    }

    public void OnRunExit()
    {
        ReturnToStart();
    }
}
