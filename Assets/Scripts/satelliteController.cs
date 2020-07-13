using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satelliteController : MonoBehaviour
{

    [SerializeField] private float FlySpeed = 40f;
    [SerializeField] private float m_MovementSmoothing = 0.36f;

    private Vector3 m_TargetVelocity = Vector2.zero;
    private Vector3 m_Velocity = Vector2.zero;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_TargetVelocity.x = Input.GetAxisRaw("Horizontal") * FlySpeed;
        m_TargetVelocity.y = Input.GetAxisRaw("Vertical") * FlySpeed;
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, m_TargetVelocity * Time.fixedDeltaTime, ref m_Velocity, m_MovementSmoothing);
    }
}
