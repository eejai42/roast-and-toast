
import BaseService from './BaseService';

import { GameDetail } from '../models/GameDetail';
import { Character } from '../models/Character';
import { AppUser } from '../models/AppUser';
import { Game } from '../models/Game';
import { WEapon } from '../models/WEapon';
import { Level } from '../models/Level';




class GuestService extends BaseService {
    async GetGameDetails(view:string = "") : Promise<GameDetail[]> { 
        return this.apiCall("GET", "Guest", "GameDetails", view, null); // GameDetail
    }
    async GetCharacters(view:string = "") : Promise<Character[]> { 
        return this.apiCall("GET", "Guest", "Characters", view, null); // Character
    }
    async GetAppUsers(view:string = "") : Promise<AppUser[]> { 
        return this.apiCall("GET", "Guest", "AppUsers", view, null); // AppUser
    }
    async GetGames(view:string = "") : Promise<Game[]> { 
        return this.apiCall("GET", "Guest", "Games", view, null); // Game
    }
    async GetWEapons(view:string = "") : Promise<WEapon[]> { 
        return this.apiCall("GET", "Guest", "WEapons", view, null); // WEapon
    }
    async GetLevels(view:string = "") : Promise<Level[]> { 
        return this.apiCall("GET", "Guest", "Levels", view, null); // Level
    }
}

export default new GuestService();                        