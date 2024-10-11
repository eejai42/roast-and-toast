
import BaseService from './BaseService';

import { Game } from '../models/Game';

import { GameDetail } from '../models/GameDetail';

import { Character } from '../models/Character';

import { AppUser } from '../models/AppUser';


class AdminService extends BaseService {
    
        
    
    async GetGames(view:string = 'Grid%20view', airtableWhere :string = '') : Promise<Game[]>  { 
        return this.apiCall("GET", "Admin", "Games", view, null, airtableWhere, null); // Game
    }        
    async GetGame(id:string)  : Promise<Game|null> { 
        var dbGames : any = await this.apiCall("GET", "Admin", "Games", null, null, "RECORD_ID()='" + id + "'", null); // Game
        if (!dbGames || (dbGames.length == 0)) return null;
        return dbGames[0];
        
   }
    
    
        
    
    async AddGame(Game:Game) : Promise<Game>  { 
        return this.apiCall("POST", "Admin", "Games", null, Game, null, null); // Game 1
   }
    
    
        
    
    async UpdateGame(Game:Game)  : Promise<Game> {
        return this.apiCall("PUT", "Admin", "Game", null, Game, null, null); // Game 2
   }
    
    
        
    
    async DeleteGame(id:string)  : Promise<Game> { 
        return this.apiCall("DELETE", "Admin", "Game", "", id, null, null); // Game
   }
    
    
        
    
    async GetGameDetails(view:string = 'Grid%20view', airtableWhere :string = '') : Promise<GameDetail[]>  { 
        return this.apiCall("GET", "Admin", "GameDetails", view, null, airtableWhere, null); // GameDetail
    }        
    async GetGameDetail(id:string)  : Promise<GameDetail|null> { 
        var dbGameDetails : any = await this.apiCall("GET", "Admin", "GameDetails", null, null, "RECORD_ID()='" + id + "'", null); // GameDetail
        if (!dbGameDetails || (dbGameDetails.length == 0)) return null;
        return dbGameDetails[0];
        
   }
    
    
        
    
    async AddGameDetail(GameDetail:GameDetail) : Promise<GameDetail>  { 
        return this.apiCall("POST", "Admin", "GameDetails", null, GameDetail, null, null); // GameDetail 1
   }
    
    
        
    
    async UpdateGameDetail(GameDetail:GameDetail)  : Promise<GameDetail> {
        return this.apiCall("PUT", "Admin", "GameDetail", null, GameDetail, null, null); // GameDetail 2
   }
    
    
        
    
    async DeleteGameDetail(id:string)  : Promise<GameDetail> { 
        return this.apiCall("DELETE", "Admin", "GameDetail", "", id, null, null); // GameDetail
   }
    
    
        
    
    async GetCharacters(view:string = 'Grid%20view', airtableWhere :string = '') : Promise<Character[]>  { 
        return this.apiCall("GET", "Admin", "Characters", view, null, airtableWhere, null); // Character
    }        
    async GetCharacter(id:string)  : Promise<Character|null> { 
        var dbCharacters : any = await this.apiCall("GET", "Admin", "Characters", null, null, "RECORD_ID()='" + id + "'", null); // Character
        if (!dbCharacters || (dbCharacters.length == 0)) return null;
        return dbCharacters[0];
        
   }
    
    
        
    
    async AddCharacter(Character:Character) : Promise<Character>  { 
        return this.apiCall("POST", "Admin", "Characters", null, Character, null, null); // Character 1
   }
    
    
        
    
    async UpdateCharacter(Character:Character)  : Promise<Character> {
        return this.apiCall("PUT", "Admin", "Character", null, Character, null, null); // Character 2
   }
    
    
        
    
    async DeleteCharacter(id:string)  : Promise<Character> { 
        return this.apiCall("DELETE", "Admin", "Character", "", id, null, null); // Character
   }
    
    
        
    
    async GetAppUsers(view:string = 'Grid%20view', airtableWhere :string = '') : Promise<AppUser[]>  { 
        return this.apiCall("GET", "Admin", "AppUsers", view, null, airtableWhere, null); // AppUser
    }        
    async GetAppUser(id:string)  : Promise<AppUser|null> { 
        var dbAppUsers : any = await this.apiCall("GET", "Admin", "AppUsers", null, null, "RECORD_ID()='" + id + "'", null); // AppUser
        if (!dbAppUsers || (dbAppUsers.length == 0)) return null;
        return dbAppUsers[0];
        
   }
    
    
        
    
    async AddAppUser(AppUser:AppUser) : Promise<AppUser>  { 
        return this.apiCall("POST", "Admin", "AppUsers", null, AppUser, null, null); // AppUser 1
   }
    
    
        
    
    async UpdateAppUser(AppUser:AppUser)  : Promise<AppUser> {
        return this.apiCall("PUT", "Admin", "AppUser", null, AppUser, null, null); // AppUser 2
   }
    
    
        
    
    async DeleteAppUser(id:string)  : Promise<AppUser> { 
        return this.apiCall("DELETE", "Admin", "AppUser", "", id, null, null); // AppUser
   }
    
    
}

export default new AdminService();