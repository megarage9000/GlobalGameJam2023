using System.Collections;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    private const float offsetScaleFactor = 0.1f;
    private const float resizeScaleFactor = 1.5f;
    private const float resizeMinValue = 0.3f;
    private const float crosshairUpdateRate = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(raycastToMousePos(crosshairUpdateRate));
    }

    IEnumerator raycastToMousePos(float freq)
    {
        while (true)
        {
            Vector3 mousePos = Input.mousePosition;
            float offsetX = offsetScaleFactor * (mousePos.x - Screen.width / 2);
            float offsetZ = offsetScaleFactor * (mousePos.y - Screen.height / 2);
            Vector3 hoverPos = transform.position + new Vector3(offsetX, 0f, offsetZ);
            RaycastHit hit;
            
            if (Physics.Raycast(hoverPos, Vector3.down, out hit))
            {
                crosshair.transform.position = hit.point + Vector3.up;
                float dist = offsetX*offsetX + offsetZ*offsetZ;
                float scaleBy = resizeScaleFactor * Mathf.Log10(dist * resizeScaleFactor) + resizeMinValue;
                crosshair.transform.localScale = new Vector3(scaleBy, 1f, scaleBy);
            }

            yield return new WaitForSeconds(1f / freq);
        }
    }
}
