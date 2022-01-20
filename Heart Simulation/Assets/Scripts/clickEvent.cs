using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickEvent : MonoBehaviour
{
    Color idle = Color.white;
    Color selected = Color.red;
    public UnityEvent clicked;
    public UnityEvent entered;
    public UnityEvent exited;
    public Renderer renderer;
    public bool testCube = false;
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
        entered.Invoke();
        if (testCube) { renderer.material.SetColor("_Color", selected); }
    }

    public void OnMouseExit()
    {
        exited.Invoke();
        if (testCube) { renderer.material.SetColor("_Color", idle); }
    }
}
