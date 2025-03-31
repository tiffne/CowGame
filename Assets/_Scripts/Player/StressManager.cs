using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StressManager : MonoBehaviour
{
    public float currentStress = 0f;
    public float stressRecoveryRate = 0.2f;
    public float maxVignetteIntensity = 0.55f;
    public float vignetteSmoothness = 3f;

    public Volume volume;
    private Vignette vignette;
    private float targetVignette;

    private void Start()
    {
        if (volume != null)
        {
            volume.profile.TryGet(out vignette);
            
            // Initialize vignette settings
            if (vignette != null)
            {
                vignette.intensity.value = 0;
                vignette.color.value = Color.black;
                vignette.smoothness.value = 0.5f;
            }
        }
    }

    private void Update()
    {
        // UpdateStress(); // See my comment about this at the function
        UpdateVignette();
        
        // Just for testing purposes: Press T to increase vignette and Y to decrease vignette
        if (Input.GetKey(KeyCode.T)) AddStress(0.01f);
        if (Input.GetKey(KeyCode.Y)) ReduceStress(0.01f);
    }

    public void AddStress(float amount)
    {
        currentStress += amount;
    }

    public void ReduceStress(float amount)
    {
        currentStress -= amount;
    }

    private void UpdateStress()
    {
        // Idk if we want it but this automatically decays vignette over time
        if (currentStress > 0)
        {
            currentStress -= stressRecoveryRate * Time.deltaTime;
        }
    }

    private void UpdateVignette()
    {
        if (vignette == null) return;
        
        targetVignette = currentStress * maxVignetteIntensity;
        
        vignette.intensity.value = Mathf.Lerp(
            vignette.intensity.value,
            targetVignette,
            vignetteSmoothness * Time.deltaTime
        );
    }
}