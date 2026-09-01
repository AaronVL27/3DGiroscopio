using UnityEngine;
using UnityEngine.InputSystem;

public class DissolveShaderController : MonoBehaviour
{
    public InputActionAsset actionAsset;

    InputAction interactAcction;


    private Material material;
    [SerializeField] private float dissolveSpeed;
    private bool inDissolving = false;
    private float dissolveCurrLevel;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        interactAcction = InputSystem.actions.FindAction("Interact");
    }

    private void OnEnable()
    {
        actionAsset.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        actionAsset.FindActionMap("Player").Disable();
    }
    private void Update()
    {
        if (interactAcction.WasPressedThisFrame())
        {
            inDissolving = true;
        }
        if (inDissolving)
        {
            dissolveCurrLevel += dissolveSpeed * Time.deltaTime;
            material.SetFloat("_DissolveLevel", dissolveCurrLevel);
            if (dissolveCurrLevel >= 1)
            {
                inDissolving = false;
                dissolveCurrLevel = 1;
            }
        }

    }
}
