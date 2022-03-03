using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite mainIcon;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 CursorPos = Input.mousePosition;
        //disatnce from the camera
        CursorPos.z = 100.0f;

        CursorPos = Camera.main.ScreenToWorldPoint(CursorPos);
        transform.position = CursorPos;
    }

    public void updateIcon(Sprite icon)
    {
        rend.sprite = icon;
    }

    public void resetIcon()
    {
        rend.sprite = mainIcon;
    }
}
