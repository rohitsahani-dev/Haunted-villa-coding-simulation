using UnityEngine;
using UnityEngine.InputSystem;
public class MouseClicker : MonoBehaviour
{
// This code is based on the Unity example found here:
// https://learn.unity.com/tutorial/onmousedown#63566bf3edbc2a0285856b5a
[SerializeField]
private Camera m_Camera;
private bool mousePress = false;
void Awake()
{
m_Camera = Camera.main;
}
// Start is called before the first frame update
void Start()
{
}
// Update is called once per frame
void Update()
{
Mouse mouse = Mouse.current;
if (mouse.leftButton.wasPressedThisFrame)
{
mousePress = true;
}
}
// Update is called once per frame
void FixedUpdate()
{
Mouse mouse = Mouse.current;
if (mousePress)
{
mousePress = false;
Vector3 mousePosition = mouse.position.ReadValue();
Ray ray = m_Camera.ScreenPointToRay(mousePosition);
if (Physics.Raycast(ray, out RaycastHit hit))
{
Debug.Log("Clicked on: " + hit.collider.gameObject.name);
}
}
}
}