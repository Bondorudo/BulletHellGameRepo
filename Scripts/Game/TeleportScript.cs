using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] private int code;
    [SerializeField] private ParticleSystem teleParticle;
    private float disableTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0) disableTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && disableTimer <= 0)
        {
            AudioManager.instance.PlaySound("Teleporter");
            teleParticle.Play();
            foreach (TeleportScript tp in FindObjectsOfType<TeleportScript>())
            {
                if (tp.code==code && tp != this)
                {
                    tp.teleParticle.Play();
                    tp.disableTimer = 1;
                    Vector3 position = tp.gameObject.transform.position;
                    position.y = 1;
                    other.gameObject.transform.position = position;
                }
            }
        }
    }
}
