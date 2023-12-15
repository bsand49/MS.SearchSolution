import React from 'react';
import { MDBTable, MDBTableHead, MDBTableBody, MDBRow, MDBCol } from 'mdb-react-ui-kit';

const PersonsTable = ({persons, hasSearched}) => {
    if (!hasSearched) {
        return;
    }
    
    if (!persons || persons.length < 1) {
        return (
            <MDBRow className='row-cols-auto g-3 align-items-center justify-content-center'>
                <p className='fs-6' data-testid='no-search-results'>No search results found.</p>
            </MDBRow>    
        );
    }
    
    return (
        <MDBRow className='row-cols-auto g-3 align-items-center justify-content-center'>
            <MDBCol>
                <MDBTable striped data-testid='persons-table'>
                    <MDBTableHead>
                        <tr>
                            <th scope='col'>ID</th>
                            <th scope='col'>First Name</th>
                            <th scope='col'>Last Name</th>
                            <th scope='col'>Email</th>
                            <th scope='col'>Gender</th>
                        </tr>
                    </MDBTableHead>
                    <MDBTableBody>
                        {
                            persons.map((person) => (
                                <tr key={person.id}>
                                    <th scope='row'>{person.id}</th>
                                    <td>{person.first_name}</td>
                                    <td>{person.last_name}</td>
                                    <td>{person.email}</td>
                                    <td>{person.gender}</td>
                                </tr>
                            ))
                        }
                    </MDBTableBody>
                </MDBTable>
            </MDBCol>
        </MDBRow>
    );
};

export default PersonsTable;