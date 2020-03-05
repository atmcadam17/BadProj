using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob blinks on and off.*/
public class BlobStateBlinking : BlobState
{
    private Renderer renderer; // Cache renderer.

    private float elapsedBlinkTime; // How long until next blink.
    private int curBlinks; // How many times the blob has blinked so far.

    private const int maxBlinks = 4; // How many times to blink.
    private const float maxBlinkTime = 0.5f; // How long between blinks.

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        elapsedBlinkTime -= Time.deltaTime;

        if (elapsedBlinkTime < 0.0f)
        {
            elapsedBlinkTime = maxBlinkTime;
            curBlinks++;
            renderer.enabled = !renderer.enabled; // Blink renderer on and off
        }

        if (curBlinks >= maxBlinks)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }

    public override void Enter() // Overriden from base class.
    {
        renderer = blob.GetComponent<Renderer>();
    }

    public override void Leave() // Overriden from base class.
    {
        blob.IncreaseScore(1);
    }

}
