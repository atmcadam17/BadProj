using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    // Cached
    private Renderer renderer;

    // Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;

    //Time until blinking
    private float delayTimer = 0.5f;
    private const float delayTime = 0.5f;

    bool blinkVisible = true;




    public override void Run()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        if (delayTimer <= 0)
        {
            if (!blinkVisible)
            {
                renderer.enabled = true;
                blinkVisible = true;
            } else {
                renderer.enabled = false;
                blinkVisible = false;
            }
            delayTimer = delayTime;
        }

        // Return to Moving state after time has elapsed.
        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            renderer.enabled = true;
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }

    public override void Enter()
    {
        base.Enter();

        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.

        renderer.enabled = false;
        blinkVisible = false;
        delayTimer = delayTime;

        endTime = Random.Range(minTime, maxTime);

    }

    public override void Leave()
    {

    }

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }
}
