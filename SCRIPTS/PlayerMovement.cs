using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 6f;
    [SerializeField] Transform modeloVisual; // arrastrá BaseHuman acá
    Rigidbody rb;
    Camera camara;
    Animator animator; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camara = Camera.main;
        animator = GetComponent<Animator>(); // antes decía GetComponentInChildren
    }

    void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direccion = new Vector3(x, 0f, z).normalized;

        rb.linearVelocity = new Vector3(
            direccion.x * velocidad,
            rb.linearVelocity.y,
            direccion.z * velocidad
            
        );

        float speed = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;
        animator.SetFloat("velocidad", speed);

    }

    void Update()
    {
        Plane plano = new Plane(Vector3.up, transform.position);
        Ray rayo = camara.ScreenPointToRay(Input.mousePosition);

        if (plano.Raycast(rayo, out float distancia))
        {
            Vector3 puntoMouse = rayo.GetPoint(distancia);
            Vector3 direccionMouse = puntoMouse - transform.position;
            direccionMouse.y = 0f;

            

            if (direccionMouse.sqrMagnitude > 0.01f)
            {
                float angulo = Mathf.Atan2(direccionMouse.x, direccionMouse.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, angulo, 0f);
            }
        }
        else
        {
           
        }
    }
}