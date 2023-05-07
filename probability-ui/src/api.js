export async function getAllData() {
    const response = await fetch(`${process.env.REACT_APP_API_URL}/data/getall`);
    return await response.json();
  }

export async function submitCalculateRequest(val1, val2, selectedOption) {
    const endpoint = selectedOption === 'AND' ? `${process.env.REACT_APP_API_URL}/calculation/and` : `${process.env.REACT_APP_API_URL}/calculation/or`;
    const response = await fetch(endpoint, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ val1, val2 }),
    }).then(res => res.json());

    return response;
}

export async function GetById(id) {
    try {
        const endpoint = `${process.env.REACT_APP_API_URL}/data/${id}`;
        const response = await fetch(endpoint);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          throw new Error("Request failed with status " + response.status);
        }
      } catch (error) {
        console.error(error);
        return null;
      }
}