using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPass : ScriptableRenderPass
{
    Material material;

    public CustomPass(CustomFeature.FeatureSettings settings)
    {
        if (material == null)
        {
            material = CoreUtils.CreateEngineMaterial("Hidden/Invert");
        }
        material.SetFloat("_Invert", settings.invert? 1.0f : 0.0f);
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get();
        RenderTargetIdentifier colour = renderingData.cameraData.renderer.cameraColorTarget;
        commandBuffer.Blit(colour, colour, material);
        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }
}
