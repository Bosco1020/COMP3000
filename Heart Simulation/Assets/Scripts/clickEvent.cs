using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickEvent : MonoBehaviour
{
    Color idle = Color.white;
    Color selected = Color.red;
    public UnityEvent clicked;
    public Renderer renderer;
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
        renderer.material.SetColor("_Color", selected);
    }

    public void OnMouseExit()
    {
        renderer.material.SetColor("_Color", idle);
    }
}
