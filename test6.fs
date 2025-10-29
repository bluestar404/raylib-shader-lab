#version 330

uniform sampler2D texture0;
uniform float time;
in vec2 fragTexCoord;
out vec4 fragColor;

void main()
{
    vec2 uv = fragTexCoord;
    uv.y += sin(uv.x * 20.0 + time * 5.0) * 0.02;
    uv.x += cos(uv.y * 15.0 + time * 3.0) * 0.02;
    fragColor = texture(texture0, uv);
}
