using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy blob on leaving arena
    void OnTriggerEnter(UnityEngine.Collider other)
    {
        Blob collideBlob = other.gameObject.GetComponent<Blob>();

        if (collideBlob != null)
        {
            //Reverses direction of blob if running into a wall with enough Safe Bounces remaining
            if (collideBlob.SafeBouncesRemaining > 0)
            {
                if (collideBlob.MovingState != null)
                {
                    collideBlob.SafeBouncesRemaining--;
                    collideBlob.MovingState.ReverseDirection();
                    print("Boing");
                } else {
                    print("Not in moving state");
                }
            }
            else
            {
                collideBlob.Kill();
            }
        }
    }
}
