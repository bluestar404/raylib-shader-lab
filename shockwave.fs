#version 330
in vec2 fragTexCoord;
out vec4 fragColor;
uniform sampler2D texture0;
uniform float time;

void main()
{
    vec4 col = texture(texture0, fragTexCoord);
    float ripple = sin(fragTexCoord.y * 30.0 + time * 5.0) * 0.3;
    vec2 uv = fragTexCoord + vec2(ripple,0);
    col += texture(texture0, uv) * 0.5; // streak
    fragColor = clamp(col, 0.0, 1.0);
}
