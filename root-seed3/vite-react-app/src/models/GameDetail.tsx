// Auto Generated Typescript Model interface

import { Game } from "./Game";

export interface GameDetail 
{
    GameDetailId? : string;
    
        /**
        * The Name of an attribute of the project
        */Name? : string;
    
        /**
        * The name of the game associated with the game detail.
        */GameLoadingScreen? : string;
    
        /**
        * The Value of an attribute of the project
        */Value? : string;
    
        /**
        * The game that this attribute/detail is associated with.
        */Game? : string;
    
        /**
        * A flag indicating if the game is the active game.
        */GameIsActive? : boolean;
    
        /**
        * A flag indicating if the associated game has been disabled (removed).
        */GameIsDisabled? : boolean;
    
    GameDetails_Games? : Game[];
    
}
