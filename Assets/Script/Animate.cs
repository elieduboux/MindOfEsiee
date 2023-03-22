using MyMathTools;
using System .Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Animate : MonoBehaviour
{

    [SerializeField]
    float m_TranslationSpeed;
    [SerializeField] Transform TransformCenter;
    //Transform m_Target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //translation with Translate method and Local (self) coordinates.
        //Vector3 moveVect = transform.forward * m_TranslationSpeed * Time.deltaTime;
        //transform.Translate(moveVect, Space.World);

        //translation with transform.position method and World coordinates.
        //Vector3 moveVect = new Vector3(0, 0, 1) * m_TranslationSpeed * Time.deltaTime;
        //transform.Translate(moveVect, Space.Self);

        //translation with transform.position method and World coordinates.
        //transform.position = new Vector3(Mathf.Sin(Time.time * 4), Mathf.Cos(Time.time * 3), 552+ Time.time * 2);

        //Cylindrical cyl = new Cylindrical(5, Time.time, Mathf.PingPong(Time.time, 7));
        //transform.position = m_TranslationSpeed * CoordConvert.CylindricalToCartesian(cyl);

        Spherical sph = new Spherical(13, Time.time * Mathf.PI/2 * m_TranslationSpeed, Mathf.PI/2);
        transform.position = CoordConvert.SphericalToCartesian(sph);
        transform.position += TransformCenter.position;

        transform.LookAt(transform.position);

        //m_Target.position = transform.position + Vector3.forward;
        //transform.LookAt(m_Target.position);
    }

}
