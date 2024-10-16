// Auto Generated Typescript Model interface

import { Game } from "./Game";
import { Weapon } from "./Weapon";

export interface Character 
{
    CharacterId? : string;
    
        /**
        * Name of the character.
        */Name? : string;
    
        /**
        * A url to an image for this character in the game
        */Avatar? : string;
    
        /**
        * This is the primary weapon that this characer uses by default.
        */PrimaryWeapon? : string;
    PrimaryWeaponName? : string;
    PrimaryWeaponDescription? : string;
    PrimaryWeaponImageURL? : string;
    
        /**
        * The type of character (Marshmallow, Ninja, NPC, etc.)
        */Type? : string;
    
        /**
        * A flag indicating if this character is 'missing' - i.e. they have not been found yet.
        */IsMissing? : boolean;
    
        /**
        * The number of the level which the character shows up.  For some games, all characters will be introduced right out of the gate.  This field is agnostic of the specific values   - and it can be left blank.
        */IntroducedAtLevel? : number;
    
        /**
        * Short description of the character for the game.
        */Description? : string;
    Skills? : string;
    
        /**
        * The game associated with this character.
        */Game? : string;
    
        /**
        * A flag for whether the game associated with this character is active.
        */GameIsActive? : boolean;
    
    Characters_Games? : Game[];
    Characters_Weapons? : Weapon[];
    
}
