using System.Data;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float speed= 0.1f;
    private Vector2 offsetActual;
    private Material parallaxmaterial;
    //public Transform cam;
    //private float lastplayerY;

    private void Start()
    {
        parallaxmaterial = GetComponent<Renderer>().material;
        //cam = Camera.main.transform;
        //lastplayerY = cam.position.y;
    }

    public void Update()
    {
        Vector2 velocidad = new Vector2(0f, speed); // Sube verticalmente

        offsetActual += velocidad * Time.deltaTime;
        offsetActual.y = offsetActual.y % 1f;


        //float deltaY = cam.position.y - lastplayerY;
        parallaxmaterial.mainTextureOffset = new Vector2(0f, offsetActual.y);
        //lastplayerY = cam.position.y;
    }
}
