using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        playerController.HorizontalMovement = horizontal == 1 || horizontal == -1 ? Input.GetAxisRaw("Horizontal") : 0;
        playerController.VerticlalMovement = vertical == 1 || vertical == -1 ? Input.GetAxisRaw("Vertical") : 0;

    }
}
