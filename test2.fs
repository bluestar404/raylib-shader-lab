
// //glowing center effect
// #version 330
// in vec2 fragTexCoord;
// out vec4 fragColor;
// uniform sampler2D texture0;
// uniform float time;

// void main()
// {
//     vec2 center = vec2(0.5,0.5);
//     vec4 col = texture(texture0, fragTexCoord);
//     float dist = distance(fragTexCoord, center);
//     float alpha = smoothstep(0.5, 0.0, dist); // halo
//     col.rgb += vec3(0.0,1.0,1.0) * alpha;     // cyan glow
//     fragColor = clamp(col, 0.0, 1.0);
// }





// snow_fall_effect
// #version 330
// in vec2 fragTexCoord;
// out vec4 fragColor;
// uniform sampler2D texture0;
// uniform float time;

// float rand(vec2 co){ return fract(sin(dot(co.xy,vec2(12.9898,78.233)))*43758.5453); }

// void main()
// {
//     vec4 col = texture(texture0, fragTexCoord);
//     float lightning = step(0.97, rand(fragTexCoord*100.0 + time*10.0));
//     col.rgb += vec3(0.5,0.9,1.0) * lightning; // cyan sparks
//     fragColor = clamp(col,0.0,1.0);
// }



#version 330
in vec2 fragTexCoord;
out vec4 fragColor;
uniform sampler2D texture0;
uniform float time;

float rand(vec2 co){ return fract(sin(dot(co.xy,vec2(12.9898,78.233)))*43758.5453); }

void main()
{
    vec4 col = texture(texture0, fragTexCoord);
    float r = rand(fragTexCoord*100.0 + time*5.0);
    if(r > 0.9) col.rgb += vec3(1.0,0.8,0.2); // yellow spark
    fragColor = clamp(col,0.0,1.0);
}
