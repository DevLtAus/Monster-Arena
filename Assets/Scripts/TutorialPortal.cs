using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPortal : MonoBehaviour
{
    public string targetScene;
    public float spinSpeed = 1;
    private Transform trans;
    public Vector3 axis;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            SceneManager.LoadScene(targetScene);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        trans.Rotate(axis, spinSpeed);
    }
}
