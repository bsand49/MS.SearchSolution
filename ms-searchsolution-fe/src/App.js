import React from 'react';
import PersonsSearch from './components/personsSearch';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';

function App() {
  return (
    <div className="App">
      <PersonsSearch data-testid='persons-search' />
    </div>
  );
};

export default App;
