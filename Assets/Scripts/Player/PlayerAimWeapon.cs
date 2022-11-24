using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAimWeapon : MonoBehaviour
{

    private Transform aimTransform;

    private void Awake()
    {
        //aimTransform = transform.Find("Aim");
    }

    // Start is called before the first frame update
    void Start()
    {
        aimTransform = transform.Find("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        if(InGameUIManager.instance.openMenu) return;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;


        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (angle < 90 && angle > -90)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            
            transform.Find("Aim").transform.GetComponentInChildren<SpriteRenderer>().flipY = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Find("Aim").transform.GetComponentInChildren<SpriteRenderer>().flipY = true;
        }
        
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if (angle  > 180 || angle < 0)
        {
            transform.Find("Aim").transform.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            transform.Find("Aim").transform.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        
    }
}
