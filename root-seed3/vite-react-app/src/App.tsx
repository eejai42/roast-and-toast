import { useEffect, useState } from 'react';
import './App.css';
import { Character } from './models/Character';
import GuestService from './effortlessapiservices/GuestService';

function App() {
  const [characters, setCharacters] = useState<Character[]>([]);
  useEffect(() => {
    async function LoadCharacters() {
      const characters = await GuestService.GetCharacters('Grid view');
      console.error("characters", characters);
      setCharacters(characters)
    }

    LoadCharacters();
  }, []);
  return (
    <div id="root" className="root">
      {/* Header */}
      <div className="header">
        <img src="/favicon.ico" alt="logo" className="logo" />
        <h1 className="header-title">Vite React App Seed</h1>
        <button className="settings-button">Settings</button>
      </div>

      {/* Main content */}
      <div className="main-content">
        <p>Welcome to the <b>Vite React App Seed!</b> </p>
        <p>&nbsp;</p>
        <p>Add your content goes here.</p>
        <h3>Missing Characters</h3>
        {characters.length === 0 && <p>Loading...</p>}
        {characters.length > 0 && (
          <ul>
            {characters.filter(character => character.IsMissing).map((character) => (
              <li key={character.CharacterId}  style={{backgroundColor: character.BackgroundColor}}>
                <img src={character.Avatar} alt={character.Name} />
                <h4>{character.Name}</h4>
                <p>{character.Description}</p>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}

export default App;
