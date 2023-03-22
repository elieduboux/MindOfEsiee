using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float m_TranslationSpeed;  // in m/s
    [SerializeField] float m_RotationSpeed;     // in °/s

    Rigidbody m_Rb;

    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_TranslationSpeed = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {

        //kinematic --> Update, transform, deltaTime
        //Changes apply immediatly

        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");

        Vector3 moveVect = vAxis * transform.forward * m_TranslationSpeed * Time.deltaTime;
        moveVect += Vector3.up * Time.deltaTime * jump * 10;
        transform.Translate(moveVect, Space.World);

        float rotAngle = hAxis * m_RotationSpeed * Time.deltaTime;
        transform.Rotate(transform.up, rotAngle);

    }

    private void FixedUpdate()
    {
        //dynamic --> FixedUpdate, rigidBody, fixed deltaTime
        //Changes apply on next frame

        //float vAxis = Input.GetAxis("Vertical");
        //float hAxis = Input.GetAxisRaw("Horizontal");

        //// TELEPORTATION MODE

        //Vector3 moveVect = vAxis * transform.forward * m_TranslationSpeed * Time.fixedDeltaTime;
        //m_Rb.MovePosition(transform.position + moveVect);

        //float rotAngle = hAxis * m_RotationSpeed * Time.fixedDeltaTime;
        //Quaternion qRot = Quaternion.AngleAxis(rotAngle, transform.up);
        //Quaternion qFinalOrient = qRot * transform.rotation;
        //m_Rb.MoveRotation(qFinalOrient);

        //// VELOCITY MODE
        //Vector3 targetVelocity = vAxis * m_TranslationSpeed * transform.forward;
        //Vector3 velocityChange = targetVelocity - m_Rb.velocity;
        //m_Rb.AddForce(velocityChange, ForceMode.VelocityChange);

        //Vector3 targetAngularVelocity = hAxis * m_RotationSpeed * transform.up;
        //Vector3 velocityAngularChange = targetAngularVelocity - m_Rb.angularVelocity;
        //m_Rb.AddTorque(velocityAngularChange, ForceMode.VelocityChange);
    }

    public void setSpeed(float a)
    {
        if (a > 20 | a < 1)
        {
            m_TranslationSpeed = 3;
        }
        else
        {
            m_TranslationSpeed = a;
        }
        
    }
}
