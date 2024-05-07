using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
private void Update() {
    if(Input.GetAxisRaw("Jump")>0)
    SceneManager.LoadScene(2);
}
}
