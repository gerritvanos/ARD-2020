Shader "Cg basic shader" { // defines the name of the shader 
    Properties{
        _Temp ("Temp",Float) = 0
        _Middle_temp ("Middle",Float) = 0
        _Load ("Load",float) = 0

    }
   SubShader { // Unity chooses the subshader that fits the GPU best
      Pass { // some shaders require multiple passes
         CGPROGRAM // here begins the part in Unity's Cg

         #pragma vertex vert 
            // this specifies the vert function as the vertex shader 
         #pragma fragment frag
         float _Temp;
         float _Middle_temp;
         float _Load;
            // this specifies the frag function as the fragment shader

         float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
            // vertex shader 
         {
           return UnityObjectToClipPos(float4(2.0, 2.0, 2.0, 1.0) * vertexPos);
              // this line transforms the vertex input parameter 
              // and returns it as a nameless vertex output parameter 
              // (with semantic SV_POSITION)
         }

         float4 frag(void) : COLOR // fragment shader
         {
           return float4(1.0,1.0,1.0,1.0);
         }

         ENDCG // here ends the part in Cg 
      }
   }
}