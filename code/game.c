#include <stdio.h>
#include "include/game.h"
#include "include/board.h"
#include "include/piece.h"

void start_game() {
    init_board();
    create_piece();
    display_board();
}

