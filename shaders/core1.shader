Shader "core 1" { // defines the name of the shader 
    Properties{
        _Temp ("Temp",Float) = 0
        _Middle_temp ("Middle_Temp",Float) = 0
        _Temp_range("Temp_Range",float) = 0
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
         float _Temp_range;
            // this specifies the frag function as the fragment shader

         float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
            // vertex shader 
         {
            float4 output = vertexPos;
            output.y = output.y + 0.5;
            output *= float4(1.0, _Load/100, 1.0, 1.0);
            return UnityObjectToClipPos(output);
         }

         float4 frag(void) : COLOR // fragment shader
         {
            float red = 0;
            float green = 0;
            if (_Temp > _Middle_temp){
                green = 1.0 - ((_Temp-_Middle_temp)*((1.0/_Temp_range)*2));
                red = 1.0;
            }
            else{
                red = 1.0 + ((_Temp-_Middle_temp)*((1.0/_Temp_range)*2));
                green = 1.0;
            }
            return float4(red, green, 0.0, 1.0); 
         }

         ENDCG // here ends the part in Cg 
      }
   }
}