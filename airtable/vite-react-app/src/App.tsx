import './App.css';

function App() {
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
      </div>
    </div>
  );
}

export default App;
