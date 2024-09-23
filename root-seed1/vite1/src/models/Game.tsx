// Auto Generated Typescript Model interface

import { GameDetail } from "./GameDetail";
import { Character } from "./Character";

export interface Game 
{
    GameId? : string;
    
        /**
        * Name of the character.
        */Name? : string;
    
        /**
        * Loading screen image.
        */LoadingScreen? : string;
    
        /**
        * Notes about the game
        */Notes? : string;
    
        /**
        * A flag indicating if this game is the active game.
        */IsActive? : boolean;
    
        /**
        * Links to the details about the game
        */GameDetails? : string;
    
        /**
        * A list of characters in the game.
        */Characters? : string;
    
        /**
        * Flag indicating if the game is disabled.
        */IsDisabled? : boolean;
    
    Game_GameDetails? : GameDetail[];
    Game_Characters? : Character[];
    
}
