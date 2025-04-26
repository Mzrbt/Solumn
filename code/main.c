#include <stdio.h>
#include <SDL2/SDL.h>

#include "include/board.h"
#include "include/game.h"

int main(int argc, char* argv[]) {
    if (SDL_Init(SDL_INIT_VIDEO) != 0){
	    SDL_Log("Unable to init SDL : %s", SDL_GetError());
	    return 1;
    }

    SDL_Window* window = SDL_CreateWindow("Solumn", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 800, 600, 0);
    
    SDL_Renderer* renderer = SDL_CreateRenderer(window, -1, 0);

    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
    SDL_RenderClear(renderer);
    SDL_RenderPresent(renderer);

    SDL_Delay(3000);

    SDL_DestroyRenderer(renderer);
    SDL_DestroyWindow(window);
    SDL_Quit();

    //start_game();
    return 0;
}
