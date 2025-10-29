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

