using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class URPCustomFeature : ScriptableRendererFeature
{

   
    RenderTargetHandle renderTextureHandle;



    private class URPCustomRenderFeature : ScriptableRenderPass
    {
        private RenderTargetHandle tempTexture;
        private Material materialToBlit;
        private string profilerTag;
        private RenderTargetIdentifier cameraColorTargetIdent;

        public URPCustomRenderFeature(RenderPassEvent renderPass, Material material, string tag)
        {
            materialToBlit = material;
            profilerTag = tag;
            this.renderPassEvent = renderPass;

        }


        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
          
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor,FilterMode.Point);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            cameraColorTargetIdent = renderingData.cameraData.renderer.cameraColorTarget;
        }

        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();
            cmd.Blit(cameraColorTargetIdent, tempTexture.Identifier(), materialToBlit, 0);
            cmd.Blit(tempTexture.Identifier(), cameraColorTargetIdent);
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }
    }


    [System.Serializable]
    public class Settings
    {
       
        public bool IsEnabled = true;
        public RenderPassEvent WhenToInsert = RenderPassEvent.AfterRendering;
        public Material MaterialToBlit;
    }

    URPCustomRenderFeature m_ScriptablePass;
    [SerializeField] private RenderPassEvent renderPassEvent;
    public Settings settings = new Settings();

    public override void Create()
    {
        m_ScriptablePass = new URPCustomRenderFeature(renderPassEvent,settings.MaterialToBlit,"CustomFeature");

     
      
    }

   
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
       // var cameraColorTargetIdent = renderer.cameraColorTarget;
      //  m_ScriptablePass.OnCameraSetup(cameraColorTargetIdent);

        renderer.EnqueuePass(m_ScriptablePass);
    }
}


