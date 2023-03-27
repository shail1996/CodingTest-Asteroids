using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    #region variables
    // Editable from Editor
    [SerializeField] private Rigidbody2D bulletRigidBody;
    [SerializeField] private float bulletSpeed = 15;

    // Private 
    #endregion

    #region UnityFunction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Asteroid")
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region publicFuntions
    public void Shoot(Vector2 direction)
    {
        bulletRigidBody.velocity = direction.normalized * bulletSpeed;
        StartCoroutine(DeleteGameObject());
    }
    #endregion

    #region privateFuntions
    IEnumerator DeleteGameObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
    #endregion


}
