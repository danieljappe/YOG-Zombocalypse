using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    public int bulletDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        // Lock rotation in the y-axis
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
