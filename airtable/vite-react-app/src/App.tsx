import { useEffect, useState } from "react";
import "./App.css";
import AdminService from "./effortlessapiservices/AdminService";
import { Character } from "./models/Character";

function App() {
  const [characters, setCharacters] = useState<Character[]>([]);

  useEffect(() => {
    // Fetch data from API
    (async () => {
      const characters = await AdminService.GetCharacters();
      setCharacters(characters);
    })();
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
        <p>
          Welcome to the <b>Vite React App Seed!</b>{" "}
        </p>
        <p>&nbsp;</p>
        <p>Add your content goes here.</p>
        <h3>Characters</h3>
        {characters.map((character) => (
          <div key={character.CharacterId}>
            <h4>{character.Name}</h4>
            <p>{character.Description}</p>
            <img src={character.Avatar} alt={character.Name} style={{width: '10em'}} />
          </div>
        ))}
      </div>
    </div>
  );
}
export default App;
