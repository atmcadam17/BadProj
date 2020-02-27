using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This state class is derived from BlobState
 * it implments a state where the Blob blinks on and off every half second
 */
public class BlobStateBlinking : BlobState
{
    bool blinked = false;
    MeshRenderer mR;
    float blinkTimer = 0;
    float timerMax = .5f;

    // Time until state change *taken from the other state classes*
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {
        
    }

    public override void Run()
    {
        //blink on and off
        blinkTimer += Time.deltaTime;

        if(blinkTimer > timerMax)
        {
            blinkTimer = 0;
            blink();
        }

        // Return to Moving state after time has elapsed.
        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }

    public override void Enter() // Overriden from base class.
    {
        mR = blob.GetComponent<MeshRenderer>();
        if (!mR) throw new System.Exception("No Mesh Renderer found");
        endTime = Random.Range(minTime, maxTime);
    }

    public override void Leave()
    {
        base.Leave();

        GameController gc = blob.GetComponentInParent<GameController>();
        gc.Score += 1;
       
    }

    //turns the blob mesh renderer on or off when called 
    public void blink() 
    {
        if (blinked) mR.enabled = false;
        else { mR.enabled = true; }
        blinked = !blinked;
    }

    

}
