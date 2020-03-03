using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private Renderer renderer;
    private bool rendIsActive = true;
    private float timer = 0.0f;
    public int seconds { get { return (int)(timer % 60); } }//this is an iteration on the code shown below

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Enter()
    {
        base.Enter();
        renderer = blob.GetComponent<Renderer>();
        renderer.enabled = true;
        changeRend(true);
       // StartCoroutine("BlinkingHappening");

    }
   
    public override void Run()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            blob.ChangeState(new BlobStatePulsing(blob));
        }
        if(seconds >= 3)
        {
            if (rendIsActive == true)
            {
                changeRend(false);
                timer = 0;
            }
            else if (rendIsActive == false)
            {
                changeRend(true);
                timer = 0;
            }
        }
        timer += Time.deltaTime;
        //seconds = (int)(timer % 60); //found at https://answers.unity.com/questions/1038757/how-to-make-a-timer-that-counts-up-in-seconds-as-a.html
        //Debug.Log(seconds);
        //Debug.Log(timer);
        //Debug.Log(rendIsActive);
    }

    public void changeRend(bool rendIsOn)
    {
        renderer.enabled = rendIsOn;
        rendIsActive = rendIsOn;
    }

    public override void Leave()
    {
        gameController.Score += 1; //having problems with a null reference exception
        base.Leave();
    }

}
