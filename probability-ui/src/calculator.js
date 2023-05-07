import React from 'react';
import { useState } from 'react';
import { submitCalculateRequest } from './api';

function Calculator(props) {
  const [val1, setValue1] = useState(0);
  const [val2, setValue2] = useState(0);
  const [selectedOption, setSelectedOption] = useState("AND");
  const [response, setResponse] = useState(null);

  const handleOptionChange = (e) => {
    setSelectedOption(e.target.value);
  };

  const handleSubmit = async (event) => {
    var result = await submitCalculateRequest(val1, val2, selectedOption);
    setResponse(result);
  };


  return (
    <div>
      <label>
        Probability 1:
        <input type="number" min={0} max={1} value={val1} onChange={e => setValue1(e.target.value)} />
      </label>
      <br />
      <label>
        Probability 2:
        <input type="number" min={0} max={1} value={val2} onChange={e => setValue2(e.target.value)} />
      </label>
      <br />
      <label>
        AND
        <input type="radio" value="AND" checked={selectedOption === "AND"} onChange={handleOptionChange} />
      </label>
      <label>
        OR
        <input type="radio" value="OR" checked={selectedOption === "OR"} onChange={handleOptionChange} />
      </label>
      <br />
      <button onClick={handleSubmit}>Submit</button>
      {response && (
        <div>
          <p>Value 1: {response.val1}</p>
          <p>Value 2: {response.val2}</p>
          <p>Result: {response.result}</p>
          <p>Type: {response.type===0 ? "AND" : "OR" }</p>
        </div>
      )}
    </div>
  );
}

export default Calculator;