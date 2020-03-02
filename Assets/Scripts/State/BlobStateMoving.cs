using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob moves in a sine wave pattern.*/
public class BlobStateMoving : BlobState
{
    // Sine wave definitions.
    public const float speed = 0.01f;

    //IMPROVEMENT: changed the amplitude to be random to make the blobs feel more natural.
    public float amplitude = Random.Range(0.25f, 0.5f);

    public const float sinSpeed = 2.0f;
    public const float minTime = 1.0f;
    public const float maxTime = 5.0f;

    // Movement over time.
    private Vector3 curPos;
    private float elapsedTime;
    private float endTime;
    private Vector3 direction;

    public BlobStateMoving(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        // Regular movement
        curPos += Time.deltaTime * direction; 

        // Sine movement
        elapsedTime += Time.deltaTime;
        Vector3 offset = new Vector3(0.0f, amplitude * Mathf.Sin(elapsedTime * sinSpeed), 0.0f);
        blob.transform.position = curPos + offset;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStatePulsing(blob)); // Change to color pulsing state.
        }
    }

    //the warning was caused by the function not overriding the enter function from the base class
    // this caused the inheritance not to function properly as the enter class wasnt being
    // overriden and setting the new current position, endtime, and direction
    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.

        curPos = blob.transform.position;
        endTime = Random.Range(minTime, maxTime);
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)); // Move in random direction.
    }
}
