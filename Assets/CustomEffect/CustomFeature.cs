using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomFeature : ScriptableRendererFeature
{
    CustomPass pass;

    [System.Serializable]
    public class FeatureSettings
    {
        public bool invert = true;
    }

    public FeatureSettings settings = new FeatureSettings();
        
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    public override void Create()
    {
        pass = new CustomPass(settings);
    }
}
