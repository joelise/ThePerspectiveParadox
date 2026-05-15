using UnityEngine;

public class LayerFaderManager : MonoBehaviour
{
    public static LayerFaderManager Instance;

    public string layerName = "Red";
    public float fadeSpeed = 2f;

    float targetFade = 1f;
    float currentFade = 1f;

    MaterialPropertyBlock mpb;
    Renderer[] renderers;

    private void Awake()
    {
        mpb = new MaterialPropertyBlock();
        int layer = LayerMask.NameToLayer(layerName);

        var allRenderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        renderers = System.Array.FindAll(allRenderers, r => r.gameObject.layer == layer);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentFade = Mathf.Lerp(currentFade, targetFade, Time.deltaTime * fadeSpeed);

        foreach (var r in renderers)
        {
            r.GetPropertyBlock(mpb);
            mpb.SetFloat("LayerFade", currentFade);
            r.SetPropertyBlock(mpb);
        }
    }

    public void FadeIn() => targetFade = 1f;
    public void FadeOut() => targetFade = 0f;
}
