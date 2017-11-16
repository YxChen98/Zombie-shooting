﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public Transform m_transform;
    CharacterController m_ch;

    Transform m_camTransform;
    Vector3 m_camRotation;
    float m_camHeight = 1.4f;

    public float m_movSpeed = 3.0f;
    public float m_gravity = 2.0f;
    public int m_life = 5;

    Transform m_muzzlepoint;
    public LayerMask m_layer;
    public Transform m_fx;
    public AudioClip m_audio;
    float m_shootTimer = 0;


// Use this for initialization
void Start () {
        m_transform = this.transform;
        m_ch = this.GetComponent<CharacterController>();

        //Get camera and initialise
        m_camTransform = Camera.main.transform;
        Vector3 pos = m_transform.position;
        pos.y += m_camHeight;
        m_camTransform.position = pos;

        //Same rotation as player
        m_camTransform.rotation = m_transform.rotation;
        m_camRotation = m_camTransform.eulerAngles;
        Screen.lockCursor = true;

        m_muzzlepoint = m_camTransform.Find("M16/weapon/muzzlepoint").transform;
    }

    public void OnDamage(int damage)
    {
        m_life -= damage;
        GameManager.Instance.SetLife(m_life);
        if (m_life <= 0)
        {
            Screen.lockCursor = false;
        }
    }

    void Control()
    {
        float xm = 0;
        float ym = 0;
        float zm = 0;


        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        m_camRotation.x -= rv;
        m_camRotation.y += rh;
        m_camTransform.eulerAngles = m_camRotation;

        //Same facing direction
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0;
        camrot.z = 0;
        m_transform.eulerAngles = camrot;


        ym -= m_gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zm -= m_movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xm += m_movSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            xm -= m_movSpeed * Time.deltaTime;
        }

        m_ch.Move(m_transform.TransformDirection(new Vector3(xm, ym, zm)));

        Vector3 pos = m_transform.position;
        pos.y += m_camHeight;
        m_camTransform.position = pos;
    }

    // Update is called once per frame
    void Update () {
        if (m_life <= 0)
        {
            return;
        }
        Control();

  

        m_shootTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0)&& m_shootTimer <= 0)
        {
            m_shootTimer = 0.09f;
            this.GetComponent<AudioSource>().PlayOneShot(m_audio);
            GameManager.Instance.SetAmmo(1);
            
            RaycastHit info;
            bool hit = Physics.Raycast(m_muzzlepoint.position, m_camTransform.TransformDirection(Vector3.forward), out info, 100, m_layer);
            if (hit)
            {
                if (info.transform.tag.CompareTo("enemy")==0)
                {
                    Zombie enemy = info.transform.GetComponent<Zombie>();
                    enemy.OnDamage(1);
                }
                Instantiate(m_fx, info.point, info.transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.Reload();
        }

       
	}


    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Spawn.tif");
    }

   
}
