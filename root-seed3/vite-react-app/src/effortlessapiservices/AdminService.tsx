
import BaseService from './BaseService';

import { GameDetail } from '../models/GameDetail';
import { Character } from '../models/Character';
import { AppUser } from '../models/AppUser';
import { Game } from '../models/Game';
import { WEapon } from '../models/WEapon';
import { Level } from '../models/Level';




class AdminService extends BaseService {
    async GetGameDetails(view:string = "") : Promise<GameDetail[]> { 
        return this.apiCall("GET", "Admin", "GameDetails", view, null); // GameDetail
    }
    async AddGameDetail(payload: GameDetail) : Promise<GameDetail> { 
        return this.apiCall("POST", "Admin", "GameDetail", "", payload as any); // GameDetail
    }
    async UpdateGameDetail(payload: GameDetail) : Promise<GameDetail> {
        return this.apiCall("PUT", "Admin", "GameDetail", "", payload as any); // GameDetail
    }
    async DeleteGameDetail(id:string) { 
        return this.apiCall("DELETE", "Admin", "GameDetail", "", id as any); // GameDetail
    }
    async GetCharacters(view:string = "") : Promise<Character[]> { 
        return this.apiCall("GET", "Admin", "Characters", view, null); // Character
    }
    async AddCharacter(payload: Character) : Promise<Character> { 
        return this.apiCall("POST", "Admin", "Character", "", payload as any); // Character
    }
    async UpdateCharacter(payload: Character) : Promise<Character> {
        return this.apiCall("PUT", "Admin", "Character", "", payload as any); // Character
    }
    async DeleteCharacter(id:string) { 
        return this.apiCall("DELETE", "Admin", "Character", "", id as any); // Character
    }
    async GetAppUsers(view:string = "") : Promise<AppUser[]> { 
        return this.apiCall("GET", "Admin", "AppUsers", view, null); // AppUser
    }
    async AddAppUser(payload: AppUser) : Promise<AppUser> { 
        return this.apiCall("POST", "Admin", "AppUser", "", payload as any); // AppUser
    }
    async UpdateAppUser(payload: AppUser) : Promise<AppUser> {
        return this.apiCall("PUT", "Admin", "AppUser", "", payload as any); // AppUser
    }
    async DeleteAppUser(id:string) { 
        return this.apiCall("DELETE", "Admin", "AppUser", "", id as any); // AppUser
    }
    async GetGames(view:string = "") : Promise<Game[]> { 
        return this.apiCall("GET", "Admin", "Games", view, null); // Game
    }
    async AddGame(payload: Game) : Promise<Game> { 
        return this.apiCall("POST", "Admin", "Game", "", payload as any); // Game
    }
    async UpdateGame(payload: Game) : Promise<Game> {
        return this.apiCall("PUT", "Admin", "Game", "", payload as any); // Game
    }
    async DeleteGame(id:string) { 
        return this.apiCall("DELETE", "Admin", "Game", "", id as any); // Game
    }
    async GetWEapons(view:string = "") : Promise<WEapon[]> { 
        return this.apiCall("GET", "Admin", "WEapons", view, null); // WEapon
    }
    async AddWEapon(payload: WEapon) : Promise<WEapon> { 
        return this.apiCall("POST", "Admin", "WEapon", "", payload as any); // WEapon
    }
    async UpdateWEapon(payload: WEapon) : Promise<WEapon> {
        return this.apiCall("PUT", "Admin", "WEapon", "", payload as any); // WEapon
    }
    async DeleteWEapon(id:string) { 
        return this.apiCall("DELETE", "Admin", "WEapon", "", id as any); // WEapon
    }
    async GetLevels(view:string = "") : Promise<Level[]> { 
        return this.apiCall("GET", "Admin", "Levels", view, null); // Level
    }
    async AddLevel(payload: Level) : Promise<Level> { 
        return this.apiCall("POST", "Admin", "Level", "", payload as any); // Level
    }
    async UpdateLevel(payload: Level) : Promise<Level> {
        return this.apiCall("PUT", "Admin", "Level", "", payload as any); // Level
    }
    async DeleteLevel(id:string) { 
        return this.apiCall("DELETE", "Admin", "Level", "", id as any); // Level
    }
}

export default new AdminService();                        