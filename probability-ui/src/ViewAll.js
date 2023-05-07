import React, { useState, useEffect } from 'react';
import { getAllData } from './api';


function ViewAll(props) {
    const [responses, setResponses] = useState([]);

    useEffect(() => {
        async function fetchData() {
          const response = await getAllData();
          const data = await response;
          setResponses(data);
            }
        fetchData();
    }, []);

    return(
      <div>
      <h1>List of Responses</h1>
      {responses.map((response) => (
        <div key={response.id}>
          <p>Calc ID: {response.id}</p>
          <p>Value 1: {response.dataObject.val1}</p>
          <p>Value 2: {response.dataObject.val2}</p>
          <p>Result: {response.dataObject.result}</p>
          <p>Type: {response.dataObject.type === 0 ? "AND" : "OR"}</p>
          <p>-----------------------------------------</p>
        </div>
        
      ))}
    </div>);
}

export default ViewAll;