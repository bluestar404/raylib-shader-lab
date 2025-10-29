#version 330

in vec2 fragTexCoord;
in vec4 fragColor;
out vec4 finalColor;

uniform float time;

void main() {
    float wave = sin(fragTexCoord.y * 8.0 + time * 6.0) * 0.03;
    float pulse = 0.5 + 0.5 * sin(time * 3.0);
    vec2 uv = fragTexCoord + vec2(wave, 0.0);
    vec3 color = mix(vec3(0.8, 0.9, 1.0), vec3(1.0, 1.0, 1.0), pulse);
    finalColor = vec4(color, 1.0);
}
