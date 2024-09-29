
#include "FrameRateManager.hpp"

FrameRateManager::FrameRateManager():
    lastUpdateTime (0),
    deltaTime(0)
{

}

FrameRateManager::~FrameRateManager(){
}

FrameRateManager::FrameRateManager(uint32_t targetDeltaTime):
    lastUpdateTime (0),
    deltaTime(0),
    targetDeltaTime(targetDeltaTime)
{
}

void FrameRateManager::start(){
    lastUpdateTime = SDL_GetTicks();
}

bool FrameRateManager::check(){
    uint32_t newTime = SDL_GetTicks();
    if((newTime - lastUpdateTime) > targetDeltaTime){
        update(newTime);
        return true;
    }else{
        return false;
    }
}

void FrameRateManager::update(uint32_t newTime){
    deltaTime = newTime - lastUpdateTime;
    lastUpdateTime = newTime;
}

void FrameRateManager::print(){
}

uint32_t FrameRateManager::getDeltaTime(){
    return deltaTime;
}
