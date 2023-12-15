import React, { useState } from 'react';
import {
    MDBBtn,
    MDBCol,
    MDBInput,
    MDBRow
  } from 'mdb-react-ui-kit';

const SearchBar = ({onSubmit = () => null}) => {
    const [searchTerm, setSearchTerm] = useState('');
    const [error, setError] = useState('');
    
    const handleSubmit = (event) => {
        if (!searchTerm) {
            setError('You must provide a search term');
            onSubmit('');
        }
        
        onSubmit(searchTerm);
        event.preventDefault();
    };
    
    const handleSearchTermChange = (event) => {
        setError('');
        setSearchTerm(event.target.value);
    };
    
    return (
        <form onSubmit={handleSubmit}>
            <MDBRow className='row-cols-auto g-3 align-items-center justify-content-center mt-2'>
                <MDBCol>
                    <MDBInput type='text' value={searchTerm} onChange={handleSearchTermChange} label='Search Term' data-testid='search-box' />
                </MDBCol>
                <MDBCol>
                    <MDBBtn type='submit' data-testid='search-button'>Search</MDBBtn >
                </MDBCol>
            </MDBRow>
            <MDBRow className='row-cols-auto align-items-center justify-content-center mb-4 mt-2' >
                {
                    error && (
                        <p className='text-danger fs-6' data-testid='search-error'>{error}</p>
                    )
                }
            </MDBRow>
        </form>
    );
};

export default SearchBar;
