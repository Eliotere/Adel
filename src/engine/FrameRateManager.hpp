#ifndef DELTATIMER_HPP
#define DELTATIMER_HPP

#include <iostream>
#include <chrono>

#include <SDL2/SDL.h>

class FrameRateManager {
private:
    uint32_t targetDeltaTime;
    uint32_t lastUpdateTime ;
    uint32_t deltaTime;
public:

    FrameRateManager();
    FrameRateManager(uint32_t targetDeltaTime);
    ~FrameRateManager();

    /// @brief Starts the timer for FrameRateManager (if not called, the first frame might arrive without delay)
    void start();

    /// @brief Checks if enough time has passed to change frame
    /// @return has enough time passed
    bool check();

    /// @brief update class variables
    /// @param newTime is the new last time the frame passed to the next
    void update(uint32_t newTime);
    
    void print();

    /// @brief Returns the deltaTime
    /// @return The deltaTime (time elapsed between the start of the frame and the last)
    uint32_t getDeltaTime();
};

#endif // DELTATIMER_HPP