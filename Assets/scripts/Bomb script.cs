using System;
using UnityEngine;

public class Bombscript : MonoBehaviour
{
    [SerializeField] int gridOffset;
    [SerializeField] int spawnHeight;
    [Header("Ray")]
    [SerializeField] float sphereCastRad;
    [SerializeField] LayerMask mask;
    [Header("stats")]
    [SerializeField] int range;
    [SerializeField] float timer;
    float spawnTime;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.z);

        int divMain = (int)MathF.Floor(Mathf.Abs(spawnPos.x / gridOffset));
        float module = spawnPos.x % gridOffset;
        if (Mathf.Abs(module) > gridOffset / 2) divMain++;
        spawnPos.x = divMain * gridOffset;

        divMain = (int)MathF.Floor(Mathf.Abs(spawnPos.y / gridOffset));
        module = spawnPos.y % gridOffset;
        if (Mathf.Abs(module) > gridOffset / 2) divMain++;
        spawnPos.y = divMain * -gridOffset;

        transform.position = new Vector3(spawnPos.x, spawnHeight, spawnPos.y);

    }
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime >= timer)
        {
            
            animator.SetTrigger("Explode");
            spawnTime = Time.time; 
        }
    }
   void ExplodeInDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
       
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRad, range, mask);
        Array.Sort(hits,(a, b) => a.distance.CompareTo(b.distance));
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

   public void Explode()
    {
        ExplodeInDirection(Vector3.right);
        ExplodeInDirection(Vector3.left);
        ExplodeInDirection(Vector3.forward);
        ExplodeInDirection(Vector3.back);
            }
    public void DisableObject()
    { 
        gameObject.SetActive(false);
    }

    public void SetBombRange(int range)
    {
        this.range = range;
    }
}
