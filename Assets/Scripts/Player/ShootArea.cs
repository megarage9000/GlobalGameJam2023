using UnityEngine;

public class ShootArea : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    private float offsetScaleFactor = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        float offsetX = (mousePos.x - Screen.width / 2);
        float offsetZ = (mousePos.y - Screen.height / 2);
        float dist = offsetX*offsetX + offsetZ*offsetZ;
        Vector3 hoverPos = transform.position + new Vector3(
            offsetScaleFactor * offsetX, 0f, offsetScaleFactor * offsetZ);
        RaycastHit hit;
        
        if (Physics.Raycast(hoverPos, Vector3.down, out hit))
        {
            crosshair.transform.position = hit.point;
        }
        
    }
}
