using System.Collections;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    private const float offsetScaleFactor = 0.05f;
    private const float resizeScaleFactor = 1.5f;
    private const float resizeMinValue = 0.8f;
    private const float crosshairUpdateRate = 3.0f;

    public float ATTACK_DELAY = 0.1f;
    public int DAMAGE_AMOUT = 5;

    Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(raycastToMousePos(crosshairUpdateRate));
    }

    IEnumerator raycastToMousePos(float freq)
    {
        while (true)
        {
            // cast the screen position to world position relative to the player
            Vector3 mousePos = Input.mousePosition;
            float offsetX = offsetScaleFactor * (mousePos.x - Screen.width / 2);
            float offsetZ = offsetScaleFactor * (mousePos.y - Screen.height / 2);
            Vector3 hoverPos = playerTransform.position + new Vector3(offsetX, 0f, offsetZ);
            RaycastHit hit;
            
            if (Physics.Raycast(hoverPos, Vector3.down, out hit))
            {
                transform.position = hit.point + Vector3.up;
                float dist = offsetX*offsetX + offsetZ*offsetZ;
                float scaleBy = resizeScaleFactor * Mathf.Log10(dist * resizeScaleFactor) + resizeMinValue;
                transform.localScale = new Vector3(scaleBy, 1f, scaleBy);
            }

            yield return new WaitForSeconds(1f / freq);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(DAMAGE_AMOUT);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(DAMAGE_AMOUT);
        }
    }
}
