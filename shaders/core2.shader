Shader "core 2" { // defines the name of the shader 
    Properties{
       //Properties that are changed by the core_script
        _Temp ("Temp",Float) = 0
        _Middle_temp ("Middle_Temp",Float) = 0
        _Temp_range("Temp_Range",float) = 0
        _Load ("Load",float) = 0

    }
   SubShader { // Unity chooses the subshader that fits the GPU best
      Pass { // some shaders require multiple passes
         CGPROGRAM // here begins the part in Unity's Cg

         #pragma vertex vert 
         #pragma fragment frag
         //Local variables for properties
         float _Temp;
         float _Middle_temp;
         float _Load;
         float _Temp_range;

         float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
            // vertex shader 
         {
            float4 output = vertexPos;
            output.y = output.y + 0.5; //scale vertex from y(-0,5 thru 0,5) to y(0 thru 1,0)
            output *= float4(1.0, _Load/100, 1.0, 1.0); //Multipy Y by load/100(to scale load from 0-100 to 0.0-1.0)
            //return value based on above calculations the object will grow from flat till cube
            return UnityObjectToClipPos(output);
         }

         float4 frag(void) : COLOR // fragment shader
         {
            //Calculate red and green values
            float red = 0;
            float green = 0;
            //if temperature above middle go from yellow-->red
            if (_Temp > _Middle_temp){
                green = 1.0 - ((_Temp-_Middle_temp)*((1.0/_Temp_range)*2));
                red = 1.0;
            }
            //if temperature below middle go from yellow-->green
            else{
                red = 1.0 + ((_Temp-_Middle_temp)*((1.0/_Temp_range)*2));
                green = 1.0;
            }
            return float4(red, green, 0.0, 1.0); //return color values blue(0) because we only use red and green
         }

         ENDCG // here ends the part in Cg 
      }
   }
}