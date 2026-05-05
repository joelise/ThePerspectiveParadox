using UnityEngine;

public class LensManager : MonoBehaviour
{

    public static LensManager Instance;

    public Camera cam;

    public LayerMask RedMask;
    public LayerMask BlueMask;

    //public GameObject RedLens;

    int playerLayer;
    int redLayer;
    int blueLayer;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        redLayer = LayerMask.NameToLayer("Red");
        blueLayer = LayerMask.NameToLayer("Blue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRedOn()
    {
        
        cam.cullingMask += RedMask;
        //RedLens.SetActive(true);

        Physics.IgnoreLayerCollision(playerLayer, redLayer, false);
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
        
       
    }

    public void ToggleRedOff()
    {
        cam.cullingMask -= RedMask;
        Physics.IgnoreLayerCollision(playerLayer, redLayer, true);
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
    }

    public void ToggleBlueOn()
    {
        cam.cullingMask += BlueMask;

        Physics.IgnoreLayerCollision(playerLayer, blueLayer, false);
    }

    public void ToggleBlueOff()
    {
        cam.cullingMask -= BlueMask;

        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
    }

    
}
