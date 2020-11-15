using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementManager : MonoBehaviour
{

    public float movementSpeed;
    public float scrollSpeed;
    public float middleMouseRotationSpeed;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        float height = Input.GetAxis("Height") * Time.deltaTime * movementSpeed;
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            horizontal /=  5f;
            vertical /= 5f;
            height /= 5f;
        }
        Vector3 newPosition = new Vector3(horizontal, height, vertical);

        float rotY = transform.rotation.eulerAngles.y;
        newPosition = Quaternion.AngleAxis(rotY, Vector3.up) * newPosition;
        transform.position += newPosition;

        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 diff = (Input.mousePosition - lastMousePosition) * Time.deltaTime ;
            float movementSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? (middleMouseRotationSpeed / 5f): middleMouseRotationSpeed;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + (new Vector3(-diff.y, diff.x,0) * movementSpeed));
            lastMousePosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(!EventSystem.current.IsPointerOverGameObject() && scroll != 0)
        {
            transform.position += transform.forward * scrollSpeed * (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? (scroll / 5f) : scroll);
        }
    }
}
