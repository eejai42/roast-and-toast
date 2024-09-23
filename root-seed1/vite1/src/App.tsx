import { useEffect, useState } from 'react';
import './App.css';
import { Character } from './models/Character';
import GuestService from './effortlessapiservices/GuestService';

function App() {
  const [character, setCharacter] = useState<Character[]>([])

  useEffect(() => {
      async function GetCharacters() {
        const characters = await GuestService.GetCharacters("Grid view");
        setCharacter(characters);
      }
      GetCharacters();
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
        <h3>Characters</h3>
        {character.map((char) => (
          <div key={char.CharacterId} style={{backgroundColor: 'white'}}>
            <img src={char.Avatar} alt={char.Name} />
            <p>{char.Name}</p>  
          </div>
        ))}
                  </div>
                  </div>


  );
}

export default App;
