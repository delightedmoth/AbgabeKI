using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControlller : MonoBehaviour
{
    private Vector2 move;

    public int speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(-1, Input.GetAxisRaw("Vertical"));

        transform.Translate(move * speed * Time.deltaTime);
    }
}
