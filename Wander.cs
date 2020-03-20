using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isWandering == false)
        {
            StartCoroutine(StartWandering());
        }

        // Rotating right
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }

        // Rotating left
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }

        // Moving forward
        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator StartWandering()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);
        int attackOrJump = Random.Range(0, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        // Walking forward animation
        anim.SetInteger("condition", 1);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);

        // Idel animation
        anim.SetInteger("condition", 0);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1)
        {
            // Apply walking animation until it finishes rotating
            isRotatingRight = true;
            anim.SetInteger("condition", 1);
            yield return new WaitForSeconds(rotTime);
            anim.SetInteger("condition", 0);
            isRotatingRight = false;
        }

        if (rotateLorR == 2)
        {
            // Apply walking animation until it finishes rotating
            isRotatingLeft = true;
            anim.SetInteger("condition", 1);
            yield return new WaitForSeconds(rotTime);
            anim.SetInteger("condition", 0);
            isRotatingLeft = false;
        }

        isWandering = false;

        if (attackOrJump == 1)
        {
            anim.SetInteger("jump", 1);
            yield return new WaitForSeconds(2);
            anim.SetInteger("jump", 0);
        }

        if (attackOrJump == 2)
        {
            anim.SetInteger("attack", 1);
            yield return new WaitForSeconds(2);
            anim.SetInteger("attack", 0);
        }
    }
}
