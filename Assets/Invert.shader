Shader "Hidden/Invert"
{
    Properties
    {
        _MainTex("_MainTex", 2D) = "white"
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 positionHCS : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        TEXTURE2D(_MainTex);

        SAMPLER(sampler_MainTex);
        float4 _MainTex_ST;
        float _Invert;

        Varyings vert(Attributes IN)
        {
            Varyings OUT;
            OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
            OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
            return OUT;
        }
        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            half4 frag(Varyings IN) : SV_TARGET
            {
                half4 pixel = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float negate = (_Invert * -2.0 + 1.0);
                return _Invert + negate * pixel;
            }
            ENDHLSL
        }
    }
}
