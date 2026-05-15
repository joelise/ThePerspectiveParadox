using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class LensManager : MonoBehaviour
{

    public static LensManager Instance;

    public Camera cam;

    public LayerMask RedMask;
    public LayerMask BlueMask;
    public LayerMask RedLensMask;
    public LayerMask BlueLensMask;
    //public GameObject RedLens;

    int playerLayer;
    int redLayer;
    int blueLayer;

    public Animator LensAnim;

    private bool redActive;
    private bool blueActive;
    public bool playing = false;

    MaterialPropertyBlock mpb;
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

    public void ToggleRedLens()
    {
        redActive = !redActive;
        ApplyRedLensState();
    }

    public void ApplyRedLensState()
    {
        if (redActive)
        {
            ToggleRedOn();
        }
        else
        {
            ToggleRedOff();
        }

        //LensAnim.SetBool("RedActive", redActive);

    }

    public void ToggleBlueLens()
    {
        blueActive = !blueActive;
        ApplyBlueLensState();
    }

    public void ApplyBlueLensState()
    {
        if (blueActive)
        {
            ToggleBlueOn();
        }
        else
        {
            ToggleBlueOff();
        }
    }


    
    public void ToggleRedOn()
    {
        playing = true;
        //LensAnim.SetBool("RedActive", true);
        cam.cullingMask += RedMask;
        //RedLens.SetActive(true);
        StartCoroutine(StartAnimation("RedLensOn"));
        //cam.cullingMask += RedLensMask;
        //StartCoroutine(PlayAnimation("RedLensOn", "RedLensOff", RedMask, RedLensMask, true));
        Physics.IgnoreLayerCollision(playerLayer, redLayer, false);
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
        
       
    }

    public void ToggleRedOff()
    {
        playing = true;
        //LensAnim.SetBool("RedActive", false);
        StartCoroutine(EndAnimation("RedLensOff", RedMask));
        //cam.cullingMask -= RedMask;

        //StartCoroutine(PlayAnimation("RedLensOn", "RedLensOff", RedMask, RedLensMask, false));
        //cam.cullingMask -= RedLensMask;

        Physics.IgnoreLayerCollision(playerLayer, redLayer, true);
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
    }

    public void ToggleBlueOn()
    {
        playing = true;
        cam.cullingMask += BlueMask;
        StartCoroutine(StartAnimation("BlueLensOn"));
        //StartCoroutine(PlayAnimation("BlueLensOn", "BlueLensOff", BlueMask, BlueLensMask, true));
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, false);
    }

    public void ToggleBlueOff()
    {
        playing = true;
        //cam.cullingMask -= BlueMask;
        //StartCoroutine(PlayAnimation("BlueLensOn", "BlueLensOff", BlueMask, BlueLensMask, false));
        StartCoroutine(EndAnimation("BlueLensOff", BlueMask));
        Physics.IgnoreLayerCollision(playerLayer, blueLayer, true);
    }

    public IEnumerator StartAnimation(string stateName)
    {
        LensAnim.Play(stateName, 0, 0f);
        yield return null;

        AnimatorStateInfo info = LensAnim.GetCurrentAnimatorStateInfo(0);
        float clipLength = info.length;

        yield return new WaitForSeconds(clipLength);
        playing = false;
    }

    public IEnumerator EndAnimation(string stateName, LayerMask mask)
    {
        LensAnim.Play(stateName, 0, 0f);
        yield return null;

        AnimatorStateInfo info = LensAnim.GetCurrentAnimatorStateInfo(0);
        float clipLength = info.length;

        yield return new WaitForSeconds(clipLength);
        playing = false;
        cam.cullingMask -= mask;
    }

    public IEnumerator PlayAnimation(string stateNameOn, string stateNameOff, LayerMask mask, LayerMask lensMask, bool active)
    {
        if (active)
        {
            
            LensAnim.Play(stateNameOn, 0, 0f);
            
            
            yield return null;
           
            AnimatorStateInfo info = LensAnim.GetCurrentAnimatorStateInfo(0);
            float clipLength = info.length;
            cam.cullingMask += mask;
            
            yield return new WaitForSeconds(clipLength);
        }

        if (!active)
        {
            LensAnim.Play(stateNameOff, 0, 0f);
            
            yield return new WaitForSeconds(0.25f);
            
            AnimatorStateInfo info = LensAnim.GetCurrentAnimatorStateInfo(0);
            float clipLength = info.length;

            yield return new WaitForSeconds(clipLength);

            cam.cullingMask -= mask;
            //yield return new WaitForSeconds(2f);
            //cam.cullingMask -= lensMask;

        }
    }




}
