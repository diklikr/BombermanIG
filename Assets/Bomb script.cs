using UnityEngine;

public class Bombscript : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask mask;
    [Header("stats")]
    [SerializeField] int range;
    [SerializeField] float timer;
    float spawnTime;

    private void OnEnable()
    {
        spawnTime = Time.time;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime >= timer)
        {
            Explode();
            gameObject.SetActive(false);
        }
    }
    void Explode()
    {
        Ray ray = new Ray(transform.position, Vector3.right);
       
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRad, range, mask);
       
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                
                if (hit.transform.tag == "unbreakable") break;
                hit.transform.gameObject.SetActive(false);
                if(hit.transform.tag == "breakable")
                {
                    break;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, sphereCastRad);

    }
}
