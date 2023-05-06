using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private int powerupID;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            
            if (powerupID == 0)
            {
                player.TripleShotActive();
            }
            else if(powerupID == 1)
            {
                player.SpeedActive();
            }
            else if (powerupID == 2)
            {
                Debug.Log("e 2");
            }

            Destroy(this.gameObject);
        }
    }
}
