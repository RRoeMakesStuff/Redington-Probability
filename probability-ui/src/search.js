import React, { useState, useEffect } from 'react';
import { GetById } from './api';

function Search(props) {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchResult, setSearchResult] = useState(null);

  const getById = async (id) => {
    setSearchResult(await GetById(id));
    console.log(searchResult);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    await getById(searchTerm);
  };

  const handleInputChange = (event) => {
    setSearchTerm(event.target.value);
  };

  return (
    <div>
        <form onSubmit={handleSubmit}>
        <label>
            Search term:
            <input type="text" value={searchTerm} onChange={handleInputChange} />
        </label>
        <button type="submit">Search</button>
        </form>
        {searchResult && (
        <div>
          <p>Calc ID: {searchResult.id}</p>
          <p>Value 1: {searchResult.dataObject.val1}</p>
          <p>Value 2: {searchResult.dataObject.val2}</p>
          <p>Result: {searchResult.dataObject.result}</p>
          <p>Type: {searchResult.dataObject.type === 0 ? "AND" : "OR"}</p>
        </div>
      )}
    </div>
    
  );
}

export default Search;