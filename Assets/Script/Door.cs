using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Animator.SetBool("isOpen",true);
    }

    private void OnTriggerExit(Collider other)
    {
        m_Animator.SetBool("isOpen", false);
    }
}
