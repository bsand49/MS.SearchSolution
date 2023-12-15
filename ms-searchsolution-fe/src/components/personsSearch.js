import React, { useState } from 'react';
import axios from 'axios';
import SearchBar from './searchBar';
import PersonsTable from './personsTable';

const PersonsSearch = () => {
    const [persons, setPersons] = useState(null);
    const [hasSearched, setHasSearched] = useState(false);

    const API_URL = process.env.REACT_APP_BE_API_URL;
    const SEARCH_PERSONS_ENDPOINT_PATH = process.env.REACT_APP_BE_API_SEARCH_PERSONS_ENDPOINT_PATH;

    const onSearchSubmit = (searchTerm) => {
      setHasSearched(true);

      if (!searchTerm) {
        setPersons([]);
        setHasSearched(false);
        return;
      }
      
      performPersonsSearch(searchTerm);
    }
    
    const performPersonsSearch = (searchTerm) => {
      axios.get(`${API_URL}${SEARCH_PERSONS_ENDPOINT_PATH}?searchTerm=${searchTerm}`, {
        headers: {
          'Content-Type': 'application/json',
        }
      })
      .then(response => {
        if (response && response.status === 200 && response.data && response.data.persons) {
          setPersons(response.data.persons);
        }
      })
      .catch(error => {
        setPersons([]);
        console.error(error);
      });
    }
    
    return (
      <>
        <SearchBar onSubmit={(searchTerm) => onSearchSubmit(searchTerm)} />
        <PersonsTable persons={persons} hasSearched={hasSearched} />
      </>
    );
};

export default PersonsSearch;