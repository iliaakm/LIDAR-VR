using UnityEngine;

public class GunPainter : MonoBehaviour
{
    [Space]
    public Color paintColor;

    [SerializeField] private float randomAngle = 30f;
    [SerializeField] private int scansPerFrame = 10;

    [SerializeField] private Transform muzzle = null;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    void Update()
    {
        bool click = false;
        click = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        
        if (click)
        {
            Ray forwardRay = new Ray(muzzle.position, muzzle.forward);
            Debug.DrawRay(forwardRay.origin, forwardRay.direction, Color.red);
            
            for (int i = 0; i < scansPerFrame; i++)
            {
                var direction = forwardRay.direction;
                var offset = Random.insideUnitCircle * randomAngle;
                direction.x += offset.x;
                direction.y += offset.y;
                var ray = new Ray(forwardRay.origin, direction);
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
                ScanRay(ray);
            }
        }
    }

    private void ScanRay(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
            transform.position = hit.point;
            Paintable p = hit.collider.GetComponent<Paintable>();
            if (p != null)
            {
                PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
            }
        }
    }
}