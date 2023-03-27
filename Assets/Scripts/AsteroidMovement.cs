using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    #region variables
    // Editable from Editor
    [SerializeField] private Sprite _sprite;
    [SerializeField] private SpriteRenderer _spriteRendered;
    [SerializeField] private PolygonCollider2D colliderAsteroid;
    private UIManager _ui;

    // Private 
    private Rigidbody2D rigidbodyAsteroid;
    //private float speed = 2f;
    #endregion
    private void Awake()
    {
        rigidbodyAsteroid = GetComponent<Rigidbody2D>();
        _ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    public void kick(float theMass, Vector2 direction)
    {
        _spriteRendered.sprite = _sprite;

        List<Vector2> path = new List<Vector2>();
        _spriteRendered.sprite.GetPhysicsShape(0, path);
        colliderAsteroid.SetPath(0, path.ToArray());

        rigidbodyAsteroid.mass = theMass;
        float width = Random.Range(0.75f, 1.33f);
        float height = 1 / width;
        transform.localScale = new Vector2(width, height) * theMass;

        rigidbodyAsteroid.velocity = direction.normalized * GlobalVariables.asteroidSpeedGlobal;
        rigidbodyAsteroid.AddTorque(Random.Range(-4f, 4f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            // Score
            if (rigidbodyAsteroid.mass > 1.5f)
                GlobalVariables.score += 20;
            else if (rigidbodyAsteroid.mass == 1.0f)
                GlobalVariables.score += 50;
            else if (rigidbodyAsteroid.mass == 0.5f)
                GlobalVariables.score += 100;
            _ui.UpdateScore();

            // Update Speed
            if (GlobalVariables.score > 1000 && GlobalVariables.score < 2000)
            {
                GlobalVariables.asteroidSpeedGlobal = 3.0f;
                GlobalVariables.asteroidFrequencyGlobal = 1.5f;
            }
            else if (GlobalVariables.score > 2000 && GlobalVariables.score < 3000)
            {
                GlobalVariables.asteroidSpeedGlobal = 4.0f;
                GlobalVariables.asteroidFrequencyGlobal = 1.0f;
            }
            else if (GlobalVariables.score > 3000)
            {
                GlobalVariables.asteroidSpeedGlobal = 5.0f;
                GlobalVariables.asteroidFrequencyGlobal = 0.5f;
            }


            if (rigidbodyAsteroid.mass > 0.5f)
            {
                split();
                split();
            }
            Destroy(this.gameObject);
        }
        if (collision.tag == "Ship")
        {
            if(GlobalVariables.life > 0)
            {
                GlobalVariables.life -= 1;
                _ui.UpdateLifeCount();
            }
        }
    }

    void split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;


        AsteroidMovement small = Instantiate(this, position, this.transform.rotation);
        Vector2 direction = Random.insideUnitCircle;
        // 1.5, 1, 0.8;
        float mass;
        if (rigidbodyAsteroid.mass == 1.5f)
            mass = 1.0f;
        else
            mass = 0.5f;
        small.kick(mass, direction);
    }

}
