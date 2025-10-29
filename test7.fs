#version 330

in vec2 fragTexCoord;
out vec4 fragColor;

uniform sampler2D texture0;
uniform float time;

void main()
{
    vec4 tex = texture(texture0, fragTexCoord);

    // Discard fully transparent pixels (background)
    if (tex.a < 0.1) discard;

    vec2 uv = fragTexCoord;

    // ---- Thruster trail (right side of UV now, since jets move left -> right) ----
    float trailStart = 0.9;
    float dist = (uv.x - trailStart);

    // Fire trail fades outward from thruster
    float glow = smoothstep(0.0, 0.3, dist) * (1.0 - dist * 2.0);
    glow = clamp(glow, 0.0, 1.0);

    // Animated noise/flicker effect
    float flicker = sin(time * 30.0 + uv.y * 25.0) * 0.2 + 0.8;

    // Flame waves (make it look alive)
    float wave = sin((uv.y + time * 3.0) * 12.0 + uv.x * 20.0);
    glow *= 0.8 + 0.2 * wave;

    // Final fire color: mix orange -> yellow -> white
    vec3 fireColor = mix(vec3(1.0, 0.3, 0.0), vec3(1.0, 0.9, 0.3), glow);
    fireColor *= flicker;

    // Add blue tint near thruster
    fireColor += vec3(0.1, 0.2, 1.0) * smoothstep(0.95, 0.85, uv.x);

    // Mix with original jet (black shape)
    vec3 finalColor = mix(vec3(0.0), fireColor, glow * 1.5);

    // Dynamic transparency for trail
    float alpha = clamp(glow * 1.2, 0.0, 1.0);

    fragColor = vec4(finalColor, alpha);
}
