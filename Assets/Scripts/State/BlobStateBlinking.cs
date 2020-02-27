using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    public GameController gc;

    private const float delayTime = 0.5f;
    private float currentTime;

    private float elapsedTime;
    private float endTime;

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= delayTime)
        {
            blob.enabled = false;
            currentTime = 0.0f;
        }
        else { blob.enabled = true; }


        elapsedTime += Time.deltaTime;
        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }

    }

    public override void Enter()
    {
        base.Enter(); //Call tbe base class
        endTime = Random.Range(0.1f, 0.5f);

        Blob blob;
    }

    public override void Leave()
    {
        base.Leave();
       // blob.controller._score += 1;
    }
}
