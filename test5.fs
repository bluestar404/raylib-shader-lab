#version 330

in vec2 fragTexCoord;
out vec4 fragColor;

uniform sampler2D texture0; // background (sky)
uniform float time;          // time in seconds

void main() {
    vec2 uv = fragTexCoord;

    // --- Moving, soft cloud patterns ---
    float cloud1 = sin((uv.x + time * 0.05) * 3.0) * 0.5 + 0.5;
    float cloud2 = sin((uv.y + time * 0.03) * 5.0) * 0.5 + 0.5;
    float cloud3 = sin((uv.x + uv.y) * 2.0 + time * 0.04) * 0.5 + 0.5;

    // Combine for soft puffy effect
    float cloudPattern = (cloud1 + cloud2 + cloud3) / 3.0;

    // Soften edges
    cloudPattern = smoothstep(0.4, 0.8, cloudPattern);

    // --- Transparency ---
    float alpha = cloudPattern * 0.5; // semi-transparent, adjust 0.0-1.0

    // White clouds
    vec3 cloudColor = vec3(1.0);

    // Sample background
    vec4 base = texture(texture0, uv);

    // Blend cloud with background using alpha
    fragColor = vec4(mix(base.rgb, cloudColor, alpha), alpha);
}
