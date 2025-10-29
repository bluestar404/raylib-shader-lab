#version 330

in vec2 fragTexCoord;
out vec4 fragColor;

uniform sampler2D texture0;  // Background (e.g., grass or scene under the water)
uniform float time;          // Time in seconds

void main() {
    vec2 uv = fragTexCoord;

    // --- Subtle, calm distortion (like gentle water movement) ---
    float wave1 = sin(uv.x * 8.0 + time * 0.6);
    float wave2 = sin(uv.y * 10.0 + time * 0.8);
    float wave3 = sin((uv.x + uv.y) * 4.0 + time * 0.4);
    float distortion = (wave1 + wave2 + wave3) * 0.002;

    // Apply the gentle distortion to the UV
    vec2 distortedUV = uv + vec2(distortion * 0.5, distortion);

    // Sample the background texture (grass or whatever is underneath)
    vec4 base = texture(texture0, distortedUV);

    // --- Very subtle shimmer (optional, gives a living water feel) ---
    float shimmer = 0.5 + 0.5 * sin(time * 2.0 + uv.x * 10.0 + uv.y * 12.0);
    base.rgb += shimmer * 0.02;  // gentle brightness variation

    // --- Fully transparent base ---
    // We keep the alpha low so blending doesn’t “paint” over the background.
    float alpha = 0.15 + 0.05 * shimmer;  // mostly see-through

    fragColor = vec4(base.rgb, alpha);
}
