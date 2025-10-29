// //tv_noise_effect
// #version 330
// in vec2 fragTexCoord;
// out vec4 fragColor;
// uniform sampler2D texture0;
// uniform float time;

// void main()
// {
//     vec4 col = texture(texture0, fragTexCoord);
//     float spark = fract(sin(dot(fragTexCoord.xy*100.0, vec2(12.9898,78.233))) * 43758.5453 + time*10.0);
//     spark = step(0.95, spark);
//     col += vec4(1.0, 1.0, 0.5, 1.0) * spark; // yellow spark
//     fragColor = clamp(col,0.0,1.0);
// }



// cool_plasma_effect
// #version 330
// in vec2 fragTexCoord;
// out vec4 fragColor;
// uniform sampler2D texture0;
// uniform float time;

// void main()
// {
//     vec4 col = texture(texture0, fragTexCoord);
//     vec2 uv = fragTexCoord * 10.0; // frequency
//     float n = sin(uv.x + time) + cos(uv.y + time * 1.5);
//     vec3 plasma = vec3(0.5 + 0.5*sin(n*3.14), 0.5 + 0.5*sin(n*1.7), 1.0);
//     fragColor = vec4(col.rgb + plasma * 0.5, col.a);
// }




#version 330
in vec2 fragTexCoord;
out vec4 fragColor;
uniform sampler2D texture0;
uniform float time;

void main()
{
    vec4 col = texture(texture0, fragTexCoord);
    vec2 center = vec2(0.5,0.5);
    float dist = distance(fragTexCoord, center);
    float pulse = 0.3 + 0.7*sin(time*5.0 - dist*10.0);
    col.rgb += vec3(0.0,1.0,1.0) * pulse; // cyan aura
    fragColor = clamp(col,0.0,1.0);
}



//warp_shader_wiggle

#version 330
in vec2 fragTexCoord;
out vec4 fragColor;

uniform sampler2D texture0;
uniform float time;

void main() {
    vec2 uv = fragTexCoord;
    uv.y += sin(uv.x*20.0 + time*10.0) * 0.05;
    uv.x += cos(uv.y*20.0 + time*12.0) * 0.03;
    vec4 col = texture(texture0, uv);
    fragColor = col;
}




//grass_movement_shader
#version 330
in vec2 fragTexCoord;
out vec4 fragColor;

uniform sampler2D texture0;
uniform float time;

void main() {
    vec2 uv = fragTexCoord;

    // Simulate waving effect along x-axis
    float wave = sin(uv.y*20.0 + time*3.0) * 0.02;
    uv.x += wave;

    vec4 col = texture(texture0, uv);
    // Greenish grass tint
    col.rgb *= vec3(0.2, 1.0, 0.2);
    fragColor = col;
}


