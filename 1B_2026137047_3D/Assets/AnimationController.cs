using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;


public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public ExampleCharacterController character;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", character.Motor.Velocity.magnitude / 10f);
    }
}