using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    #region variables
    // Editable from Editor
    [SerializeField] private Rigidbody2D shipRigidBody;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float torqueSpeed;
    [SerializeField] private FireBullet bullet;

    // Private 
    private bool moveUp = false;
    private float torqueDirection = 0.0f;
    private bool fireAgain = true;
    #endregion

    #region start/update/physics
    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            torqueDirection = -1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            torqueDirection = 1;
        }
        else
        {
            torqueDirection = 0;
        }

        RepositionInScren();

        // Fire
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireAgain)
            {
                fireAgain = false;
                FireBullet _fireBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                _fireBullet.Shoot(transform.up);
                StartCoroutine(DelayEachFrame());
            }
        }
    }

    private void FixedUpdate()
    {
        if (moveUp)
        {
            shipRigidBody.AddForce(transform.up * forwardSpeed);
        }

        if(torqueDirection != 0)
        {
            shipRigidBody.AddTorque(torqueDirection * torqueSpeed);
        }
    }

    #endregion

    #region privateFuntions
    private void RepositionInScren()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x > Screen.width / 100)
        {
            x -= Screen.width / 50;
        }
        if (x < -Screen.width / 100)
        {
            x += Screen.width / 50;
        }
        if (y > Screen.height / 100)
        {
            y -= Screen.height / 50;
        }
        if (y < -Screen.height / 100)
        {
            y += Screen.height / 50;
        }

        transform.position = new Vector2(x, y);
    }

    IEnumerator DelayEachFrame()
    {
        yield return new WaitForSeconds(0.2f);
        fireAgain = true;
    }
    #endregion
}
