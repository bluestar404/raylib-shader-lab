#include "raylib.h"
#include <stdlib.h>
#include <time.h>
#include <math.h>

#define NUM_GRASS 400
#define MAX_DECORATIONS 100

typedef enum {
    DECOR_BIRD,
    DECOR_FISH,
    DECOR_FLOWER
} DecorType;

typedef struct {
    Vector2 pos;
    DecorType type;
    float life;
    bool active;
} Decoration;

typedef struct {
    float x;
    float height;
} Blade;

float RandRange(float a, float b) {
    return a + (float)rand() / RAND_MAX * (b - a);
}

int main(void)
{
    const int screenWidth = 800;
    const int screenHeight = 600;

    InitWindow(screenWidth, screenHeight, " Grass + Water + Sun");
    SetTargetFPS(60);
    srand(time(NULL));

    // --- Grass blades ---
    Blade grass[NUM_GRASS];
    for (int i = 0; i < NUM_GRASS; i++) {
        grass[i].x = (float)(rand() % screenWidth);
        grass[i].height = 10.0f + (float)(rand() % 40);
    }

    // --- Shaders ---
    Shader grassShader = LoadShader(0, "test3.fs");
    int locGrassTime = GetShaderLocation(grassShader, "time");

    Shader waterShader = LoadShader(0, "test4.fs");
    int locWaterTime = GetShaderLocation(waterShader, "time");

    Shader sunShader = LoadShader(0, "test6.fs");
    int locSunTime = GetShaderLocation(sunShader, "time");

    float shaderTime = 0.0f;

    // --- Render textures ---
    RenderTexture2D grassTarget = LoadRenderTexture(screenWidth, screenHeight);
    RenderTexture2D waterTarget = LoadRenderTexture(screenWidth, screenHeight);
    RenderTexture2D sunTarget = LoadRenderTexture(100, 100);

    Rectangle waterRect = {0, screenHeight - screenHeight / 4, screenWidth, screenHeight / 4};
    Vector2 sunPos = {screenWidth - 150.0f, 80.0f};

    // --- Decorations ---
    Decoration decorations[MAX_DECORATIONS] = {0};

    while (!WindowShouldClose())
    {
        float dt = GetFrameTime();
        shaderTime += dt;

        // Update decoration life
        for (int i = 0; i < MAX_DECORATIONS; i++) {
            if (decorations[i].active) {
                decorations[i].life -= dt;
                if (decorations[i].life <= 0) decorations[i].active = false;
            }
        }

        // --- Render grass ---
        BeginTextureMode(grassTarget);
        ClearBackground(BLANK);
        for (int i = 0; i < NUM_GRASS; i++)
            DrawRectangle((int)grass[i].x, screenHeight - 50 - (int)grass[i].height, 1, (int)grass[i].height, WHITE);
        EndTextureMode();

        // --- Render water ---
        BeginTextureMode(waterTarget);
        ClearBackground(BLANK);
        DrawRectangle(0, 0, waterRect.width, waterRect.height, WHITE);
        EndTextureMode();

        // --- Render sun ---
        BeginTextureMode(sunTarget);
        ClearBackground(BLANK);
        DrawCircle(sunTarget.texture.width / 2, sunTarget.texture.height / 2, 40, YELLOW);
        EndTextureMode();

        // --- Draw Scene ---
        BeginDrawing();
        ClearBackground(SKYBLUE);

        // Clouds
        DrawCircle(120, 100, 40, WHITE);
        DrawCircle(160, 90, 50, WHITE);
        DrawCircle(200, 100, 40, WHITE);

        // Ground
        DrawRectangle(0, screenHeight - 50, screenWidth, 50, DARKGREEN);

        // Grass
        BeginShaderMode(grassShader);
        SetShaderValue(grassShader, locGrassTime, &shaderTime, SHADER_UNIFORM_FLOAT);
        DrawTextureRec(grassTarget.texture,
                       (Rectangle){0, 0, (float)grassTarget.texture.width, (float)-grassTarget.texture.height},
                       (Vector2){0, 0}, WHITE);
        EndShaderMode();

        // Water
        BeginShaderMode(waterShader);
        SetShaderValue(waterShader, locWaterTime, &shaderTime, SHADER_UNIFORM_FLOAT);
        DrawTextureRec(waterTarget.texture,
                       (Rectangle){0, 0, (float)waterTarget.texture.width, (float)-waterTarget.texture.height},
                       (Vector2){0, waterRect.y}, WHITE);
        EndShaderMode();

        // Sun
        BeginShaderMode(sunShader);
        SetShaderValue(sunShader, locSunTime, &shaderTime, SHADER_UNIFORM_FLOAT);
        DrawTextureRec(sunTarget.texture,
                       (Rectangle){0, 0, (float)sunTarget.texture.width, (float)-sunTarget.texture.height},
                       sunPos, WHITE);
        EndShaderMode();

        // --- Decorations ---
        for (int i = 0; i < MAX_DECORATIONS; i++) {
            if (!decorations[i].active) continue;
            Color c = WHITE;
            switch (decorations[i].type) {
                case DECOR_BIRD:   c = (Color){200, 200, 255, 255}; break;
                case DECOR_FISH:   c = (Color){0, 150, 255, 255}; break;
                case DECOR_FLOWER: c = (Color){255, 100, 200, 255}; break;
            }
            float alpha = fmaxf(0.0f, decorations[i].life / 15.0f);
            c.a = (unsigned char)(alpha * 255);
            DrawCircleV(decorations[i].pos, 5, c);
        }

        DrawText(" Peaceful Nature Scene", 10, 10, 20, WHITE);

        EndDrawing();
    }

    // Cleanup
    UnloadRenderTexture(grassTarget);
    UnloadRenderTexture(waterTarget);
    UnloadRenderTexture(sunTarget);
    UnloadShader(grassShader);
    UnloadShader(waterShader);
    UnloadShader(sunShader);
    CloseWindow();
    return 0;
}
