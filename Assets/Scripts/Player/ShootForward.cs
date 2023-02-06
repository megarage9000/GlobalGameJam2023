using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForward : MonoBehaviour
{
    private const float offsetScaleFactor = 0.05f;
    private const float resizeScaleFactor = 1.5f;
    private const float resizeMinValue = 0.8f;

    public float ATTACK_DELAY = 0.1f;
    public int DAMAGE_AMOUT = 5;
    public float FIRE_RANGE = 10f;

    Transform playerTransform;

    public bool isFiring = false;
    const int MOUSE_PRIMARY = 0;

    private void Update()
    {
        if (!isFiring && Input.GetMouseButtonDown(MOUSE_PRIMARY))
        {
            isFiring = true;
            StartCoroutine(emitRay());
        }
        else if (Input.GetMouseButtonUp(MOUSE_PRIMARY))
        {
            isFiring = false;
            StopCoroutine(emitRay());
        }
    }

    IEnumerator emitRay()
    {
       Debug.Log("SHOOTING COROUTINE START");
       while(true)
       {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, FIRE_RANGE))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag.Equals("Enemy"))
                {
                    hitObject.GetComponent<HealthSystem>().TakeDamage(DAMAGE_AMOUT);
                }
            }

            yield return new WaitForSeconds(ATTACK_DELAY);
        }
    }
}
