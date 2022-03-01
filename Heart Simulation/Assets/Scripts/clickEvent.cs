using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickEvent : MonoBehaviour
{
    public UnityEvent clicked;
    public UnityEvent entered;
    public UnityEvent exited;
    public UnityEvent clickConfirmed;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseDown()
    {
        clicked.Invoke();
    }

    public void OnMouseEnter()
    {
        anim.SetInteger("speed", 1);
        entered.Invoke();
    }

    public void OnMouseExit()
    {
        anim.SetInteger("speed", -1);
        exited.Invoke();
    }

    public void confirmClick()
    {
        clickConfirmed.Invoke();
    }
}
