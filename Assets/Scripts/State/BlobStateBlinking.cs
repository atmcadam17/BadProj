using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobStatePulsing
{
    private bool isBkinking = false;
    private float blinkInterval;
    private float blink;
    private float totalBlinkTime;
    private MeshRenderer blobMR;
    
    public BlobStateBlinking(Blob theBlob) : base(theBlob)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        blinkInterval = .5f;
        blink = 0f;
        totalBlinkTime = 10f;
        blobMR = blob.GetComponent<MeshRenderer>();
    }

    public override void Run()
    {
        totalBlinkTime -= Time.deltaTime;
        if (totalBlinkTime > 0)
        {
            
            Blink();
            
        }
        else
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }

    }

    public void Blink()
    {

        blink += Time.deltaTime;


        if (blink > blinkInterval)
        {
            if (isBkinking) blobMR.enabled = false;
            else
            {
                
                blobMR.enabled = true;
            }

            isBkinking = !isBkinking;
        }
    }
    
    
    public override void Leave()
    {
        base.Leave();
        GameController controller = blob.GetComponentInParent<GameController>();
        controller.Score += 1;
    }
}

