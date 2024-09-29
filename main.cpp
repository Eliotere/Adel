#include <iostream>

#include <algorithm>
#include <chrono>
#include <ctime>
#include <iomanip>

#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>

#include "src/engine/FrameRateManager.hpp"

#define WINDOW_X_SIZE = 900
#define WINDOW_Y_SIZE = 600

const int SCREEN_FPS = 144;
const int SCREEN_TICKS_PER_FRAME = 1000 / SCREEN_FPS;

const double TARGET_FPS_RATE = 1;
const int TARGET_FPS_DELAY = 1000 / TARGET_FPS_RATE;

int initSDL(){

    if(SDL_Init(SDL_INIT_VIDEO) < 0)
    {
        std::cout << "Failed to initialize the SDL2 library\n";
        return -1;
    }

    return 0;
}

int createSDLWindow(){
    SDL_Window *window = SDL_CreateWindow("Adel",
                                        SDL_WINDOWPOS_CENTERED,
                                        SDL_WINDOWPOS_CENTERED,
                                        680, 480,
                                        0);
        if(!window)
    {
        std::cout << "Failed to create window\n";
        return -1;
    }

    return 0;
}

int main()
{

    std::cout << "Hello World!" << std::endl;
    initSDL();

    // SDL_Surface *window_surface = SDL_GetWindowSurface(window);

    // if(!window_surface)
    // {
    //     std::cout << "Failed to get the surface from the window\n";
    //     return -1;
    // }

    // SDL_UpdateWindowSurface(window);

    // SDL_Delay(5000);

    bool isRunning = true;

    FrameRateManager frameRateManager = FrameRateManager(TARGET_FPS_DELAY);
    frameRateManager.start();

    int compt = 0;
    
    while(isRunning){
        if(frameRateManager.check() > 0){
            std::cout << "deltaTime is : " << frameRateManager.getDeltaTime() << std::endl;
            ++compt;
        }
        if(compt > 10){
            isRunning = false;
        }
    }


    std::cout << "Bye World!" << std::endl;
    return 0;
    
}