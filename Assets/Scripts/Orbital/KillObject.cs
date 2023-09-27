using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
    private bool once = false;
    public GameObject explode;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orbitals" && once == false)
        {
            //KILL
            once = true;
            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameObject explosion = Instantiate(explode, this.transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }
}
