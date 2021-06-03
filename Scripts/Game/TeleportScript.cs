using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] private int code;
    [SerializeField] private ParticleSystem teleParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !teleParticle.isPlaying)
        {
            AudioManager.instance.PlaySound("Teleporter");
            teleParticle.Play();
            foreach (TeleportScript tp in FindObjectsOfType<TeleportScript>())
            {
                if (tp.code==code && tp != this)
                {
                    tp.teleParticle.Play();
                    Vector3 position = tp.gameObject.transform.position;
                    position.y = 1;
                    other.gameObject.transform.position = position;
                }
            }
        }
    }
}
