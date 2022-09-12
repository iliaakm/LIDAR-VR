using UnityEngine;

public class GunPainter : MonoBehaviour
{
    [Space]
    public Color paintColor;

    [SerializeField] private float randomAngle = 30f;
    [SerializeField] private int scansPerFrame = 10;
    [SerializeField] private int horisonstalScans = 10;
    [SerializeField] private float horisontalAngle = 10;

    [SerializeField] private Transform muzzle = null;
    [SerializeField] private LineManager lineManager;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    void Update()
    {
        bool click = false;
        click = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        
        if (click)
        {
            SpotScan();
        }
        
        click = OVRInput.Get(OVRInput.RawButton.RHandTrigger);
        
        if (click)
        {
            RowScan();
        }
    }

    private void RowScan()
    {
        Ray forwardRay = new Ray(muzzle.position, muzzle.forward);
        Debug.DrawRay(forwardRay.origin, forwardRay.direction, Color.red);

        float step = horisontalAngle / horisonstalScans;
        
        for (int i = 0; i < horisonstalScans; i++)
        {
            var direction = forwardRay.direction;
            Quaternion spreadAngle = Quaternion.AngleAxis(-horisontalAngle/2f + step * i, new Vector3(0, 1, 0));
            direction = spreadAngle * direction;
            var ray = new Ray(forwardRay.origin, direction);
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            ScanRay(ray);
        }
    }

    private void SpotScan()
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
                lineManager.DrawLine(muzzle.position, hit.point);
                PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
            }
        }
    }
}