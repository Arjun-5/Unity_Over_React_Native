using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Characters = null;

    [SerializeField]
    private GameObject[] gunPointers = null;

    [SerializeField]
    private GameObject destroyedObject = null;

    [SerializeField]
    private GameObject sparkEffect = null;

    private Touch UserTouch;
    private RaycastHit bulletHit;
    private GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;
    private float moveSpeed;
    private bool touchActive;
    private bool shootactive;
    private Quaternion rotationY;
    private Vector3 moveDirection;
    private float totalDamage = 0;

    private int index;
    //public variables

    // Start is called before the first frame update
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        moveSpeed = 4.5f;
        touchActive = false;
        shootactive = false;
        foreach(GameObject character in Characters)
        {
            character.SetActive(false);
        }
        index = (UI_Character_Selection.FemaleVisible == true) ? 1 : 0;
        Characters[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            UserTouch = Input.GetTouch(0);

            if(UserTouch.phase == TouchPhase.Began)
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = UserTouch.position;

                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);
                if (results.Count > 0)
                {
                    if (results[0].gameObject.CompareTag("Forward_Dir"))
                        touchActive = true;
                    if (results[0].gameObject.CompareTag("Shoot"))
                        shootactive = true;
                }
            }
            else if(UserTouch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, UserTouch.deltaPosition.x * 0.1f, 0f);
                Characters[index].transform.rotation *= rotationY;
            }
            else if(UserTouch.phase == TouchPhase.Ended)
            {
                touchActive = false;
                Characters[index].GetComponent<Animator>().SetBool("IsRunning", false);
                shootactive = false;
                Characters[index].GetComponent<Animator>().SetBool("IsShooting", false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (touchActive == true)
        {
            moveDirection = new Vector3(0f, 0f, moveSpeed);

            //Move forward in the Z direction based on the character's transform direction
            Characters[index].GetComponent<Rigidbody>().velocity = Characters[index].transform.TransformDirection(moveDirection);
            Characters[index].GetComponent<Animator>().SetBool("IsRunning", true);
        }
        if (shootactive)
        {
            Characters[index].GetComponent<Animator>().SetBool("IsShooting", true);
            if (!Characters[index].GetComponent<AudioSource>().isPlaying)
            {
                Characters[index].GetComponent<AudioSource>().Play();
            }

            if (Physics.Raycast(gunPointers[index].transform.position, gunPointers[index].transform.forward, out bulletHit,Mathf.Infinity))
            {
                if (bulletHit.collider.gameObject.CompareTag("ShootTarget"))
                {
                    if(totalDamage <= 1300)
                    {
                        GameObject particle = Instantiate(sparkEffect, bulletHit.point, Quaternion.LookRotation(bulletHit.normal));
                        Destroy(particle, 1f);
                    }
                    totalDamage += 10;
                    if(totalDamage >= 1500)
                    {
                        totalDamage = 0;
                        Instantiate(destroyedObject, bulletHit.transform.position, bulletHit.transform.rotation);
                        Destroy(bulletHit.collider.gameObject);
                    }

                }
            }
        }
    }
}