#version 330

uniform sampler2D texture0;
uniform float time;
in vec2 fragTexCoord;
out vec4 fragColor;

void main()
{
    vec4 color = texture(texture0, fragTexCoord);
    float r = sin(time + color.r * 6.28) * 0.5 + 0.5;
    float g = sin(time + color.g * 6.28 + 2.0) * 0.5 + 0.5;
    float b = sin(time + color.b * 6.28 + 4.0) * 0.5 + 0.5;
    fragColor = vec4(vec3(r,g,b), color.a);
}
